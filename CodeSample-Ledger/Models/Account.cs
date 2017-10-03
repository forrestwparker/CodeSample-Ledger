using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSample_Ledger.Models
{
    public class Account
    {
        [Key]
        public int accountId { get; set; }
        public string username { get; set; }
        public string passwordHash { get; set; }
        public string passwordSalt { get; set; }

        // There may be many transactions associated with each account.
        // Load these transactions only when necessary.
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
