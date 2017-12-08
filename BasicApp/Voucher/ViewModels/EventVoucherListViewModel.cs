using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using System.Windows.Input;
using BasicApp.Voucher.Services;
using BasicApp.UI.Services;

namespace BasicApp.Voucher.ViewModels
{
    public class EventVoucherListViewModel : BaseViewModel
    {
        private readonly IVoucherService _voucherService;
        private readonly INavigationService _navigationService;

        public Models.Event Event { get; private set; }
        public ICommand FetchEventVoucherListCommand { get; private set; }


        public EventVoucherListViewModel(IVoucherService voucherService, INavigationService navigationService, IUIServices uiServices) : base(uiServices)
        {
            _voucherService = voucherService;
            _navigationService = navigationService;

            FetchEventVoucherListCommand = new DelegateCommand(async () =>
            {
                Event.VoucherList = await _voucherService.GetVouchers(Event.Id);
            });
        }



        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Event = parameters.GetValue<Models.Event>("Event");
        }
    }
}
