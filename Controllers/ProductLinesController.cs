using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using S3G11_PVFAPP.Models;
using EntityState = System.Data.Entity.EntityState;

namespace S3G11_PVFAPP.Controllers
{
    public class ProductLinesController : Controller
    {
        private Entities db = new Entities();

        // GET: ProductLines
        public ActionResult Index()
        {
            return View(db.ProductLine.ToList());
        }

        // GET: ProductLines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductLine productLine = db.ProductLine.Find(id);
            if (productLine == null)
            {
                return HttpNotFound();
            }
            return View(productLine);
        }

        // GET: ProductLines/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductLines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductLineID,ProductLineName")] ProductLine productLine)
        {
            if (ModelState.IsValid)
            {
                db.ProductLine.Add(productLine);
                db.SaveChanges();
                TempData["Message"] = "Product Line Added Successfully!!";
                return RedirectToAction("Index");
            }

            return View(productLine);
        }

        // GET: ProductLines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductLine productLine = db.ProductLine.Find(id);
            if (productLine == null)
            {
                return HttpNotFound();
            }
            return View(productLine);
        }

        // POST: ProductLines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductLineID,ProductLineName")] ProductLine productLine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productLine).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Product Lines Saved Successfully!!";
                return RedirectToAction("Index");
            }
            return View(productLine);
        }

        // GET: ProductLines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductLine productLine = db.ProductLine.Find(id);
            if (productLine == null)
            {
                return HttpNotFound();
            }
            return View(productLine);
        }

        // POST: ProductLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductLine productLine = db.ProductLine.Find(id);
            db.ProductLine.Remove(productLine);
            db.SaveChanges();
            TempData["Message"] = "Product Line Deleted Successfully!!";
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
