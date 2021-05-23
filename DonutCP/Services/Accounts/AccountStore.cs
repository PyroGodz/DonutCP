using DonutCP.Model;
using System;

namespace DonutCP.Services.Accounts
{
    public class AccountStore : IAccountStore
    {
        private Users _currentAccount;
        public Users CurrentAccount
        {
            get
            {
                return _currentAccount;
            }
            set
            {
                _currentAccount = value;
                StateChanged?.Invoke();
            }
        }

        public event Action StateChanged;
    }
}
