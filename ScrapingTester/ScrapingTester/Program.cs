using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Numerics;
using System.Threading.Tasks;

namespace ScrapingTester
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string BaseURL = "https://steamcommunity.com/";
            string Profiles = "profiles/";
            string NewTradeOffer = "tradeoffer/new/?partner=";

            string FriendCode, SteamID, CombinedURL;

            Console.WriteLine("Please Enter the SteamID");
            SteamID = Console.ReadLine();

            
            if (SteamID != string.Empty)
            {
                CombinedURL = BaseURL + SteamID;
            } else
            {
                CombinedURL = "IDK";
            }

            using var client = new HttpClient();

            var Resp = await client.GetStringAsync(CombinedURL);

            var Content = new FormUrlEncodedContent(CreateContent("onlineboy"));
            var Response = client.PostAsync(CombinedURL, Content).Result;
            string ResponseContent = Response.Content.ReadAsStringAsync().Result;

            RSAKey RSAkey = JsonConvert.DeserializeObject<RSAKey>(ResponseContent);
            RSAPublicKey PubKey = new RSAPublicKey(RSAkey.PublicKeyMod, RSAkey.PublicKeyExp);

            byte[] bytearray = PubKey.modulus.ToByteArray();
            #region
            //string SearchWord = "data-miniprofile";
            //int DataIndex = Response.IndexOf("data-miniprofile") + SearchWord.Length + 2;
            //int DataEndIndex = Response.IndexOf("\"", DataIndex);
            //int IndexingLenght = DataEndIndex - DataIndex;
            //string TrimmedData = Response.Substring(DataIndex, IndexingLenght);
            #endregion
            int someint = 2;

            //Console.WriteLine(TrimmedData);
        }

        private static List<KeyValuePair<string, string>> CreateContent(string tadaa)
        {
            var Content = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>(PostParameters.Username, tadaa),
            new KeyValuePair<string, string>(PostParameters.DoNotCache, DateTime.UtcNow.ToUnixMilliSecondsTime().ToString()),
        };
        return Content;
        }
    }
}


internal class PostParameters
{
    internal const string DoNotCache = "donotcache";
    internal const string Username = "usernames";
}

internal static class DateExtention
{
    internal static long ToUnixMilliSecondsTime(this DateTime dateTimeInstance)
    {
        var duration = dateTimeInstance - new DateTime(1970, 1, 1, 0, 0, 0);
        return (long)(1000 * duration.TotalSeconds);
    }
}

internal class RSAKey
{
    [JsonProperty(PropertyName = "success")]
    internal bool Success { get; set; }
    [JsonProperty(PropertyName = "publickey_mod")]
    internal string PublicKeyMod { get; set; }
    [JsonProperty(PropertyName = "publickey_exp")]
    internal string PublicKeyExp { get; set; }
    [JsonProperty(PropertyName = "timestamp")]
    internal string Timestamp { get; set; }
    [JsonProperty(PropertyName = "token_gid")]
    internal string TokenGid { get; set; }
}

internal class RSAPublicKey
{
    public RSAPublicKey(string publickey_mod, string publickey_exp)
    {
        modulus = createBigInt(publickey_mod);
        encryptionExponent = createBigInt(publickey_exp);
    }
    public BigInteger modulus { get; private set; }
    public BigInteger encryptionExponent { get; private set; }

    private BigInteger createBigInt(string hex)
    {
        BigInteger result;
        result = BigInteger.Parse($"00{hex}", System.Globalization.NumberStyles.AllowHexSpecifier);
        return result;
    }
}