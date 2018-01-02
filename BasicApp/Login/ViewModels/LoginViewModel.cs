using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Prism.Navigation;
using System.Threading.Tasks;
using BasicApp.UI.Services;
using System.Windows.Input;
using BasicApp.Login.Services;
using BasicApp.Session;
using BasicApp.Login.Models;
using Prism.Services;

namespace BasicApp.Login.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly ILoginService _loginService;
        private readonly ISessionManager _sessionManager;
        private readonly IPageDialogService _pageDialogService;

        public ICommand LoginCommand { get; private set; }
        public ICommand NavigateToRecoverCommand { get; private set; }
        public ICommand NavigateToRegisterCommand { get; private set; }

        public LoginCredentials Login { get; set; }
        public string Title { get; set; }

        public LoginViewModel(INavigationService navigationService, ILoginService loginService, ISessionManager sessionManager, IPageDialogService pageDialogService, IUIServices uiServices) : base(uiServices, navigationService)
        {
            _loginService = loginService;
            _sessionManager = sessionManager;
            _pageDialogService = pageDialogService;

            Title = "Hello";
            LoginCommand = new DelegateCommand(LoginCommandAction);
            NavigateToRecoverCommand = new DelegateCommand(async () => await navigationService.NavigateAsync("Recover"));
            NavigateToRegisterCommand = new DelegateCommand(async () => await navigationService.NavigateAsync("Register"));
        }


        private async void LoginCommandAction()
        {
            var user = await _loginService.LogUserAsync(Login);
            if (user != null)
                await navigationService.NavigateAsync("RootMasterDetail/RootNavigation/EventList");
        }


        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            Login = new LoginCredentials();
        }
    }
}
