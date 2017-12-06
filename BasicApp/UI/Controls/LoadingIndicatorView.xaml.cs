using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BasicApp.UI.Controls
{
    public partial class LoadingIndicatorView : ContentView
    {
        public LoadingIndicatorView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty IsLoadingProperty = BindableProperty.Create("IsLoading", typeof(bool), typeof(ContentView), false);
        public static readonly BindableProperty LoadingMessageProperty = BindableProperty.Create("LoadingMessage", typeof(string), typeof(ContentView), "");

        public bool IsLoading
        {
            get => (bool)GetValue(IsLoadingProperty);
            set
            {
                SetValue(IsLoadingProperty, value);
                OnPropertyChanged("IsLoading");
            }
        }

        public bool LoadingMessage
        {
            get => (bool)GetValue(LoadingMessageProperty);
            set
            {
                SetValue(LoadingMessageProperty, value);
                OnPropertyChanged("LoadingMessage");
            }
        }

    }
}
