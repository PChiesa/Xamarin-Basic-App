using System;
using BasicApp.Database;

namespace BasicApp.Login.Models
{
    public class User : IEntity
    {
        public User()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
