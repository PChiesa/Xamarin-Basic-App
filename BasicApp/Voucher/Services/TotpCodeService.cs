using System;
using System.Text;
using BasicApp.Totp;

namespace BasicApp.Voucher.Services
{
    public class TotpCodeService : ITotpCodeService
    {
        public string GenerateCode(string secretKey, TimeSpan offset = new TimeSpan())
        {
            var keyBytes = Encoding.ASCII.GetBytes(secretKey);

            var totp = new Totp.Totp(keyBytes, mode: OtpHashMode.Sha1, step: 30, totpSize: 8);
            return totp.ComputeTotp(DateTime.UtcNow.Add(offset));
        }
    }
}
