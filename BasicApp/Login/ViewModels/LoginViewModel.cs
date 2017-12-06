using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Prism.Navigation;
using System.Threading.Tasks;

namespace BasicApp.Login.ViewModels
{
    public class LoginViewModel : BaseViewModel, INavigationAware
    {
        public LoginViewModel()
        {
            Title = "Hello";
        }

        public string Title { get; set; }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            TestBinding();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        void TestBinding()
        {
            ShowLoading("Carregando");
            Task.Run(() =>
            {
                Thread.Sleep(2000);
                HideLoading();
            });

        }
    }
}
