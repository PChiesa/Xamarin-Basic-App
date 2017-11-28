using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicApp.Login.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        public LoginViewModel()
        {
            Title = "Hello";
        }

        public string Title { get; set; }
    }
}
