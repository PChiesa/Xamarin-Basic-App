using BasicApp.Login;
using Prism.Autofac;
using Xamarin.Forms;

namespace BasicApp
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("Login");
        }

        protected override void RegisterTypes()
        {
            LoginModule.Initialize(Builder);
        }
    }
}
