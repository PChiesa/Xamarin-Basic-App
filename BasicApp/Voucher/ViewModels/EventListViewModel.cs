using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using BasicApp.Voucher.Services;
using System.Windows.Input;
using Prism.Navigation;
using BasicApp.UI.Services;

namespace BasicApp.Voucher.ViewModels
{
    public class EventListViewModel : BaseViewModel
    {
        private readonly IVoucherService _voucherService;
        private readonly INavigationService _navigationService;

        public IEnumerable<Models.Event> Events { get; private set; }
        public ICommand GetEventsCommand { get; private set; }
        public ICommand SelectEventCommand { get; private set; }

        public EventListViewModel(IVoucherService voucherService, INavigationService navigationService, IUIServices uiServices) : base(uiServices)
        {
            _voucherService = voucherService;
            _navigationService = navigationService;

            GetEventsCommand = new DelegateCommand(GetEventsCommandAction);

            SelectEventCommand = new DelegateCommand<Models.Event>(SelectEventCommandAction);

        }

        private async void GetEventsCommandAction()
        {
            Events = await _voucherService.GetEvents() ?? new List<Models.Event>();
        }

        private async void SelectEventCommandAction(Models.Event ev)
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("Event", ev);
            await _navigationService.NavigateAsync("EventVoucherList", navigationParams);
        }


        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            GetEventsCommandAction();
        }


    }
}
