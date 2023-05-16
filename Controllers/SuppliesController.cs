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
    public class SuppliesController : Controller
    {
        private Entities db = new Entities();

        // GET: Supplies
        public ActionResult Index()
        {
            var supplies = db.Supplies.Include(s => s.RawMaterial).Include(s => s.Vendor);
            return View(supplies.ToList());
        }

        // GET: Supplies/Details/5
        public ActionResult Details(int? vid, int? mid)
        {
            if (vid == null || mid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplies supplies = db.Supplies.Find(vid, mid);
            if (supplies == null)
            {
                return HttpNotFound();
            }
            return View(supplies);
        }

        // GET: Supplies/Create
        public ActionResult Create()
        {
            ViewBag.MaterialID = new SelectList(db.RawMaterial, "MaterialID", "MaterialName");
            ViewBag.VendorID = new SelectList(db.Vendor, "VendorID", "VendorName");
            return View();
        }

        // POST: Supplies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VendorID,MaterialID,SupplyUnitPrice")] Supplies supplies)
        {
            if (ModelState.IsValid)
            {
                db.Supplies.Add(supplies);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaterialID = new SelectList(db.RawMaterial, "MaterialID", "MaterialName", supplies.MaterialID);
            ViewBag.VendorID = new SelectList(db.Vendor, "VendorID", "VendorName", supplies.VendorID);
            return View(supplies);
        }

        // GET: Supplies/Edit/5
        public ActionResult Edit(int? vid, int? mid)
        {
            if (vid == null || mid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplies supplies = db.Supplies.Find(vid, mid);
            if (supplies == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaterialID = new SelectList(db.RawMaterial, "MaterialID", "MaterialName", supplies.MaterialID);
            ViewBag.VendorID = new SelectList(db.Vendor, "VendorID", "VendorName", supplies.VendorID);
            return View(supplies);
        }

        // POST: Supplies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VendorID,MaterialID,SupplyUnitPrice")] Supplies supplies)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplies).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaterialID = new SelectList(db.RawMaterial, "MaterialID", "MaterialName", supplies.MaterialID);
            ViewBag.VendorID = new SelectList(db.Vendor, "VendorID", "VendorName", supplies.VendorID);
            return View(supplies);
        }

        // GET: Supplies/Delete/5
        public ActionResult Delete(int? vid, int? mid)
        {
            if (vid == null || mid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplies supplies = db.Supplies.Find(vid, mid);
            if (supplies == null)
            {
                return HttpNotFound();
            }
            return View(supplies);
        }

        // POST: Supplies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int vid, int mid)
        {
            Supplies supplies = db.Supplies.Find(vid, mid, mid);
            db.Supplies.Remove(supplies);
            db.SaveChanges();
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
