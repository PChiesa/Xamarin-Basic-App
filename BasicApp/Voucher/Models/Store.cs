using System;
using BasicApp.Database;
using SQLite;

namespace BasicApp.Voucher.Models
{
    public class Store : IEntity
    {
        public Store()
        {
        }

        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
