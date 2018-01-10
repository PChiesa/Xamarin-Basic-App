using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using BasicApp.UI.Services;
using Prism.Navigation;
using BasicApp.Login.Services;
using System.Windows.Input;
using Prism.Services;
using Xamarin.Forms;

namespace BasicApp.Login.ViewModels
{
    public class RecoverViewModel : BaseViewModel
    {
        private readonly ILoginService _loginService;

        public string Email { get; set; }
        public ICommand RecoverCommand { get; private set; }
        public ICommand GoToSiteCommand { get; private set; }

        public RecoverViewModel(ILoginService loginService, INavigationService navigationService, IUIServices uiServices) : base(uiServices, navigationService)
        {
            _loginService = loginService;
            RecoverCommand = new DelegateCommand(RecoverCommandAction);
            GoToSiteCommand = new DelegateCommand(() => Device.OpenUri(new Uri("https://www.voucherseguro.com/voucherseguro/customer/account/login/")));
        }

        private async void RecoverCommandAction()
        {
            await _loginService.RecoverPasswordAsync(Email);
        }
    }
}
