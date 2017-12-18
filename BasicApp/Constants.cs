using System;
namespace BasicApp
{
    public class Constants
    {
#if DEBUG && __ANDROID__
        public const string DEFAULT_API_ENDPOINT = "http://10.0.2.2:55556/api";
#if DEBUG && __IOS__
        public const string DEFAULT_API_ENDPOINT = "http://localhost:55556/api";
#endif
#else
        public const string DEFAULT_API_ENDPOINT = "http://";
#endif
    }
}
