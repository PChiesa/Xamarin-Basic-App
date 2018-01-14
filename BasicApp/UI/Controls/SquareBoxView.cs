using System;

using Xamarin.Forms;

namespace BasicApp.UI.Controls
{
    public class SquareContentView : ContentView
    {
        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            base.LayoutChildren(x, y, width, width);
        }
    }
}

