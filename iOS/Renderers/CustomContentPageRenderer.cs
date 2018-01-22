using System;
using BasicApp.iOS.Renderers;
using BasicApp.Login.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(CustomContentPageRenderer))]
namespace BasicApp.iOS.Renderers
{

    public class CustomContentPageRenderer : PageRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (e.NewElement is LoginPage || e.NewElement is RecoverPage || e.NewElement is RegisterPage)
                {
                    (e.NewElement as Page).Padding = new Thickness(0, 20, 0, 0);
                }

                e.NewElement.BackgroundColor = Color.FromHex("333");
            }

        }
    }
}
