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
    public class SalesTerritoriesController : Controller
    {
        private Entities db = new Entities();

        // GET: SalesTerritories
        public ActionResult Index()
        {
            return View(db.SalesTerritory.ToList());
        }

        // GET: SalesTerritories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesTerritory salesTerritory = db.SalesTerritory.Find(id);
            if (salesTerritory == null)
            {
                return HttpNotFound();
            }
            return View(salesTerritory);
        }

        // GET: SalesTerritories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalesTerritories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TerritoryID,TerritoryName")] SalesTerritory salesTerritory)
        {
            if (ModelState.IsValid)
            {
                db.SalesTerritory.Add(salesTerritory);
                db.SaveChanges();
                TempData["Message"] = "Sales Territory Added Successfully!!";
                return RedirectToAction("Index");
            }

            return View(salesTerritory);
        }

        // GET: SalesTerritories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesTerritory salesTerritory = db.SalesTerritory.Find(id);
            if (salesTerritory == null)
            {
                return HttpNotFound();
            }
            return View(salesTerritory);
        }

        // POST: SalesTerritories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TerritoryID,TerritoryName")] SalesTerritory salesTerritory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesTerritory).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Sales Territory Saved Successfully!!";
                return RedirectToAction("Index");
            }
            return View(salesTerritory);
        }

        // GET: SalesTerritories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesTerritory salesTerritory = db.SalesTerritory.Find(id);
            if (salesTerritory == null)
            {
                return HttpNotFound();
            }
            return View(salesTerritory);
        }

        // POST: SalesTerritories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalesTerritory salesTerritory = db.SalesTerritory.Find(id);
            db.SalesTerritory.Remove(salesTerritory);
            db.SaveChanges();
            TempData["Message"] = "Sales Territory Deleted Successfully!!";

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
