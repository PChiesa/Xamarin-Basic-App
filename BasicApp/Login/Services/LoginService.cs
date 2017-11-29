using System;
using System.Threading.Tasks;
using BasicApp.Login.Models;
using Refit;

namespace BasicApp.Login.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginApi _api;

        public LoginService()
        {
            _api = RestService.For<ILoginApi>(Constants.DEFAULT_API_ENDPOINT);
        }

        public async Task<User> LogUserAsync(LoginCredentials login)
        {
            return await _api.LogUserAsync(login);
        }

        public async Task RecoverPasswordAsync(string email)
        {
            await _api.RecoverPasswordAsync(email);
        }

        public async Task<User> RegisterUserAsync(User user)
        {
            return await _api.RegisterUserAsync(user);
        }
    }
}
