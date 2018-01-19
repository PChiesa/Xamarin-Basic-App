using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using BasicApp.Database;
using Foundation;
using Prism.Autofac;
using UIKit;

namespace BasicApp.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            // Code for starting up the Xamarin Test Cloud Agent
            //#if DEBUG
            //			Xamarin.Calabash.Start();
            //#endif

            /* Set NavigationPage Title Color */
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes
            {
                TextColor = UIColor.White
            });

            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }

    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(ContainerBuilder container)
        {
            container.RegisterType<SQLite_iOS>().As<ISQLite>();
        }
    }

}
