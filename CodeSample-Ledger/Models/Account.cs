using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeSample_Ledger.Models
{
    public class Account
    {
        [Key]
        public int accountId { get; set; }
        public string username { get; set; }
        public string passwordSalt { get; set; }
        public string passwordHash { get; set; }
        public decimal balance { get; set; }

        // There may be many transactions associated with each account.
        // Load these transactions only when necessary.
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
