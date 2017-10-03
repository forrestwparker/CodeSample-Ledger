using CodeSample_Ledger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSample_Ledger.DAL
{
    public static class AccountActions
    {
        public static bool Login(string username, string password, out Account account)
        {
            using (var db = new LedgerContext())
            {
                account = null;
                return false;
            }
        }

        private static bool UsernameAvailable(string username)
        {
            using (var db = new LedgerContext())
            {
                return db.Accounts.Where(x => x.username.Equals(username)).Count() == 0;
            }
        }
    }
}
