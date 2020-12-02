using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SteamRoller
{
    class SteamClient
    {
        private HttpClient client;
        private SteamURLs SURL;
        public bool isConnected { get; private set; }
        public SteamClient()
        {
            SURL = new SteamURLs();
            Connect();
        }

        public async void Connect()
        {
            client = new HttpClient();

            isConnected = await TestConnection();
        }

        private async Task<bool> TestConnection()
        {
            var Result = await client.GetAsync(SURL.SteamCommunity.);
            if (Result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return false;
            } else
            {
                return true;
            }

        }
    }
}
