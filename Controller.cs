using MAVF.API;
using System.Text.Json;

namespace MAVF
{
	public class Controller
	{
		private static readonly string configFilePath = "./config.json";

		private Configuration configuration;
		private readonly UserInterface masterUser;
		private string mode;

		public Controller()
		{
			LoadConfiguration();

			// Validate again
			if (configuration == null) throw new Exception("configuration is null");

			masterUser = configuration.Users[configuration.MasterUserId] ?? throw new Exception("Invalid master user id");

			if (!configuration.Modes.Contains(configuration.DefaultModeId))
			{
				throw new Exception("Invalid default mode id");
			}
			mode = configuration.DefaultModeId;

			UpdateUserModes();
		}

		public void LoadConfiguration()
		{
			if (File.Exists(configFilePath))
			{
				Console.WriteLine("Reading config file");

				var jsonString = File.ReadAllText(configFilePath);

				try
				{
					var configuration = JsonSerializer.Deserialize<Configuration>(jsonString);

					if (configuration == null)
					{
						throw new JsonException("Unable to deserialize JSON. Deserialization returned null.");
					}

					this.configuration = configuration;
				}
				catch (Exception e)
				{
					Console.WriteLine("Invalid configuration. See details:");
					Console.WriteLine(e);

					CreateDefaultConfiguration();
				}

				if (configuration != null)
				{
				}
				else
				{
					throw new IOException("Unable to deserialize JSON configuration file contents");
				}
			}
			else
			{
				CreateDefaultConfiguration();
			}

			// Write configuration to file
			File.WriteAllText(configFilePath, JsonSerializer.Serialize(configuration));
		}

		private void CreateDefaultConfiguration()
		{
			Console.WriteLine("Creating default config file");

			configuration = new Configuration();
		}

		public UserInterface GetMasterUser() => masterUser;

		public string GetDefaultMode() => configuration.DefaultModeId;

		public string GetMode() => mode;

		public void SetMode(string nextMode)
		{
			if (mode == nextMode)
			{
				return;
			}

			if (!configuration.Modes.Contains(nextMode))
			{
				throw new Exception("Invalid mode id");
			}
			mode = nextMode;

			UpdateUserModes();
		}

		private void UpdateUserModes()
		{
			foreach (var user in configuration.Users.Values)
			{
				user.UpdateMode(mode);
			}
		}

		public Dictionary<string, API.Device.Device> GetDevices()
		{
			return configuration.Devices;
		}

		public API.Device.Device? GetDeviceById(string id)
		{
			return configuration.Devices[id];
		}

		public UserInterface? GetUserByIp(string ip)
		{
			return configuration.Users[ip];
		}
	}
}
