using Xamarin.Forms;

namespace BasicApp.Login.Views
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            backButton.IsVisible = Device.RuntimePlatform == Device.iOS;
        }
    }
}

