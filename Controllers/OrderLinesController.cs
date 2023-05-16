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
    public class OrderLinesController : Controller
    {
        private Entities db = new Entities();

        // GET: OrderLines
        public ActionResult Index()
        {
            var orderLine = db.OrderLine.Include(o => o.Order).Include(o => o.Product);
            return View(orderLine.ToList());
        }

        // GET: OrderLines/Details/5
        public ActionResult Details(int? oid, int? pid)
        {
            if (oid == null || pid== null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderLine orderLine = db.OrderLine.Find(oid, pid);
            if (orderLine == null)
            {
                return HttpNotFound();
            }
            return View(orderLine);
        }

        // GET: OrderLines/Create
        public ActionResult Create()
        {
            ViewBag.OrderID = new SelectList(db.Order, "OrderID", "OrderID");
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "ProductDescription");
            return View();
        }

        // POST: OrderLines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,ProductID,OrderedQuantity")] OrderLine orderLine)
        {
            if (ModelState.IsValid)
            {
                db.OrderLine.Add(orderLine);
                db.SaveChanges();
                TempData["Message"] = "Order Line Added Successfully!!";
                return RedirectToAction("Index");
            }

            ViewBag.OrderID = new SelectList(db.Order, "OrderID", "OrderID", orderLine.OrderID);
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "ProductDescription", orderLine.ProductID);
            return View(orderLine);
        }

        // GET: OrderLines/Edit/5
        public ActionResult Edit(int? oid, int? pid)
        {
            if (oid == null || pid==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderLine orderLine = db.OrderLine.Find(oid, pid);
            if (orderLine == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderID = new SelectList(db.Order, "OrderID", "OrderID", orderLine.OrderID);
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "ProductDescription", orderLine.ProductID);
            return View(orderLine);
        }

        // POST: OrderLines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,ProductID,OrderedQuantity")] OrderLine orderLine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderLine).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Order Line Saved Successfully!!";
                return RedirectToAction("Index");
            }
            ViewBag.OrderID = new SelectList(db.Order, "OrderID", "OrderID", orderLine.OrderID);
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "ProductDescription", orderLine.ProductID);
            return View(orderLine);
        }

        // GET: OrderLines/Delete/5
        public ActionResult Delete(int? oid, int? pid)
        {
            if (oid == null || pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderLine orderLine = db.OrderLine.Find(oid, pid);
            if (orderLine == null)
            {
                return HttpNotFound();
            }
            return View(orderLine);
        }

        // POST: OrderLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? oid, int? pid)
        {
            OrderLine orderLine = db.OrderLine.Find(oid, pid);
            db.OrderLine.Remove(orderLine);
            db.SaveChanges();
            TempData["Message"] = "Order Line Deleted Successfully!!";
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
