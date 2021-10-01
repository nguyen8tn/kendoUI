using ASP.NET_Framework_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_Framework_WebApp.Repository
{
    public interface ITransactionRepository : IDisposable
    {
        IEnumerable<Transaction> GetTransactions();
        Transaction GetTransactionByID(int studentId);
        bool InsertTransaction(Transaction transaction);
        bool DeleteTransaction(int transactionID);
        bool UpdateTransaction(Transaction transaction);
        void Save();

    }
}