using CodeSample_Ledger.Models;
using System;
using System.Data.Common;
using System.Data.Entity;

namespace CodeSample_Ledger
{
    public class LedgerContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        
        // This is the global connection of the database in local memory
        private static DbConnection DbConn = Effort.DbConnectionFactory.CreateTransient();

        // All DbContexts must use the static DbConnection
        public LedgerContext() : this(DbConn) { }
        private LedgerContext(DbConnection connection) : base(connection, true) { }
    }
}