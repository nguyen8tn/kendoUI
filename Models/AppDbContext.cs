using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ASP.NET_Framework_WebApp.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext() : base("AppDbContext")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Transaction> Transactions;
    }
}