using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EBill.Models;
using EBill.Repository;

namespace EBill.Controllers
{
    public class EBillController : Controller
    {
        // GET: EBill
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(BillDetail details)
        {
            Data data = new Data(); 
            data.SaveBillDetails(details);
            ModelState.Clear();
            return View();
        }
    }
}