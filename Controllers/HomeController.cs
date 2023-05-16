using S3G11_PVFAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S3G11_PVFAPP.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();
        public ActionResult Index()
        {
            var products = db.Product;
            return View(products);
        }

        public ActionResult About()
        {
            ViewBag.Message = "";

            return View();
        }
    }
}