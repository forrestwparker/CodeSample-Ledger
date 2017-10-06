using CodeSample_Ledger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CodeSample_Ledger.Models.Transaction;

namespace CodeSample_Ledger.DAL
{
    public static class TransactionAccess
    {
        // Makes a transaction.
        public static void MakeTransaction(Account account, decimal amount, TransactionType transactionType)
        {
            using (var db = new LedgerContext())
            {
                var deposit = new Transaction();
                deposit.transactionId = db.Transactions.Count();
                deposit.accountId = account.accountId;
                deposit.amount = amount;
                deposit.type = transactionType;
                db.Transactions.Add(deposit);
                db.Accounts.Attach(account);
                switch (transactionType)
                {
                    case TransactionType.Deposit:
                        account.balance += amount;
                        break;
                    case TransactionType.Withdrawal:
                        account.balance -= amount;
                        break;
                    default:
                        break;
                }
                db.SaveChanges();
            }
        }

        // Gets account transactions.
        public static List<Transaction> GetTransactions(Account account)
        {
            using (var db = new LedgerContext())
            {
                return db.Transactions.
                       Where(x => x.accountId == account.accountId).
                       OrderByDescending(x => x.transactionId).
                       ToList();
            }
        }
    }
}
