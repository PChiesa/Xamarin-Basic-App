using System;
using BasicApp.Login.Models;
using BasicApp.Policies;
using BasicApp.Policies.Exceptions;

namespace BasicApp.Session
{
    public class SessionManager : ISessionManager
    {
        private User _loggedUser;
        private readonly IPolicyWrapper<User> _policies;

        public SessionManager(IPolicyWrapper<User> policies)
        {
            _policies = policies;
        }

        public void ClearSession()
        {
            this._loggedUser = null;
        }

        public int GetUserId()
        {
            return 1;
            //return _policies.GetVoidPolicies().Execute(() =>
            //{
            //    if (this._loggedUser == null)
            //        throw new EmptySessionException();
            //    if (this._loggedUser.Id == 0)
            //        throw new EmptySessionException();

            //    return this._loggedUser.Id;
            //});
        }

        public string GetUserToken()
        {
            return "";
            //return _policies.GetVoidPolicies().Execute(() =>
            //{
            //    if (this._loggedUser == null)
            //        throw new EmptySessionException();
            //    if (String.IsNullOrEmpty(this._loggedUser.Token) || String.IsNullOrWhiteSpace(this._loggedUser.Token))
            //        throw new EmptySessionException();

            //    return this._loggedUser.Token;
            //});
        }

        public void StartSession(User user)
        {
            this._loggedUser = user;
        }
    }
}
