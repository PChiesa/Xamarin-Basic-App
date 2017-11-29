using System;
using BasicApp.Policies.Exceptions;
using Polly;

namespace BasicApp.Policies
{
    public class NoInternetPolicy : IPolicy
    {
        public NoInternetPolicy()
        {
        }

        /// <summary>
        /// No internet policy. Executes when no connection is available and retries 3 times with time between tries increasing from 1 to 3 seconds
        /// </summary>
        /// <returns>The policy.</returns>
        public Policy GetPolicy() => Policy
            .Handle<NoInternetException>()
            .WaitAndRetry(
                new[] { TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(3) },
                (exception, timeSpan, retryCount, context) =>
                {

                });
    }
}
