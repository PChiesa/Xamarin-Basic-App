using System;
namespace BasicApp.UI.PubSubEvents
{
    public class VoucherEventArgs : EventArgs
    {
        public Voucher.Models.Voucher Voucher { get; set; }

        public VoucherEventArgs(Voucher.Models.Voucher voucher)
        {
            this.Voucher = voucher;
        }
    }
}
