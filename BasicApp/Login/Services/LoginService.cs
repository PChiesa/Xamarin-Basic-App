using System;
using System.Threading.Tasks;
using BasicApp.Connectivity;
using BasicApp.Database;
using BasicApp.Login.Models;
using BasicApp.Policies;
using BasicApp.Policies.Exceptions;
using BasicApp.Session;
using BasicApp.UI.Services;
using Refit;
using System.Net.Http;

namespace BasicApp.Login.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginApi _api;
        private readonly IPolicyWrapper<User> _policies;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IConnectivityService _connectivityService;
        private readonly IUIServices _uiServices;
        private readonly ISessionManager _sessionManager;


        public LoginService(ISessionManager sessionManager, IUIServices uiServices, IPolicyWrapper<User> policies, IBaseRepository<User> userRepository, IConnectivityService connectivityService)
        {
            _sessionManager = sessionManager;
            _uiServices = uiServices;
            _policies = policies;
            _userRepository = userRepository;
            _connectivityService = connectivityService;
            _api = RestService.For<ILoginApi>(Constants.DEFAULT_API_ENDPOINT);
        }

        public async Task<User> LogUserAsync(LoginCredentials login)
        {
            _uiServices.ShowLoading("Login em andamento, aguarde...");

            return await _policies.GetPolicies().ExecuteAsync(async () =>
            {
                if (!_connectivityService.IsConnected()) throw new NoInternetException();

                var user = await _api.LogUserAsync(login);
                _uiServices.HideLoading();
                if (user == null)
                    throw new UserNotFoundException();

                await _userRepository.AddAsync(user);

                _sessionManager.StartSession(user);
                _uiServices.HideLoading();

                return user;

            });
        }

        public async Task RecoverPasswordAsync(string email)
        {
            _uiServices.ShowLoading("Recuperando sua senha, aguarde...");

            await _policies.GetPolicies().ExecuteAsync(async () =>
            {
                if (!_connectivityService.IsConnected()) throw new NoInternetException();

                await _api.RecoverPasswordAsync(email);
                _uiServices.HideLoading();
                await _uiServices
                    .GetPageDialogService()
                    .DisplayAlertAsync("Sucesso", "Sua nova senha foi enviada para o e-mail cadastrado", "Fechar");

                return null;
            });
        }

        public async Task RegisterUserAsync(User user)
        {
            _uiServices.ShowLoading("Criando usuário, aguarde...");

            await _policies.GetPolicies().ExecuteAsync(async () =>
            {
                if (!_connectivityService.IsConnected()) throw new NoInternetException();

                await _api.RegisterUserAsync(user);
                _uiServices.HideLoading();
                await _uiServices
                    .GetPageDialogService()
                    .DisplayAlertAsync("Sucesso", "Usuário criado", "Fechar");
                return null;
            });


        }
    }
}
