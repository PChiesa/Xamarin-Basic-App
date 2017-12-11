using System;
using BasicApp.Database;
using SQLite;

namespace BasicApp.Voucher.Models
{
    public class Voucher : IEntity
    {
        public Voucher()
        {
        }

        [PrimaryKey]
        public int Id { get; set; }

        public int EventId { get; set; }

        public int UserId { get; set; }

        public string Description1 { get; set; }

        public string Description2 { get; set; }

        public string Description3 { get; set; }

        public string Token { get; set; }
    }
}
