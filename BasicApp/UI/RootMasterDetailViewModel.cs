using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using BasicApp.UI.Services;
using Prism.Navigation;
using BasicApp.Session;
using BasicApp.Login.Models;
using System.Windows.Input;

namespace BasicApp.UI
{
    public class RootMasterDetailViewModel : BindableBase, INavigatedAware
    {

        private IUIServices _uiServices;
        public User User { get; private set; }
        public ICommand LogoutCommand { get; private set; }


        public RootMasterDetailViewModel(ISessionManager sessionManager, IUIServices uiServices)
        {
            this.User = sessionManager.GetUser();
            _uiServices = uiServices;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            LogoutCommand = _uiServices.GetCurrentViewModel().LogoutCommand;
        }
    }
}
