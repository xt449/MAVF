using MILAV.API;
using MILAV.API.Device;
using Newtonsoft.Json;

namespace MILAV
{
    public class Controller
    {
        private Configuration configuration;
        private string controlState;

        private Dictionary<string, Input> inputs;
        private Dictionary<string, Output> outputs;

        public Controller()
        {
            LoadConfiguration();

            // Validate again
            if (configuration == null) throw new Exception("configuration is null");
            if (configuration.defaultState == null) throw new Exception("controlState is null");
            if (inputs == null) throw new Exception("inputs is null");
            if (outputs == null) throw new Exception("outputs is null");

            controlState = configuration.defaultState;
            UpdateUserControlStates();
        }

        public void LoadConfiguration()
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

            inputs = new Dictionary<string, Input>();
            outputs = new Dictionary<string, Output>();

            // Validate inputs/outputs
            foreach (var device in configuration.devices)
            {
                if (device.inputs != null)
                {
                    foreach (var input in device.inputs)
                    {
                        if (inputs.ContainsKey(input.id))
                        {
                            throw new IOException($"Duplicate device input id: '{input.id}'");
                        }
                        inputs[input.id] = input;
                    }
                }

                if (device.outputs != null)
                {
                    foreach (var output in device.outputs)
                    {
                        if (outputs.ContainsKey(output.id))
                        {
                            throw new IOException($"Duplicate device output id: '{output.id}'");
                        }
                        outputs[output.id] = output;
                    }
                }
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
