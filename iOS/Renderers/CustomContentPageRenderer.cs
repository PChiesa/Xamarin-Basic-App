using System;
using BasicApp.iOS.Renderers;
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

            if (e.OldElement != null || Element == null)
            {
                return;
            }

            try
            {
                e.NewElement.BackgroundColor = Color.FromHex("333");
            }
            catch (Exception)
            {
            }
        }
    }
}
