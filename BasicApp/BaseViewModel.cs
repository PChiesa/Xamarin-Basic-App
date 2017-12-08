using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using BasicApp.UI.Services;
using System.Windows.Input;

namespace BasicApp
{
    public class BaseViewModel : BindableBase, INavigationAware
    {
        protected IUIServices uiServices;
        public INavigationService navigationService;
        public ICommand GoBackCommand { get; private set; }

        public BaseViewModel()
        {
        }

        public BaseViewModel(IUIServices uiServices)
        {
            this.uiServices = uiServices;
        }

        public BaseViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            this.GoBackCommand = new DelegateCommand(GoBackCommandAction);
        }

        public BaseViewModel(IUIServices uiServices, INavigationService navigationService)
        {
            this.uiServices = uiServices;
            this.navigationService = navigationService;
            this.GoBackCommand = new DelegateCommand(GoBackCommandAction);
        }

        protected async virtual void GoBackCommandAction()
        {
            await navigationService.GoBackAsync();
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {

        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
            uiServices.SetCurrentViewModel(this);
        }

        public bool IsLoading
        {
            get; set;
        }

        public string LoadingMessage
        {
            get; set;
        }
    }
}
