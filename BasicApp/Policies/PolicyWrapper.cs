using System.Linq;
using System.Collections.Generic;
using BasicApp.UI.Services;
using Polly.Wrap;
using Polly;
using BasicApp.Policies.Exceptions;
using System;
using Prism.Navigation;
using Prism.Services;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;
using System.Net.Http;
using BasicApp.Voucher.Models;
using Refit;
using System.Net;

namespace BasicApp.Policies
{
    public class PolicyWrapper<T> : IPolicyWrapper<T> where T : class
    {
        private readonly IUIServices _uiServices;
        private readonly IPageDialogService _pageDialogService;

        private Policy<T> _noInternetPolicy;
        private Policy<T> _noInternetPolicyFallback;
        private Policy<T> _failedRequestPolicy;
        private Policy<T> _emptySessionPolicy;
        private Policy<T> _userNotFoundPolicy;



        public PolicyWrapper(IUIServices uiServices, IPageDialogService pageDialogService)
        {
            _uiServices = uiServices;
            _pageDialogService = pageDialogService;
            InitPolicies();
        }

        private void InitPolicies()
        {
            _noInternetPolicy = Policy<T>.Handle<NoInternetException>()
                                      .WaitAndRetryAsync(
                                          1,
                                          (retryCount) => TimeSpan.FromSeconds(retryCount * 3),
                                          (ex, timespan, retryCount, context) =>
                                          {
                                              _uiServices.ShowLoading($"Sem conexão com a internet, tentando outra vez");
                                          }
                                         );

            _noInternetPolicyFallback = Policy<T>.Handle<NoInternetException>()
                                           .FallbackAsync(
                                                async cancellationToken =>
                                                {
                                                    _uiServices.HideLoading();
                                                    Device.BeginInvokeOnMainThread(() => _pageDialogService.DisplayAlertAsync("Atenção", "Sem conexão com a internet, verifique sua conexão e tente novamente", "Fechar"));
                                                    return null;
                                                });

            _failedRequestPolicy = Policy<T>.Handle<HttpRequestException>()
                                            .Or<ApiException>()
                                            .FallbackAsync(
                                                 async cancellationToken =>
                                                 {
                                                     _uiServices.HideLoading();
                                                     return null;
                                                 },
                                                 async (c) =>
                                                 {
                                                     await Task.Run(() =>
                                                     {
                                                         if (c.Exception is HttpRequestException)
                                                             Device.BeginInvokeOnMainThread(() => _pageDialogService.DisplayAlertAsync("Atenção", "Erro durante a requisição, tente novamente", "Fechar"));


                                                         if (c.Exception is ApiException)
                                                         {
                                                             if ((c.Exception as ApiException).StatusCode == HttpStatusCode.InternalServerError)
                                                                 Device.BeginInvokeOnMainThread(() => _pageDialogService.DisplayAlertAsync("Atenção", "Erro durante a requisição, tente novamente", "Fechar"));

                                                             if ((c.Exception as ApiException).StatusCode == HttpStatusCode.Unauthorized)
                                                                 Device.BeginInvokeOnMainThread(() => _pageDialogService.DisplayAlertAsync("Atenção", "Credenciais inválidas, faça login novamente", "Fechar"));
                                                         }
                                                     });
                                                 });

            _userNotFoundPolicy = Policy<T>.Handle<UserNotFoundException>()
                                        .FallbackAsync(
                                            async cancellationToken =>
                                            {
                                                Device.BeginInvokeOnMainThread(() => _pageDialogService.DisplayAlertAsync("Atenção", "Login inválido", "Fechar"));
                                                return null;
                                            });

            _emptySessionPolicy = Policy<T>.Handle<EmptySessionException>()
                                        .FallbackAsync(
                                        async ct =>
                                        {
                                            await _pageDialogService.DisplayAlertAsync("Atenção", "Sua sessão expirou, efetue login novamente", "Fechar");
                                            await _uiServices.GetCurrentViewModel().navigationService.GoBackToRootAsync();
                                            await _uiServices.GetCurrentViewModel().navigationService.NavigateAsync("Login");
                                            return null;
                                        });
        }








        public PolicyWrap<T> GetPolicies() => _noInternetPolicyFallback
            .WrapAsync(_noInternetPolicy)
            .WrapAsync(_failedRequestPolicy)
            .WrapAsync(_emptySessionPolicy)
            .WrapAsync(_userNotFoundPolicy);
    }
}
