using System;
using BasicApp.Login.Models;

namespace BasicApp.Session
{
    public interface ISessionManager
    {
        int GetUserId();

        string GetUserToken();

        void ClearSession();

        void StartSession(User user);
    }
}
