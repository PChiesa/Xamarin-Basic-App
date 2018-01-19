﻿using System;
using BasicApp.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomNavigationPageRenderer))]
namespace BasicApp.iOS.Renderers
{
    public class CustomNavigationPageRenderer : NavigationRenderer
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.NavigationBar.BarTintColor = Color.FromHex("79AB4E").ToUIColor();

            this.NavigationBar.TopItem.LeftBarButtonItem.TintColor = UIColor.White;
            this.NavigationBar.TopItem.RightBarButtonItem.TintColor = UIColor.White;
        }
    }
}
