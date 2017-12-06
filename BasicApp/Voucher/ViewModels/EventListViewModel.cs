using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using BasicApp.Voucher.Services;
using System.Windows.Input;
using Prism.Navigation;

namespace BasicApp.Voucher.ViewModels
{
    public class EventListViewModel : BaseViewModel, INavigationAware
    {
        private readonly IVoucherService _voucherService;
        private readonly INavigationService _navigationService;

        public IEnumerable<Models.Event> Events { get; private set; }
        public ICommand FetchEventsCommand { get; private set; }
        public ICommand SelectEventCommand { get; private set; }

        public EventListViewModel(IVoucherService voucherService, INavigationService navigationService)
        {
            _voucherService = voucherService;
            _navigationService = navigationService;

            FetchEventsCommand = new DelegateCommand(async () =>
            {
                Events = await _voucherService.GetEvents();
            });

            SelectEventCommand = new DelegateCommand<Models.Event>(async (ev) =>
            {
                var navigationParams = new NavigationParameters();
                navigationParams.Add("Event", ev);
                await _navigationService.NavigateAsync("EventVoucherList", navigationParams);
            });
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            FetchEventsCommand.Execute(null);
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}
