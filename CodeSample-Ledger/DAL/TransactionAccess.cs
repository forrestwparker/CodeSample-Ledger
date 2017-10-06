using CodeSample_Ledger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSample_Ledger.DAL
{
    public static class TransactionAccess
    {
        // Makes a deposit of the given amount into the given acount.
        public static void Deposit(Account account, decimal amount)
        {
            using (var db = new LedgerContext())
            {
                var deposit = new Transaction();
                deposit.TransactionId = db.Transactions.Count();
                deposit.AccountId = account.accountId;
                deposit.DollarAmount = amount;
                deposit.Timestamp = DateTime.Now;
                deposit.Type = Transaction.TransactionType.Deposit;
                db.Transactions.Add(deposit);
                db.Accounts.Attach(account);
                account.balance += amount;
                db.SaveChanges();
            }
        }
    }
}
