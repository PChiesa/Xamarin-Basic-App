using System;
namespace BasicApp
{
    public class Constants
    {
#if DEBUG
        public const string DEFAULT_API_ENDPOINT = "http://localhost:3000/api";
#else
        public const string DEFAULT_API_ENDPOINT = "http://";
#endif
    }
}
