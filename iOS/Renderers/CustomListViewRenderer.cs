using System;
using BasicApp.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ListView), typeof(CustomListViewRenderer))]
namespace BasicApp.iOS.Renderers
{
    public class CustomListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                e.NewElement.BackgroundColor = Color.Transparent;
            }
        }
    }
}
