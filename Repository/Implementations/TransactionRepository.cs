using ASP.NET_Framework_WebApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ASP.NET_Framework_WebApp.Repository.Implementations
{
    public class TransactionRepository : ITransactionRepository
    {
        string cs;

        private AppDbContext context;
        public TransactionRepository(AppDbContext context)
        {
            this.context = context;
            cs = ConfigurationManager.ConnectionStrings["AppDbContext"].ConnectionString;
        }

        public Transaction GetTransactionByID(int studentId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            List<Transaction> list = new List<Transaction>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("sp_SelectTransaction", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    list.Add(new Transaction
                    {
                        TransactionID = Convert.ToInt32(rdr["TransactionID"]),
                        BankName = rdr["BankName"].ToString(),
                        AccountNumber = rdr["AccountNumber"].ToString(),
                        Amount = Convert.ToInt32(rdr["Amount"].ToString()),
                        BeneficiaryName = rdr["BeneficiaryName"].ToString(),
                        SWIFTCode = rdr["SWIFTCode"].ToString(),
                        Date = DateTime.Parse(rdr["Date"].ToString())
                    });
                }
            }
            return list;
        }

        public bool InsertTransaction(Transaction transaction)
        {
            bool check = false;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("sp_InsertTransaction", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BankName", transaction.BankName);
                com.Parameters.AddWithValue("@BeneficiaryName", transaction.BeneficiaryName);
                com.Parameters.AddWithValue("@AccountNumber", transaction.AccountNumber);
                com.Parameters.AddWithValue("@Amount", transaction.Amount);
                com.Parameters.AddWithValue("@SWIFTCode", transaction.SWIFTCode);
                com.Parameters.AddWithValue("@Date", transaction.Date);
                check = com.ExecuteNonQuery() > 0;
            }
            return check;
        }


        public bool UpdateTransaction(Transaction transaction)
        {
            bool check = false;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("sp_UpdateTransaction", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BankName", transaction.BankName);
                com.Parameters.AddWithValue("@BeneficiaryName", transaction.BeneficiaryName);
                com.Parameters.AddWithValue("@AccountNumber", transaction.AccountNumber);
                com.Parameters.AddWithValue("@Amount", transaction.Amount);
                com.Parameters.AddWithValue("@SWIFTCode", transaction.SWIFTCode);
                check = com.ExecuteNonQuery() > 0;
            }
            return check;
        }

        public bool DeleteTransaction(int transactionID)
        {
            bool check = false;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("sp_DeleteTransaction", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", transactionID);
                check = com.ExecuteNonQuery() > 0;
            }
            return check;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}