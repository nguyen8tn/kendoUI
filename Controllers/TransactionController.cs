using ASP.NET_Framework_WebApp.Models;
using ASP.NET_Framework_WebApp.Repository;
using ASP.NET_Framework_WebApp.Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_Framework_WebApp.Controllers
{
    public class TransactionController : Controller
    {
        private ITransactionRepository transactionRepository;
        public TransactionController()
        {
            transactionRepository = new TransactionRepository(new Models.AppDbContext());
        }

        public TransactionController(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }

        // GET: Transaction
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertTransaction(Transaction transaction)
        {
            transactionRepository.InsertTransaction(transaction);
            Response.StatusCode = (int)HttpStatusCode.Created;
            return Json(new { Response.StatusCode, transaction }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateTransaction(Transaction transaction)
        {
            transactionRepository.UpdateTransaction(transaction);
            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new { Response.StatusCode, transaction }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteTransaction(int id)
        {
            transactionRepository.DeleteTransaction(id);
            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new { Response.StatusCode, id }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetList()
        {
            var data = transactionRepository.GetTransactions();
            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new { Response.StatusCode, data }, JsonRequestBehavior.AllowGet);
        }
    }
}