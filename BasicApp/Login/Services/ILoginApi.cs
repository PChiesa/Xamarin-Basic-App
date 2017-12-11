using System;
using System.Threading.Tasks;
using BasicApp.Login.Models;
using Refit;

namespace BasicApp.Login.Services
{
    [Headers("X-Requested-With: XMLHttpRequest", "Content-Type: application/json")]
    public interface ILoginApi
    {
        [Post("/Login/LogUser")]
        Task<User> LogUserAsync([Body] LoginCredentials login);

        [Get("/Login/RecoverPassword/{email}")]
        Task RecoverPasswordAsync(string email);

        [Post("/Login/RegisterUser")]
        Task<User> RegisterUserAsync([Body] User user);
    }
}
