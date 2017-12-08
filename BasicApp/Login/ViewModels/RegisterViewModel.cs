using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using BasicApp.UI.Services;
using BasicApp.Login.Services;
using System.Windows.Input;
using BasicApp.Login.Models;
using Prism.Navigation;

namespace BasicApp.Login.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly ILoginService _loginService;

        public User User { get; set; }
        public ICommand RegisterCommand { get; private set; }

        public RegisterViewModel(ILoginService loginService, INavigationService navigationService, IUIServices uiServices) : base(uiServices, navigationService)
        {
            _loginService = loginService;
            RegisterCommand = new DelegateCommand(RegisterCommandAction);
        }

        private async void RegisterCommandAction()
        {
            await _loginService.RegisterUserAsync(User);
            await navigationService.NavigateAsync("EventList");
        }
    }
}
