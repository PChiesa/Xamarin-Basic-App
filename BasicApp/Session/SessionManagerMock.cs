using System;
using System.Threading.Tasks;
using BasicApp.Login.Models;
using Xamarin.Forms;

namespace BasicApp.Session
{
    public class SessionManagerMock : ISessionManager
    {
        public void ClearSession()
        {

        }

        public User GetUser()
        {
            return new User
            {
                ClientUserId = "1",
                Cpf = "12312312312",
                Email = "teste@teste.com",
                Id = 1,
                Name = "Teste",
                LastName = "LastName",
                Token = "12345"
            };
        }

        public int GetUserId()
        {
            return 1;
        }

        public string GetUserToken()
        {
            return "12345";
        }

        public Task StartSessionAsync(User user)
        {
            return Task.CompletedTask;
        }
    }
}

