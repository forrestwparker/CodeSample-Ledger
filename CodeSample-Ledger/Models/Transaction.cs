using System;
using System.ComponentModel.DataAnnotations;

namespace CodeSample_Ledger.Models
{
    public class Transaction
    {
        [Key]
        public int transactionId { get; set; }

        // Foreign Key for Account
        public int accountId { get; set; }

        public TransactionType type { get; set; }
        public decimal amount { get; set; }
        
        // Enum for tracking each transaction type
        public enum TransactionType { Deposit, Withdrawal }
    }
}
