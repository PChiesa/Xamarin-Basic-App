using System;

using Xamarin.Forms;

namespace BasicApp.Voucher.Services
{
    public interface ITotpCodeService
    {
        string GenerateCode(string secretKey, TimeSpan offset = new TimeSpan());
    }
}

