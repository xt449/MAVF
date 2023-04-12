using MILAV.API;
using MILAV.API.Device;
using Newtonsoft.Json;

namespace MILAV
{
    public class Controller
    {
        public string GetText() => "Hello world!";

        private readonly Configuration configuration;

        public string controlState;

        public Controller()
        {
            if (File.Exists("./config.json"))
            {
                Console.WriteLine("reading config file");

                using var reader = new StreamReader("./config.json");
                var configuration = (Configuration?)new JsonSerializer().Deserialize(reader, typeof(Configuration));
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

                configuration = new Configuration();

                using var writer = new StreamWriter("./config.json", false);
                new JsonSerializer().Serialize(writer, configuration);
            }

            controlState = configuration.defaultState;
            UpdateUserControlStates();
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
            foreach (var user in configuration.users)
            {
                user.SetControlState(controlState);
            }
        }

        public AbstractDevice[] GetDevices()
        {
            return (AbstractDevice[])configuration.devices.Clone();
        }

        public AbstractDevice? GetDeviceById(string id)
        {
            return configuration.devices.FirstOrDefault(d => d?.id == id, null);
        }
    }
}
