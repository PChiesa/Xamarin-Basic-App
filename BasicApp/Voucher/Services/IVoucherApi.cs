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
        [Get("Voucher/GetEvents/{userId}")]
        Task<IEnumerable<Event>> GetEvents(int userId, [Header("Authorization")] string authorization);

        [Get("Voucher/GetVouchers/{eventId}/{userId}")]
        Task<IEnumerable<Models.Voucher>> GetVouchers(int userId, int eventId, [Header("Authorization")] string authorization);
    }
}
