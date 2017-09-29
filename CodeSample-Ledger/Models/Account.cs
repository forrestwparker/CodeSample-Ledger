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
        public int AccountId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        // There may be many transactions associated with each account.
        // Load these transactions only when necessary.
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
