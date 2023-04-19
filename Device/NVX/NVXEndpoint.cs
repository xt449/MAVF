using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;

namespace MILAV.Device.NVX
{
    public class NVXEndpoint
    {
        private readonly string username;
        private readonly string password;
        private readonly HttpClient client;

        private readonly Dictionary<int, string> inputUUIDs = new Dictionary<int, string>();
        private readonly Dictionary<int, string> outputUUIDs = new Dictionary<int, string>();

        public NVXEndpoint(NVXInputOutput data)
        {
            username = data.username;
            password = data.password;

            var clientHandler = new HttpClientHandler();
            clientHandler.CookieContainer = new CookieContainer();
            clientHandler.UseCookies = true;
            client = new HttpClient(clientHandler);
            client.BaseAddress = new Uri($"https://{data.ip}");

            if(!Authenticate().Result)
            {
                throw new Exception($"NVXEndpoint failed authentication @ {client.BaseAddress}");
            }
        }

        private async Task<bool> Authenticate()
        {
            // No need to GET the userlogin
            //await client.GetAsync("userlogin.html");
            var response = await client.PostAsync("/userlogin.html", new StringContent($"login={username}&&passwd={password}", Encoding.UTF8, "text/plain"));
            return response?.IsSuccessStatusCode ?? false;
        }

        private async Task UpdateInputs()
        {
            inputUUIDs.Clear();

            var response = await client.GetAsync("/Device/AudioVideoInputOutput/Inputs");
            var body = await response.Content.ReadAsStringAsync();
            var json = JToken.Parse(body);

            var inputs = json["Device"]?["AudioVideoInputOutput"]?["Inputs"] ?? throw new JsonException("Unable to read path 'Device.AudioVideoInputOutput.Inputs' from NVX response");

            foreach (var input in inputs)
            {
                inputUUIDs.Add(
                    int.Parse(((string?)input["Name"] ?? throw new JsonException($"Unable to deserialize input. Missing Name property")).Substring(5)) + 1,
                    (string?)input["Uuid"] ?? throw new JsonException($"Unable to deserialize input. Missing Uuid property")
                );
            }
        }

        private async Task UpdateOutputs()
        {
            outputUUIDs.Clear();

            var response = await client.GetAsync("/Device/AudioVideoInputOutput/Outputs");
            var body = await response.Content.ReadAsStringAsync();
            var json = JToken.Parse(body);

            var outputs = json["Device"]?["AudioVideoInputOutput"]?["Outputs"] ?? throw new JsonException("Unable to read path 'Device.AudioVideoInputOutput.Outputs' from NVX response");

            foreach (var output in outputs)
            {
                inputUUIDs.Add(
                    int.Parse(((string?)output["Name"] ?? throw new JsonException($"Unable to deserialize input. Missing Name property")).Substring(6)) + 1,
                    (string?)output["Uuid"] ?? throw new JsonException($"Unable to deserialize input. Missing Uuid property")
                );
            }
        }

        public bool Route(NVXInputOutput input, int port)
        {
            return Task.Run(async () =>
            {
                await UpdateInputs();
                await UpdateOutputs();

                // Paused working on this for now since the NVX devices that we have appear to be error prone
                // NVX end points seem to have a very incomplete API and documentation
                var response = await client.PostAsync("/Device", null) ?? throw new NotImplementedException();

                return response.IsSuccessStatusCode;
            }).Result;
        }
    }
}
