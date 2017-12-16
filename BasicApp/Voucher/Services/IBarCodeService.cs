using System;
using Xamarin.Forms;

namespace BasicApp.Voucher.Services
{
    public interface IBarCodeService
    {
        ImageSource GenerateQrCode(string code, int width, int height, int margin = 0);
    }
}
