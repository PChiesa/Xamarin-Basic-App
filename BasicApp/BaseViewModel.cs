using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using BasicApp.UI.Services;
using System.Windows.Input;
using Xamarin.Forms;
using Prism.Autofac;
using Prism.Events;
using Autofac;
using BasicApp.UI.PubSubEvents;

namespace BasicApp
{
    public class BaseViewModel : BindableBase, INavigationAware
    {
        protected IUIServices uiServices;
        public INavigationService navigationService;
        public ICommand GoBackCommand { get; private set; }
        public ICommand LogoutCommand { get; private set; }

        //public BaseViewModel()
        //{
        //    this.GoBackCommand = new DelegateCommand(GoBackCommandAction);
        //    this.LogoutCommand = new DelegateCommand(LogoutCommandAction);
        //}

        //public BaseViewModel(IUIServices uiServices)
        //{
        //    this.uiServices = uiServices;
        //    this.GoBackCommand = new DelegateCommand(GoBackCommandAction);
        //    this.LogoutCommand = new DelegateCommand(LogoutCommandAction);
        //}

        public BaseViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            this.GoBackCommand = new DelegateCommand(GoBackCommandAction);
            this.LogoutCommand = new DelegateCommand(LogoutCommandAction);
        }

        public BaseViewModel(IUIServices uiServices, INavigationService navigationService)
        {
            this.uiServices = uiServices;
            this.navigationService = navigationService;
            this.GoBackCommand = new DelegateCommand(GoBackCommandAction);
            this.LogoutCommand = new DelegateCommand(LogoutCommandAction);
        }

        protected async virtual void LogoutCommandAction()
        {
            await navigationService.GoBackToRootAsync();
            await navigationService.NavigateAsync("Login", useModalNavigation: true);
            var eventAggregator = (Application.Current as PrismApplication).Container.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<LogoutEvent>().Publish();
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
