using MILAV.API;
using Newtonsoft.Json;

namespace MILAV
{
    public class Controller
    {
        private readonly JsonSerializer json = new JsonSerializer();

        private Configuration configuration;
        private string controlState;

        public Controller()
        {
            LoadConfiguration();

            // Validate again
            if (configuration == null) throw new Exception("configuration is null");

            controlState = configuration.defaultState;
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

                configuration = new Configuration(false, "");

                using var writer = new StreamWriter("./config.json", false);
                json.Serialize(writer, configuration);
            }
        }

        public string GetDefaultControlState() => configuration.defaultState;

        public string GetControlState() => controlState;

        public void SetControlState(string nextState)
        {
            if (controlState == nextState)
            {
                return;
            }

            controlState = configuration.defaultState;
            UpdateUserControlStates();
        }

        private void UpdateUserControlStates()
        {
            foreach (var user in configuration.users.Values)
            {
                user.SetControlState(controlState);
            }
        }

        public Dictionary<string, MILAV.API.Device.IDevice> GetDevices()
        {
            return configuration.devices;
        }

        public MILAV.API.Device.IDevice? GetDeviceById(string id)
        {
            return configuration.devices[id];
        }

        public User? GetUserByIp(string ip)
        {
            return configuration.users[ip];
        }
    }
}
