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
    public class ProducedInsController : Controller
    {
        private Entities db = new Entities();

        // GET: ProducedIns
        public ActionResult Index()
        {
            var producedIn = db.ProducedIn.Include(p => p.Product).Include(p => p.WorkCenter);
            return View(producedIn.ToList());
        }

        // GET: ProducedIns/Details/5
        public ActionResult Details(int? wcid, int? pid)
        {
            if (wcid == null || pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProducedIn producedIn = db.ProducedIn.Find(wcid, pid);
            if (producedIn == null)
            {
                return HttpNotFound();
            }
            return View(producedIn);
        }

        // GET: ProducedIns/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "ProductDescription");
            ViewBag.WorkCenterID = new SelectList(db.WorkCenter, "WorkCenterID", "WorkCenterLocation");
            return View();
        }

        // POST: ProducedIns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProducedInID,WorkCenterID,ProductID")] ProducedIn producedIn)
        {
            if (ModelState.IsValid)
            {
                db.ProducedIn.Add(producedIn);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "ProductDescription", producedIn.ProductID);
            ViewBag.WorkCenterID = new SelectList(db.WorkCenter, "WorkCenterID", "WorkCenterLocation", producedIn.WorkCenterID);
            return View(producedIn);
        }

        // GET: ProducedIns/Edit/5
        public ActionResult Edit(int? wcid, int? pid)
        {
            if (wcid==null || pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProducedIn producedIn = db.ProducedIn.Find(wcid, pid);
            if (producedIn == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "ProductDescription", producedIn.ProductID);
            ViewBag.WorkCenterID = new SelectList(db.WorkCenter, "WorkCenterID", "WorkCenterLocation", producedIn.WorkCenterID);
            return View(producedIn);
        }

        // POST: ProducedIns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProducedInID,WorkCenterID,ProductID")] ProducedIn producedIn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producedIn).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "ProductDescription", producedIn.ProductID);
            ViewBag.WorkCenterID = new SelectList(db.WorkCenter, "WorkCenterID", "WorkCenterLocation", producedIn.WorkCenterID);
            return View(producedIn);
        }

        // GET: ProducedIns/Delete/5
        public ActionResult Delete(int? wcid, int? pid)
        {
            if (wcid == null || pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProducedIn producedIn = db.ProducedIn.Find(wcid, pid);
            if (producedIn == null)
            {
                return HttpNotFound();
            }
            return View(producedIn);
        }

        // POST: ProducedIns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? wcid, int? pid)
        {
            ProducedIn producedIn = db.ProducedIn.Find(wcid, pid);
            db.ProducedIn.Remove(producedIn);
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
