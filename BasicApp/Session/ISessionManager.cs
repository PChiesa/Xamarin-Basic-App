using System;
using BasicApp.Login.Models;
using System.Threading.Tasks;

namespace BasicApp.Session
{
    public interface ISessionManager
    {
        int GetUserId();

        string GetUserToken();

        void ClearSession();

        Task StartSessionAsync(User user);
    }
}
