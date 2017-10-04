using System;
using System.ComponentModel.DataAnnotations;

namespace CodeSample_Ledger.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        // Foreign Key for Account
        public int AccountId { get; set; }

        public DateTime Timestamp { get; set; }
        public TransactionType Type { get; set; }
        public decimal DollarAmount { get; set; }
        
        // Enum for tracking each transaction type
        public enum TransactionType { Deposit, Withdrawal }
    }
}
