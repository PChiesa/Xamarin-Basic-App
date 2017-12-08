using System;
using Prism.Services;
using Xamarin.Forms;

namespace BasicApp.UI.Services
{
    public class UIServices : IUIServices
    {
        private Page _currentPage;
        private BaseViewModel _currentViewModel;
        private readonly IPageDialogService _pageDialogService;

        public UIServices(IPageDialogService dialogService)
        {
            _pageDialogService = dialogService;
        }

        public Page GetCurrentPage() => _currentPage;

        public BaseViewModel GetCurrentViewModel() => _currentViewModel;

        public void SetCurrentPage(Page page)
        {
            _currentPage = page;
        }

        public void SetCurrentViewModel(BaseViewModel viewModel)
        {
            _currentViewModel = viewModel;
        }

        public void ShowLoading(string message)
        {
            _currentViewModel.IsLoading = true;
            _currentViewModel.LoadingMessage = message;
        }

        public void HideLoading()
        {
            _currentViewModel.IsLoading = false;
            _currentViewModel.LoadingMessage = "";
        }

        public IPageDialogService GetPageDialogService() => _pageDialogService;

    }
}
