using System;
using System.Collections.Generic;
using System.Text;

namespace SteamRoller
{
    class SteamURLs
    {
        public object SteamCommunity = new { };
        {
            public const string Base = "https://steamcommunity.com/",
            public const string Login = Base + "login/",
        };

        public SteamStore SteamStore = new SteamStore();
    }

    public class SteamStore
    {
        public const string Base = "https://store.steampowered.com/";
        public const string Login = Base + "login/";
    }
}
