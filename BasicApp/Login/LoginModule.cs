using System;
using Autofac;
using BasicApp.Login.ViewModels;
using BasicApp.Login.Views;
using Prism.Autofac;
using Prism.Modularity;

namespace BasicApp.Login
{
    public class LoginModule : IModule
    {
        public LoginModule()
        {
        }

        public void Initialize()
        {
        }

        public static void Initialize(ContainerBuilder builder)
        {
            builder.RegisterTypeForNavigation<LoginPage, LoginViewModel>("Login");
        }
    }
}
