using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BasicApp.Policies;
using BasicApp.Session;
using BasicApp.UI.Services;
using BasicApp.Voucher.Models;
using Polly;
using Refit;

namespace BasicApp.Voucher.Services
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherApi _voucherApi;
        private readonly ISessionManager _sessionManager;
        private readonly IPolicyWrapper<IEnumerable<Event>> _eventPolicies;
        private readonly IPolicyWrapper<IEnumerable<Models.Voucher>> _voucherPolicies;
        private readonly IUIServices _uiServices;

        public VoucherService(ISessionManager sessionManager, IPolicyWrapper<IEnumerable<Event>> eventPolicies, IPolicyWrapper<IEnumerable<Models.Voucher>> voucherPolicies, IUIServices uiServices)
        {
            _voucherApi = RestService.For<IVoucherApi>(Constants.DEFAULT_API_ENDPOINT);
            _sessionManager = sessionManager;
            _eventPolicies = eventPolicies;
            _voucherPolicies = voucherPolicies;
            _uiServices = uiServices;
        }

        public async Task<IEnumerable<Event>> GetEvents()
        {
            _uiServices.ShowLoading("Carregando seus Eventos, aguarde");

            var events = await
                _eventPolicies.GetPolicies().ExecuteAsync(async () =>
                {
                    return await _voucherApi.GetEventsAsync(_sessionManager.GetUserId(), _sessionManager.GetUserToken());
                });

            _uiServices.HideLoading();

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
    }
}
