using System;
using Autofac;
using BasicApp.Connectivity;
using BasicApp.Database;
using BasicApp.Login;
using BasicApp.Policies;
using BasicApp.Session;
using BasicApp.UI.Services;
using BasicApp.Voucher;
using Prism.Autofac;
using Prism.Logging;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BasicApp
{
    public partial class App : PrismApplication
    {
        //public App()
        //{
        //    InitializeComponent();
        //}

        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override async void OnInitialized()
        {
            try
            {
                if (Container.Resolve<ISessionManager>().GetUserId() == 0)
                    await NavigationService.NavigateAsync("Login");
                else
                    await NavigationService.NavigateAsync("EventList");
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), Category.Exception, Priority.High);
            }

        }

        protected override void RegisterTypes()/* See https://dansiegel.net/post/2017/08/02/breaking-changes-for-prism-autofac-users*/
        {
            LoginModule.Initialize(Builder);
            VoucherModule.Initialize(Builder);
            Builder.RegisterType<SessionManager>().As<ISessionManager>().SingleInstance();
            Builder.RegisterType<UIServices>().As<IUIServices>().SingleInstance();
            Builder.RegisterType<ConnectivityService>().As<IConnectivityService>();
            Builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).SingleInstance();
            Builder.RegisterGeneric(typeof(PolicyWrapper<>)).As(typeof(IPolicyWrapper<>)).SingleInstance();
        }
    }
}
