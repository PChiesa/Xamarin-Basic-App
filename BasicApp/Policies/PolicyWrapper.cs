﻿using System.Linq;
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
                                                             var api = (c.Exception as ApiException);

                                                             if (api.StatusCode == HttpStatusCode.InternalServerError)
                                                                 Device.BeginInvokeOnMainThread(() => _pageDialogService.DisplayAlertAsync("Atenção", "Erro durante a requisição, tente novamente", "Fechar"));

                                                             if (api.StatusCode == HttpStatusCode.Unauthorized)
                                                                 Device.BeginInvokeOnMainThread(() => _pageDialogService.DisplayAlertAsync("Atenção", "Credenciais inválidas, faça login novamente", "Fechar"));

                                                             if (api.StatusCode == HttpStatusCode.NotFound)
                                                             {

                                                                 if (!string.IsNullOrEmpty(api.Content))
                                                                     Device.BeginInvokeOnMainThread(() => _pageDialogService.DisplayAlertAsync("Atenção", api.Content, "Fechar"));
                                                                 else
                                                                     Device.BeginInvokeOnMainThread(() => _pageDialogService.DisplayAlertAsync("Atenção", "Recurso não encontrado, tente novamente", "Fechar"));
                                                             }

                                                             if(api.StatusCode == HttpStatusCode.UpgradeRequired)
                                                             {
                                                                 Device.BeginInvokeOnMainThread(() => _pageDialogService.DisplayAlertAsync("Atenção", "Atualize seu app para a versão mais recente e tente novamente", "Fechar"));
                                                             }
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
                                            await _pageDialogService.DisplayAlertAsync("Atenção", "Credenciais não encontradas, efetue login novamente", "Fechar");
                                            try
                                            {
                                                await _uiServices.GetCurrentViewModel().navigationService.GoBackToRootAsync();
                                            }
                                            catch (Exception)
                                            {
                                            }

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
