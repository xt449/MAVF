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
        }

        public AbstractDevice[] GetAllDevices()
        {
            return (AbstractDevice[])configuration.Devices.Clone();
        }

        public bool TryGetDevice(string id, out AbstractDevice? o)
        {
            o = configuration.Devices.FirstOrDefault(d => d?.Id == id, null);
            return o != null;
        }
    }
}
