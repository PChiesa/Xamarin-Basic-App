﻿using BasicApp.Login;
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

        protected override void RegisterTypes()/* See https://dansiegel.net/post/2017/08/02/breaking-changes-for-prism-autofac-users*/
        {
            LoginModule.Initialize(Builder);
        }
    }
}