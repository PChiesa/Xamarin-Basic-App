using System;
using System.Linq;
using BasicApp.Database;
using BasicApp.Login.Models;
using BasicApp.Policies;
using BasicApp.Policies.Exceptions;

namespace BasicApp.Session
{
    public class SessionManager : ISessionManager
    {
        private User _loggedUser;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Voucher.Models.Voucher> _voucherRepository;

        public SessionManager(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public void ClearSession()
        {
            this._loggedUser = null;
        }

        public int GetUserId()
        {
            try
            {
                return this._loggedUser.Id;
            }
            catch (NullReferenceException)
            {

                var user = _userRepository.EnumerateAll().FirstOrDefault() ?? throw new EmptySessionException();
                this.StartSession(user);

                return this._loggedUser.Id;
            }
        }

        public string GetUserToken()
        {
            try
            {
                return this._loggedUser.Token;
            }
            catch (NullReferenceException)
            {
                var token = _userRepository.EnumerateAll().FirstOrDefault()?.Token ?? throw new EmptySessionException();
                return token;
            }

        }

        public void StartSession(User user)
        {
            this._loggedUser = user;
        }
    }
}
