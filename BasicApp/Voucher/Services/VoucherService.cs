using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BasicApp.Policies;
using BasicApp.Session;
using BasicApp.UI.Services;
using BasicApp.Voucher.Models;
using Polly;
using Refit;
using BasicApp.Database;
using BasicApp.Connectivity;
using System.Linq;

namespace BasicApp.Voucher.Services
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherApi _voucherApi;
        private readonly ISessionManager _sessionManager;
        private readonly IConnectivityService _connectivityService;
        private readonly IPolicyWrapper<IEnumerable<Event>> _eventPolicies;
        private readonly IPolicyWrapper<IEnumerable<Models.Voucher>> _voucherPolicies;
        private readonly IUIServices _uiServices;
        private readonly IBaseRepository<Event> _eventRepository;
        private readonly IBaseRepository<Models.Voucher> _voucherRepository;

        public VoucherService(ISessionManager sessionManager, IConnectivityService connectivityService, IPolicyWrapper<IEnumerable<Event>> eventPolicies, IPolicyWrapper<IEnumerable<Models.Voucher>> voucherPolicies, IUIServices uiServices, IBaseRepository<Event> eventRepository, IBaseRepository<Models.Voucher> voucherRepository)
        {
            _voucherApi = RestService.For<IVoucherApi>(Constants.DEFAULT_API_ENDPOINT);
            _sessionManager = sessionManager;
            _eventPolicies = eventPolicies;
            _voucherPolicies = voucherPolicies;
            _uiServices = uiServices;
            _eventRepository = eventRepository;
            _connectivityService = connectivityService;
            _voucherRepository = voucherRepository;
        }

        public async Task<IEnumerable<Event>> GetEvents(bool updateFromServer = true)
        {
            if (!_connectivityService.IsConnected())
            {
                updateFromServer = false;
            }


            /*Get events from local DB*/
            if (!updateFromServer)
            {
                var eventsLocal = await _eventRepository.EnumerateAllAsync();

                eventsLocal.ForEach(e =>
                {
                    e.Vouchers = _voucherRepository.GetListByPredicate(v => v.EventId == e.Id);
                });

                if (eventsLocal != null)
                    return eventsLocal;
            }


            /*Get events from server*/
            _uiServices.ShowLoading("Carregando seus Eventos, aguarde");

            var events = await
                _eventPolicies.GetPolicies().ExecuteAsync(async () =>
                {
                    return await _voucherApi.GetEventsAsync(_sessionManager.GetUserId(), _sessionManager.GetUserToken());
                });

            _uiServices.HideLoading();

            /*Removing existing events from local DB*/
            await _eventRepository.RemoveAllAsync();

            if (events != null)
                await _eventRepository.AddAllAsync(events);/*Adding the new events to local DB*/

            var vouchers = events.SelectMany(e => e.Vouchers);
            await _voucherRepository.RemoveAllAsync();

            if (vouchers != null)
                await _voucherRepository.AddAllAsync(vouchers);

            return events;
        }

        public async Task<IEnumerable<Models.Voucher>> GetVouchers(int eventId)
        {
            _uiServices.ShowLoading("Carregando VoucherSeguro®, aguarde");

            var vouchers = await
                _voucherPolicies.GetPolicies().ExecuteAsync(async () =>
                {
                    return await _voucherApi.GetVouchersAsync(_sessionManager.GetUserId(), eventId, _sessionManager.GetUserToken());
                });

            _uiServices.HideLoading();

            return vouchers;
        }

        public async Task<IEnumerable<VoucherQuickUpdate>> RefreshVouchers(IEnumerable<int> voucherIds)
        {
            if (!_connectivityService.IsConnected())
            {
                return null;
            }


            try
            {
                return await _voucherApi.RefreshVouchersAsync(voucherIds, _sessionManager.GetUserToken());
            }
            catch (Exception)
            {
                /*Do not warn the user about this exception, it's an backgroud operation that should not annoy the user with error messages*/
                return null;
            }
        }
    }
}
