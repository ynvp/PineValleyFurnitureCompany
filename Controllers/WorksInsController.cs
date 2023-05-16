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
    public class WorksInsController : Controller
    {
        private Entities db = new Entities();

        // GET: WorksIns
        public ActionResult Index()
        {
            var worksIn = db.WorksIn.Include(w => w.Employee).Include(w => w.WorkCenter);
            return View(worksIn.ToList());
        }

        // GET: WorksIns/Details/5
        public ActionResult Details(int? eid, int? wid)
        {
            if (eid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorksIn worksIn = db.WorksIn.Find(eid, wid);
            if (worksIn == null)
            {
                return HttpNotFound();
            }
            return View(worksIn);
        }

        // GET: WorksIns/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.Employee, "EmployeeID", "EmployeeName");
            ViewBag.WorkCenterID = new SelectList(db.WorkCenter, "WorkCenterID", "WorkCenterLocation");
            return View();
        }

        // POST: WorksIns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorksInID,EmployeeID,WorkCenterID")] WorksIn worksIn)
        {
            if (ModelState.IsValid)
            {
                db.WorksIn.Add(worksIn);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Employee, "EmployeeID", "EmployeeName", worksIn.EmployeeID);
            ViewBag.WorkCenterID = new SelectList(db.WorkCenter, "WorkCenterID", "WorkCenterLocation", worksIn.WorkCenterID);
            return View(worksIn);
        }

        // GET: WorksIns/Edit/5
        public ActionResult Edit(int? eid, int? wid)
        {
            if (eid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorksIn worksIn = db.WorksIn.Find(eid, wid);
            if (worksIn == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employee, "EmployeeID", "EmployeeName", worksIn.EmployeeID);
            ViewBag.WorkCenterID = new SelectList(db.WorkCenter, "WorkCenterID", "WorkCenterLocation", worksIn.WorkCenterID);
            return View(worksIn);
        }

        // POST: WorksIns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorksInID,EmployeeID,WorkCenterID")] WorksIn worksIn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(worksIn).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Employee, "EmployeeID", "EmployeeName", worksIn.EmployeeID);
            ViewBag.WorkCenterID = new SelectList(db.WorkCenter, "WorkCenterID", "WorkCenterLocation", worksIn.WorkCenterID);
            return View(worksIn);
        }

        // GET: WorksIns/Delete/5
        public ActionResult Delete(int? eid, int? wid)
        {
            if (eid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorksIn worksIn = db.WorksIn.Find(eid, wid);
            if (worksIn == null)
            {
                return HttpNotFound();
            }
            return View(worksIn);
        }

        // POST: WorksIns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? eid, int? wid)
        {
            WorksIn worksIn = db.WorksIn.Find(eid, wid);
            db.WorksIn.Remove(worksIn);
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
