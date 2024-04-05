using System.Collections.Generic;
using Sources.Project.Data;

namespace Sources.Project.Managers {
    public interface IAccountManager {
        void Register(Account account, int slotId);

        Account GetPrimaryAccount();
        Account GetAccount(int slotId);
    }
    public sealed class AccountManager : IAccountManager {
        private Dictionary<int, Account> _accounts = new();

        public void Register(Account account, int slotId) {
            account.SlotId = slotId;
            _accounts.Add(slotId, account);
        }

        public Account GetPrimaryAccount() {
            return GetAccount(0);
        }

        public Account GetAccount(int slotId) {
            return _accounts[slotId];
        }
    }
}
