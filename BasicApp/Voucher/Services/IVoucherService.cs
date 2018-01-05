using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BasicApp.Voucher.Models;

namespace BasicApp.Voucher.Services
{
    public interface IVoucherService
    {        
        Task<IEnumerable<Event>> GetEvents(bool updateFromServer);
        Task<IEnumerable<Models.Voucher>> GetVouchers(int eventId);
    }
}
