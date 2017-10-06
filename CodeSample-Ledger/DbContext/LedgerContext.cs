using CodeSample_Ledger.Models;
using System;
using System.Data.Common;
using System.Data.Entity;

namespace CodeSample_Ledger
{
    // Context for the Ledger database.
    public class LedgerContext : DbContext
    {
        //
        // Class constructors
        //

        // All DbContexts must use the static DbConnection.
        public LedgerContext() : this(DbConn) { }
        private LedgerContext(DbConnection connection) : base(connection, true) { }

        // This code sample uses the Effort.EF6 NuGet package to simulate a real database.
        // This class property creates the "real" database, but would not exist in a real project.
        // Unit tests would use their own temporary databases.
        public static DbConnection DbConn = Effort.DbConnectionFactory.CreatePersistent("RealDB");

        //
        // DbSets
        //

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}