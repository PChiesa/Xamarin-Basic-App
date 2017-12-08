using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BasicApp.Policies;
using BasicApp.Session;
using BasicApp.Voucher.Models;
using Polly;
using Refit;

namespace BasicApp.Voucher.Services
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherApi _voucherApi;
        private readonly ISessionManager _sessionManager;
        private readonly IPolicyWrapper<IEnumerable<Event>> _policies;

        public VoucherService(ISessionManager sessionManager, IPolicyWrapper<IEnumerable<Event>> policies)
        {
            _voucherApi = RestService.For<IVoucherApi>(Constants.DEFAULT_API_ENDPOINT);
            _sessionManager = sessionManager;
            _policies = policies;
        }

        public async Task<IEnumerable<Event>> GetEvents()
        {
            var events = await _policies.GetPolicies().ExecuteAsync(async () =>
                {
                    return await _voucherApi.GetEvents(_sessionManager.GetUserId(), _sessionManager.GetUserToken());
                });


            return events;
        }

        public async Task<IEnumerable<Models.Voucher>> GetVouchers(int eventId)
        {
            return null;
            //return await _voucherApi.GetVouchers(_sessionManager.GetUserId(), eventId, _sessionManager.GetUserToken());
        }
    }
}
