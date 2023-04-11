using MILAV.API.Device;
using MILAV.Config;
using MILAV.Device.VTC;
using Newtonsoft.Json;

namespace MILAV
{
    public class Controller
    {
        public string GetText() => "Hello world!";

        private readonly JsonConfiguration configuration;

        public string controlState;

        public Controller()
        {
            if (File.Exists("./config.json"))
            {
                Console.WriteLine("reading config file");

                using var reader = new StreamReader("./config.json");
                var configuration = (JsonConfiguration?)new JsonSerializer().Deserialize(reader, typeof(JsonConfiguration));
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

                configuration = JsonConfiguration.New(new CustomVTC());

                using var writer = new StreamWriter("./config.json", false);
                new JsonSerializer().Serialize(writer, configuration);
            }

            controlState = configuration.DefaultState;
            UpdateDeviceControlStates();
        }

        public string GetDefaultControlState() => configuration.DefaultState;

        public string GetControlState() => controlState;

        public void SetControlState(string nextState)
        {
            if(controlState == nextState)
            {
                return;
            }

            controlState = configuration.DefaultState;
            UpdateDeviceControlStates();
        }

        private void UpdateDeviceControlStates()
        {
            foreach (var device in configuration.Devices)
            {
                device.SetControlState(controlState);
            }
        }

        public AbstractDevice[] GetDevices()
        {
            return (AbstractDevice[])configuration.Devices.Clone();
        }

        public AbstractDevice? GetDeviceById(string id)
        {
            return configuration.Devices.FirstOrDefault(d => d?.Id == id, null);
        }
    }
}
