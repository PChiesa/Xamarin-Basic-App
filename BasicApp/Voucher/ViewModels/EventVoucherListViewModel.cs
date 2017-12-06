using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using System.Windows.Input;
using BasicApp.Voucher.Services;

namespace BasicApp.Voucher.ViewModels
{
    public class EventVoucherListViewModel : BaseViewModel, INavigationAware
    {
        private readonly IVoucherService _voucherService;
        private readonly INavigationService _navigationService;

        public Models.Event Event { get; private set; }
        public ICommand FetchEventVoucherListCommand { get; private set; }


        public EventVoucherListViewModel(IVoucherService voucherService, INavigationService navigationService)
        {
            _voucherService = voucherService;
            _navigationService = navigationService;

            FetchEventVoucherListCommand = new DelegateCommand(async () =>
            {
                Event.VoucherList = await _voucherService.GetVouchers(Event.Id);
            });
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            Event = parameters.GetValue<Models.Event>("Event");
        }
    }
}
