using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using BasicApp.Voucher.Services;
using System.Windows.Input;
using Prism.Navigation;
using BasicApp.UI.Services;
using BasicApp.Database;
using BasicApp.Voucher.Models;

namespace BasicApp.Voucher.ViewModels
{
    public class EventListViewModel : BaseViewModel
    {
        private readonly IVoucherService _voucherService;
        private readonly INavigationService _navigationService;

        private readonly IBaseRepository<Event> _eventRepository;
        private readonly IBaseRepository<Store> _storeRepository;

        public IEnumerable<Models.Event> Events { get; private set; }
        public ICommand GetEventsCommand { get; private set; }
        public ICommand GetEventsFromServerCommand { get; private set; }
        public ICommand SelectEventCommand { get; private set; }

        public EventListViewModel(IBaseRepository<Event> eventRepository, IBaseRepository<Store> storeRepository, IVoucherService voucherService, INavigationService navigationService, IUIServices uiServices) : base(uiServices, navigationService)
        {
            _voucherService = voucherService;
            _navigationService = navigationService;
            _eventRepository = eventRepository;
            _storeRepository = storeRepository;

            GetEventsCommand = new DelegateCommand(() => GetEventsCommandAction(false));
            GetEventsFromServerCommand = new DelegateCommand(() => GetEventsCommandAction(true));

            SelectEventCommand = new DelegateCommand<Models.Event>(SelectEventCommandAction);

        }


        private async void GetEventsCommandAction(bool updateFromServer)
        {
            var events = await _voucherService.GetEvents(updateFromServer);

            if (events == null)
                Events = new List<Event>();
            else
                Events = events;
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
            //TODO: Improve this logic
            if (Events == null)
                GetEventsCommandAction(false);
        }
    }
}
