using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeSample_Ledger.Models
{
    public class Account
    {
        [Key]
        public int accountId { get; set; }
        public string username { get; set; }
        public byte[] passwordSalt { get; set; }
        public byte[] passwordHash { get; set; }
        public decimal balance { get; set; }

        // There may be many transactions associated with each account.
        // Load these transactions only when necessary.
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
