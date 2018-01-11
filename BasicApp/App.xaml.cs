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
using Xamarin.Forms;
using BasicApp.UI.Converters;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BasicApp
{
    public partial class App : PrismApplication
    {
        public App()
        {
            InitializeComponent();
        }

        public App(IPlatformInitializer initializer = null) : base(initializer)
        {

        }

        protected override async void OnInitialized()
        {
            Resources = new ResourceDictionary();
            ApplyGlobalStyles();
            ApplyGlobalConverters();

            try
            {
                if (Container.Resolve<ISessionManager>().GetUserId() > 0)
                    await NavigationService.NavigateAsync("RootMasterDetail/RootNavigation/EventList");
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

            Builder.RegisterTypeForNavigation<RootMasterDetailPage, RootMasterDetailViewModel>("RootMasterDetail");
            Builder.RegisterTypeForNavigation<RootNavigationPage>("RootNavigation");
            Builder.RegisterType<SessionManager>().As<ISessionManager>().SingleInstance();
            Builder.RegisterType<UIServices>().As<IUIServices>().SingleInstance();
            Builder.RegisterType<ConnectivityService>().As<IConnectivityService>();
            Builder.RegisterType<TotpCodeService>().As<ITotpCodeService>();
            Builder.RegisterType<BarCodeService>().As<IBarCodeService>();

            Builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).SingleInstance();
            Builder.RegisterGeneric(typeof(PolicyWrapper<>)).As(typeof(IPolicyWrapper<>)).SingleInstance();

        }

        private void ApplyGlobalConverters()
        {
            Resources.Add("lengthToBoolConverter", new LengthToBoolConverter());
            Resources.Add("reverseBoolConverter", new ReverseBoolConverter());
        }

        private void ApplyGlobalStyles()
        {


#if __IOS__
            const string rezRegular = "";
            const string montSerratLight = "";
            const string montSerratRegular = "";
            const string montSerratMedium = "";
            const string montSerratBold = "";
#endif
#if __ANDROID__
            const string rezRegular = "Fonts/REZ.ttf#REZ-Regular";
            const string montSerratLight = "Fonts/MontserratLight.ttf#Montserrat-Light";
            const string montSerratRegular = "Fonts/MontserratRegular.ttf#Montserrat-Regular";
            const string montSerratMedium = "Fonts/MontserratMedium.ttf#Montserrat-Medium";
            const string montSerratBold = "Fonts/MontserratBold.ttf#Montserrat-Bold";
#endif

            var labelFontBold = new Style(typeof(Label))
            {
                Setters = {
                    new Setter { Property = Label.FontFamilyProperty, Value = montSerratBold}
                }
            };

            var labelFontMedium = new Style(typeof(Label))
            {
                Setters = {
                    new Setter { Property = Label.FontFamilyProperty, Value = montSerratMedium}
                }
            };

            var labelFontRez = new Style(typeof(Label))
            {
                Setters = {
                    new Setter { Property = Label.FontFamilyProperty, Value = rezRegular}
                }
            };

            var buttonStyle = new Style(typeof(Button))
            {
                Setters = {
                    new Setter { Property = Button.FontFamilyProperty, Value = montSerratMedium}
                }
            };

            var labelStyle = new Style(typeof(Label))
            {
                Setters = {
                    new Setter { Property = Button.FontFamilyProperty, Value = montSerratRegular},
                    new Setter { Property = Label.TextColorProperty, Value = Color.White}
                }
            };

            var buttonPrimary = new Style(typeof(Button))
            {
                Setters = {
                    new Setter { Property = Button.BackgroundColorProperty, Value = Color.FromHex("79AB4E")}
                }
            };

            var entryStyle = new Style(typeof(Entry))
            {
                Setters = {
                    new Setter { Property = Entry.FontFamilyProperty, Value = montSerratRegular}
                }
            };

            Resources.Add(buttonStyle);
            Resources.Add(labelStyle);
            Resources.Add(entryStyle);
            Resources.Add("labelFontBold", labelFontBold);
            Resources.Add("labelFontMedium", labelFontMedium);
            Resources.Add("labelFontRez", labelFontRez);
            Resources.Add("buttonPrimary", buttonPrimary);
        }
    }
}
