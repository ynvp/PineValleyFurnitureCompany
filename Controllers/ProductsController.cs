using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using S3G11_PVFAPP.Models;
using EntityState = System.Data.Entity.EntityState;

namespace S3G11_PVFAPP.Controllers
{
    public class ProductsController : Controller
    {
        private Entities db = new Entities();

        // GET: Products
        public ActionResult Index()
        {
            var product = db.Product.Include(p => p.ProductLine);
            return View(product.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.ProductLineID = new SelectList(db.ProductLine, "ProductLineID", "ProductLineName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
            string extension = Path.GetExtension(product.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            product.ProductImage = "~/ProductImages/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/ProductImages/"), fileName);
            product.ImageFile.SaveAs(fileName);
            using (Entities db = new Entities())
            {
                db.Product.Add(product);
                db.SaveChanges();
                TempData["Message"] = "Product Added Successfully!!";
                return RedirectToAction("Index");
            }
            //ModelState.Clear();

            ViewBag.ProductLineID = new SelectList(db.ProductLine, "ProductLineID", "ProductLineName", product.ProductLineID);

            return View(product);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ProductID,ProductDescription,ProductFinish,ProductStandardPrice,ProductLineID,ProductImage")] Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Product.Add(product);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ProductLineID = new SelectList(db.ProductLine, "ProductLineID", "ProductLineName", product.ProductLineID);
        //    return View(product);
        //}

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductLineID = new SelectList(db.ProductLine, "ProductLineID", "ProductLineName", product.ProductLineID);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
            string extension = Path.GetExtension(product.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            product.ProductImage = "~/ProductImages/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/ProductImages/"), fileName);
            product.ImageFile.SaveAs(fileName);
            using (Entities db = new Entities())
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Product Saved Successfully!!";

                return RedirectToAction("Index");
            }
            ModelState.Clear();
            ViewBag.ProductLineID = new SelectList(db.ProductLine, "ProductLineID", "ProductLineName", product.ProductLineID);

            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ProductID,ProductDescription,ProductFinish,ProductStandardPrice,ProductLineID,ProductImage")] Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(product).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ProductLineID = new SelectList(db.ProductLine, "ProductLineID", "ProductLineName", product.ProductLineID);
        //    return View(product);
        //}

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();
            TempData["Message"] = "Product Deleted Successfully!!";

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
