using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Prism;
using Prism.Autofac;
using Autofac;
using BasicApp.Database;
using Android.Graphics;
using Xamarin.Forms;

namespace BasicApp.Droid
{
    [Activity(Label = "VoucherSeguro.com", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            /* Prevent screenshots */
            Window.SetFlags(WindowManagerFlags.Secure, WindowManagerFlags.Secure);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App(new AndroidInitializer()));
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(ContainerBuilder container)
        {
            container.RegisterType<SQLite_Android>().As<ISQLite>();
        }
    }
}
