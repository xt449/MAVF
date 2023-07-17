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
		private string mode;

		public Controller()
		{
			LoadConfiguration();

			// Validate again
			if (configuration == null) throw new Exception("configuration is null");

			masterUser = configuration.users[configuration.masterUserId] ?? throw new Exception("Invalid master user id");

			if (!configuration.modes.Contains(configuration.defaultModeId))
			{
				throw new Exception("Invalid default mode id");
			}
			mode = configuration.defaultModeId;

			UpdateUserModes();
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

				this.configuration = new Configuration();

				using var writer = new StreamWriter("./config.json", false);
				json.Serialize(writer, configuration);
			}
		}

		public User GetMasterUser() => masterUser;

		public string GetDefaultMode() => configuration.defaultModeId;

		public string GetMode() => mode;

		public void SetMode(string nextMode)
		{
			if (mode == nextMode)
			{
				return;
			}

			if (!configuration.modes.Contains(nextMode))
			{
				throw new Exception("Invalid mode id");
			}
			mode = nextMode;

			UpdateUserModes();
		}

		private void UpdateUserModes()
		{
			foreach (var user in configuration.users.Values)
			{
				user.UpdateMode(mode);
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
