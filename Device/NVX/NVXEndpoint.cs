﻿using Newtonsoft.Json;
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

        public NVXEndpoint(string ip, string username, string password)
        {
            this.username = username;
            this.password = password;

            var clientHandler = new HttpClientHandler();
            clientHandler.CookieContainer = new CookieContainer();
            clientHandler.UseCookies = true;
            client = new HttpClient(clientHandler);
            client.BaseAddress = new Uri($"https://{ip}");
            
            // Authentication Headers
            client.DefaultRequestHeaders.Add("Origin", client.BaseAddress.ToString());
            client.DefaultRequestHeaders.Add("Referer", $"{client.BaseAddress}/userlogin.html");

            Authenticate().Wait();
        }

        private async Task Authenticate()
        {
            // No need to GET the userlogin
            //await client.GetAsync("userlogin.html");
            await client.PostAsync("/userlogin.html", new StringContent($"login={username}&&passwd={password}", Encoding.UTF8, "text/plain"));
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
                inputUUIDs.Add(int.Parse(((string)input["Name"]).Substring(5)) + 1, (string?)input["Uuid"]);
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
                inputUUIDs.Add(int.Parse(((string)output["Name"]).Substring(6)) + 1, (string?)output["Uuid"]);
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
                var response = await client.PostAsync("/Device", new StringContent(null)) ?? throw new NotImplementedException();

                return response.IsSuccessStatusCode;
            }).Result;
        }
    }
}