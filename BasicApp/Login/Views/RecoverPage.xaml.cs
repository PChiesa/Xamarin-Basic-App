using Xamarin.Forms;

namespace BasicApp.Login.Views
{
    public partial class RecoverPage : ContentPage
    {
        public RecoverPage()
        {
            InitializeComponent();
            backButton.IsVisible = Device.RuntimePlatform == Device.iOS;
        }
    }
}

