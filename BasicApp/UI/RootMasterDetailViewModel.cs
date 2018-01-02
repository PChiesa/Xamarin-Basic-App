using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using BasicApp.UI.Services;
using Prism.Navigation;
using BasicApp.Session;
using BasicApp.Login.Models;

namespace BasicApp.UI
{
    public class RootMasterDetailViewModel : BaseViewModel
    {
        private readonly ISessionManager _sessionManager;

        public User User { get; private set; }

        public RootMasterDetailViewModel(ISessionManager sessionManager, IUIServices uiServices, INavigationService navigationService) : base(uiServices, navigationService)
        {
            _sessionManager = sessionManager;
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            this.User = _sessionManager.GetUser();
        }
    }
}
