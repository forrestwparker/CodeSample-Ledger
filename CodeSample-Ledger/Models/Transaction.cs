using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSample_Ledger.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        // Foreign Key for Account
        public int AccountId { get; set; }

        public DateTime Timestamp { get; set; }
        public double DollarAmount { get; set; }
        public TransactionType Type { get; set; }
        
        // Enum for tracking each transaction type
        public enum TransactionType { Deposit, Withdrawal }
    }
}
