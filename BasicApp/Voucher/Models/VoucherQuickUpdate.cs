using BasicApp.Voucher.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicApp.Voucher.Models
{
    public class VoucherQuickUpdate
    {
        public int Id { get; set; }
        public VoucherStatus CurrentStatus { get; set; }
        public DateTime? EntryDate { get; set; }
        public string Gate { get; set; }
    }
}
