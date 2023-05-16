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
    public class UsesController : Controller
    {
        private Entities db = new Entities();

        // GET: Uses
        public ActionResult Index()
        {
            var uses = db.Uses.Include(u => u.Product).Include(u => u.RawMaterial);
            return View(uses.ToList());
        }

        // GET: Uses/Details/5
        public ActionResult Details(int? pid, int? rid)
        {
            if (pid == null || rid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uses uses = db.Uses.Find(pid,rid);
            if (uses == null)
            {
                return HttpNotFound();
            }
            return View(uses);
        }

        // GET: Uses/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "ProductDescription");
            ViewBag.MaterialID = new SelectList(db.RawMaterial, "MaterialID", "MaterialName");
            return View();
        }

        // POST: Uses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UsesID,MaterialID,ProductID")] Uses uses)
        {
            if (ModelState.IsValid)
            {
                db.Uses.Add(uses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "ProductDescription", uses.ProductID);
            ViewBag.MaterialID = new SelectList(db.RawMaterial, "MaterialID", "MaterialName", uses.MaterialID);
            return View(uses);
        }

        // GET: Uses/Edit/5
        public ActionResult Edit(int? pid, int? rid)
        {
            if (pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uses uses = db.Uses.Find(pid, rid);
            if (uses == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "ProductDescription", uses.ProductID);
            ViewBag.MaterialID = new SelectList(db.RawMaterial, "MaterialID", "MaterialName", uses.MaterialID);
            return View(uses);
        }

        // POST: Uses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UsesID,MaterialID,ProductID")] Uses uses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "ProductDescription", uses.ProductID);
            ViewBag.MaterialID = new SelectList(db.RawMaterial, "MaterialID", "MaterialName", uses.MaterialID);
            return View(uses);
        }

        // GET: Uses/Delete/5
        public ActionResult Delete(int? pid, int? rid)
        {
            if (pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uses uses = db.Uses.Find(pid, rid);
            if (uses == null)
            {
                return HttpNotFound();
            }
            return View(uses);
        }

        // POST: Uses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? pid, int? rid)
        {
            Uses uses = db.Uses.Find(pid, rid);
            db.Uses.Remove(uses);
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
