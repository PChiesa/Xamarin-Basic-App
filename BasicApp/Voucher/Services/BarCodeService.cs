using System;
using BasicApp.Policies.Exceptions;
using Xamarin.Forms;
using ZXing;
using ZXing.Common;
using ZXing.Mobile;

namespace BasicApp.Voucher.Services
{
    public class BarCodeService : IBarCodeService
    {
        public ImageSource GenerateQrCode(string code, int width, int height, int margin = 0)
        {
            try
            {
                var barcodeWriter = new BarcodeWriter
                {
                    Format = BarcodeFormat.QR_CODE,
                    Options = new EncodingOptions
                    {
                        Width = width,
                        Height = height,
                        Margin = margin,
                    }
                };


                var barcode = barcodeWriter.Write(code);

#if __IOS__
                return ImageSource.FromStream(() => barcode.AsJPEG((nfloat)0.5).AsStream());
#endif

#if __ANDROID__

                QrCode = ImageSource.FromStream(() =>
                {
                    var ms = new MemoryStream();
                    barcode.Compress(Android.Graphics.Bitmap.CompressFormat.Jpeg, 100, ms);

                    ms.Seek(0L, SeekOrigin.Begin);

                    return ms;
                });
#endif
            }
            catch (Exception ex)
            {
                throw new QrCodeException(ex.Message);
            }
        }
    }
}
