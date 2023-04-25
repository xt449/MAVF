using MILAV.API;
using MILAV.API.Device;
using Newtonsoft.Json;

namespace MILAV
{
    public class Controller
    {
        private readonly JsonSerializer json = new JsonSerializer();

        private Configuration configuration;
        private readonly User masterUser;
        private ControlState controlState;

        public Controller()
        {
            LoadConfiguration();

            // Validate again
            if (configuration == null) throw new Exception("configuration is null");

            masterUser = configuration.users[configuration.masterUser] ?? throw new Exception("Invalid master user id");

            controlState = configuration.states[configuration.defaultState] ?? throw new Exception("Invalid default control state id");
            UpdateUserControlStates();
        }

        public void LoadConfiguration()
        {
            if (File.Exists("./config.json"))
            {
                Console.WriteLine("reading config file");

                using var reader = new StreamReader("./config.json");
                var configuration = (Configuration?)json.Deserialize(reader, typeof(Configuration));
                if (configuration != null)
                {
                    this.configuration = configuration;
                }
                else
                {
                    throw new IOException("Unable to deserialize JSON configuration file contents");
                }
            }
            else
            {
                Console.WriteLine("creating default config file");

                configuration = new Configuration("");

                using var writer = new StreamWriter("./config.json", false);
                json.Serialize(writer, configuration);
            }
        }

        public User GetMasterUser() => masterUser;

        public string GetDefaultControlState() => configuration.defaultState;

        public ControlState GetControlState() => controlState;

        public void SetControlState(string nextState)
        {
            if (controlState.Id == nextState)
            {
                return;
            }

            controlState = configuration.states[nextState] ?? throw new Exception("Invalid control state id");
            UpdateUserControlStates();
        }

        private void UpdateUserControlStates()
        {
            foreach (var user in configuration.users.Values)
            {
                user.SetControlGroups(controlState.controlling[user.Id]);
            }
        }

        public Dictionary<string, IDevice> GetDevices()
        {
            return configuration.devices;
        }

        public IDevice? GetDeviceById(string id)
        {
            return configuration.devices[id];
        }

        public User? GetUserByIp(string ip)
        {
            return configuration.users[ip];
        }
    }
}
