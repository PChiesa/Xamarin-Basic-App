using System;
using System.Collections.Generic;

namespace BasicApp.Voucher.Models
{
    public class Event
    {
        public Event()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public string Description3 { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }

        public IEnumerable<Voucher> VoucherList { get; set; }
    }
}
