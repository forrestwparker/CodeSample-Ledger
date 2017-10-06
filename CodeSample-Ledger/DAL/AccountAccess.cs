using CodeSample_Ledger.Crypto;
using CodeSample_Ledger.Models;
using System.Linq;

namespace CodeSample_Ledger.DAL
{
    // Provides all functionality for account creation and verification in the database.
    public static class AccountAccess
    {
        // Attempts to grant access to an account with matching username and password.
        public static bool Grant(string username, string password, out Account account)
        {
            // Used to store account info from database temporarily.
            Account tempaccount;
            // Attempts to retrieve account with the provided username.
            using (var db = new LedgerContext())
            {
                tempaccount = db.Accounts.Where(x => x.username.Equals(username)).FirstOrDefault();
            }
            // If the account exists, attempts to verify password.
            // If password is correct, returns true and outs the account info.
            if (tempaccount != null && VerifyPassword(tempaccount, password))
            {
                account = tempaccount;
                return true;
            }
            // Otherwise, returns false and outs default(Account).
            account = default(Account);
            return false;
        }

        // Verifies the given password of the associated account.
        private static bool VerifyPassword(Account account, string password)
        {
            var hashing = new PasswordHasher(account.passwordSalt);
            return hashing.MakeHash(password).SequenceEqual(account.passwordHash);
        }

        // Determines if an account exists with the given username.
        public static bool CheckUsernameExists(string username)
        {
            using (var db = new LedgerContext())
            {
                return !(db.Accounts.
                         Where(x => x.username.Equals(username)).
                         FirstOrDefault() == null);
            }
        }

        // Makes a new account.
        public static void MakeAccount(string username, string password)
        {
            using (var db = new LedgerContext())
            {
                var account = new Account();
                var hashing = new PasswordHasher();
                account.accountId = db.Accounts.Count();
                account.username = username;
                account.passwordSalt = hashing.salt;
                account.passwordHash = hashing.MakeHash(password);
                account.balance = 0M;
                db.Accounts.Add(account);
                db.SaveChanges();
            }
        }
    }
}
