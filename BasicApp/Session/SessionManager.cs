using System;
using System.Linq;
using BasicApp.Database;
using BasicApp.Login.Models;
using BasicApp.Policies;
using BasicApp.Policies.Exceptions;
using System.Threading.Tasks;

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
                this._loggedUser = user;

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
                var user = _userRepository.EnumerateAll().FirstOrDefault() ?? throw new EmptySessionException();
                this._loggedUser = user;
                return this._loggedUser.Token;
            }

        }

        public async Task StartSessionAsync(User user)
        {
            this._loggedUser = user;

            await _userRepository.RemoveAllAsync();
            await _userRepository.AddAsync(user);

        }
    }
}
