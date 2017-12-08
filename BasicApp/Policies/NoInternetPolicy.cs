using System;
using System.Threading;
using System.Threading.Tasks;
using BasicApp.Policies.Exceptions;
using BasicApp.UI.Services;
using Polly;

namespace BasicApp.Policies
{
    public class NoInternetPolicy : IPolicy
    {
        private readonly IUIServices _uiServices;

        public NoInternetPolicy(IUIServices uiServices)
        {
            _uiServices = uiServices;
        }

        /// <summary>
        /// No internet policy. Executes when no connection is available and retries 3 times with time between tries increasing from 1 to 3 seconds
        /// </summary>
        /// <returns>The policy.</returns>
        public Policy GetPolicy() => Policy
            .Handle<Exception>()
            .FallbackAsync(
                async (context) =>
                {

                    _uiServices.ShowLoading("No internet connection");
                    Task.Run(() =>
                    {
                        Thread.Sleep(2000);
                        _uiServices.HideLoading();
                    });
                });
    }
}
