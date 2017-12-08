using System;
using Prism.Services;
using Xamarin.Forms;

namespace BasicApp.UI.Services
{
    public interface IUIServices
    {
        Page GetCurrentPage();
        BaseViewModel GetCurrentViewModel();

        void SetCurrentPage(Page page);
        void SetCurrentViewModel(BaseViewModel viewModel);

        void ShowLoading(string message);
        void HideLoading();

        IPageDialogService GetPageDialogService();
    }
}
