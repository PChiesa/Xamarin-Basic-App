using System;
using Autofac;
using BasicApp.Connectivity;
using BasicApp.Database;
using BasicApp.Login;
using BasicApp.Policies;
using BasicApp.Policies.Exceptions;
using BasicApp.Session;
using BasicApp.UI;
using BasicApp.UI.Services;
using BasicApp.Voucher;
using BasicApp.Voucher.Services;
using Prism.Autofac;
using Prism.Logging;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BasicApp
{
    public partial class App : PrismApplication
    {
        public App()
        {
            InitializeComponent();
        }

        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override async void OnInitialized()
        {
            //MainPage = new VoucherTestPage();

            try
            {
                if (Container.Resolve<ISessionManager>().GetUserId() > 0)
                    await NavigationService.NavigateAsync("RootNavigation/EventList");
            }
            catch (EmptySessionException)
            {
                await NavigationService.NavigateAsync("Login");
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

            Builder.RegisterTypeForNavigation<RootNavigationPage>("RootNavigation");
            Builder.RegisterType<SessionManager>().As<ISessionManager>().SingleInstance();
            Builder.RegisterType<UIServices>().As<IUIServices>().SingleInstance();
            Builder.RegisterType<ConnectivityService>().As<IConnectivityService>();
            Builder.RegisterType<TotpCodeService>().As<ITotpCodeService>();
            Builder.RegisterType<BarCodeService>().As<IBarCodeService>();

            Builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).SingleInstance();
            Builder.RegisterGeneric(typeof(PolicyWrapper<>)).As(typeof(IPolicyWrapper<>)).SingleInstance();

        }
    }
}
