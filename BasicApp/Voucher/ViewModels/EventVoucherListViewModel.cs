using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using System.Windows.Input;
using BasicApp.Voucher.Services;
using BasicApp.UI.Services;
using BasicApp.Voucher.Models;
using Prism.Events;
using BasicApp.UI.PubSubEvents;
using BasicApp.Database;

namespace BasicApp.Voucher.ViewModels
{
    public class EventVoucherListViewModel : BaseViewModel
    {
        const int QR_CODE_WIDTH = 200;
        const int QR_CODE_HEIGHT = 200;

        private readonly IVoucherService _voucherService;
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IBarCodeService _barCodeService;
        private readonly ITotpCodeService _totpCodeService;
        private readonly IBaseRepository<Models.Voucher> _voucherRepository;

        public Models.Event Event { get; private set; }
        public ICommand GetEventVoucherListCommand { get; private set; }


        public EventVoucherListViewModel(IBaseRepository<Models.Voucher> voucherRepository, IVoucherService voucherService, INavigationService navigationService, IEventAggregator eventAggregator, IBarCodeService barCodeService, ITotpCodeService totpCodeService, IUIServices uiServices) : base(uiServices, navigationService)
        {
            _voucherService = voucherService;
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            _barCodeService = barCodeService;
            _totpCodeService = totpCodeService;
            _voucherRepository = voucherRepository;

            GetEventVoucherListCommand = new DelegateCommand(GetEventVoucherListCommandAction);
        }

        private async void GetEventVoucherListCommandAction()
        {
            Event.VoucherList = await _voucherService.GetVouchers(Event.Id);

            if (Event.VoucherList != null)
            {
                await _voucherRepository.RemoveAllAsync();
                await _voucherRepository.AddAllAsync(Event.VoucherList);
            }


            RaisePropertyChanged("Event");
        }

        void RefreshQrCode(VoucherEventArgs args)
        {
            var voucher = args.Voucher;
            var token = _totpCodeService.GenerateCode(voucher.Token);

            voucher.QrCode = _barCodeService.GenerateQrCode("9" + voucher.Id.ToString("D7") + token, QR_CODE_WIDTH, QR_CODE_HEIGHT);
        }


        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Event = parameters.GetValue<Models.Event>("Event");

            //TODO: Move this logic to somewhere else
            //Event.VoucherList = await _voucherRepository.GetListByPredicateAsync(voucher => voucher.EventId == Event.Id);
            //if (!Event.VoucherList.Any())
            //    GetEventVoucherListCommandAction();
            //else
                //RaisePropertyChanged("Event");

            _eventAggregator.GetEvent<QrCodeRefreshEvent>().Subscribe(RefreshQrCode);
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
            _eventAggregator.GetEvent<QrCodeRefreshEvent>().Unsubscribe(RefreshQrCode);

        }
    }
}
