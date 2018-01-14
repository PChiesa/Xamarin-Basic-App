﻿using System;
namespace BasicApp
{
    public class Constants
    {
#if DEBUG && __ANDROID__
        //public const string DEFAULT_API_ENDPOINT = "http://10.0.2.2:55556/api";        
        public const string DEFAULT_API_ENDPOINT = "https://vsapi-hml.azurewebsites.net/api";
#endif
#if DEBUG && __IOS__
        public const string DEFAULT_API_ENDPOINT = "http://localhost:55556/api";
#endif
#if !DEBUG
        public const string DEFAULT_API_ENDPOINT = "https://vsapi.azurewebsites.net/api";
#endif

    }
}
