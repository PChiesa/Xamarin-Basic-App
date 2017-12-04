using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BasicApp.Session;
using BasicApp.Voucher.Models;

namespace BasicApp.Voucher.Services
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherApi _voucherApi;
        private readonly ISessionManager _sessionManager;

        public VoucherService(IVoucherApi voucherApi, ISessionManager sessionManager)
        {
            _voucherApi = voucherApi;
            _sessionManager = sessionManager;
        }

        public async Task<IEnumerable<Event>> GetEvents()
        {
            return await _voucherApi.GetEvents(_sessionManager.GetUserId(), _sessionManager.GetUserToken());
        }

        public async Task<IEnumerable<Models.Voucher>> GetVouchers(int eventId)
        {
            return await _voucherApi.GetVouchers(_sessionManager.GetUserId(), eventId, _sessionManager.GetUserToken());
        }
    }
}
