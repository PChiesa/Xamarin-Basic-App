using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BasicApp.Voucher.Models;
using Refit;

namespace BasicApp.Voucher.Services
{
    [Headers("X-Requested-With: XMLHttpRequest", "Content-Type: application/json")]
    public interface IVoucherApi
    {
        [Get("/Voucher/GetEvents/{userId}")]
        Task<IEnumerable<Event>> GetEventsAsync(int userId, [Header("UserToken")] string authorization);

        [Get("/Voucher/GetVouchers/{eventId}/{userId}")]
        Task<IEnumerable<Models.Voucher>> GetVouchersAsync(int userId, int eventId, [Header("UserToken")] string authorization);

        [Post("/Voucher/RefreshVouchers")]
        Task<IEnumerable<Models.VoucherQuickUpdate>> RefreshVouchersAsync(IEnumerable<int> voucherIds, [Header("UserToken")] string authorization);
    }
}
