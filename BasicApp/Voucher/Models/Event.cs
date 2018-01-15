using System;
using System.Collections.Generic;
using BasicApp.Database;
using SQLite;

namespace BasicApp.Voucher.Models
{
    public class Event : IEntity
    {
        public Event()
        {
        }

        [PrimaryKey]
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public string Description3 { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }

        [Ignore]
        public string GetEventDate { get => Date.ToString("dd/MM/yyyy H:mm"); }

        [Ignore]
        public Store Store { get; set; }

        [Ignore]
        public IEnumerable<Voucher> Vouchers { get; set; }
    }
}
