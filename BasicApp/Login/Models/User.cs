using System;
using BasicApp.Database;
using SQLite;

namespace BasicApp.Login.Models
{
    [Table("User")]
    public class User : IEntity
    {
        public User()
        {
        }

        [PrimaryKey]
        public int Id { get; set; }
        public string ClientUserId { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        [Ignore]
        public string Password { get; set; }
        [Ignore]
        public string PasswordConfirmation { get; set; }
    }
}
