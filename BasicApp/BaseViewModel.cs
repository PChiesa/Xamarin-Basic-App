using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicApp
{
    public class BaseViewModel : BindableBase
    {
        protected void ShowLoading(string message)
        {
            IsLoading = true;
            LoadingMessage = message;
        }

        protected void HideLoading()
        {
            IsLoading = false;
            LoadingMessage = "";
        }

        public bool IsLoading
        {
            get;
            protected set;
        }

        public string LoadingMessage
        {
            get;
            protected set;
        }
    }
}
