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
    public class HasSkillsController : Controller
    {
        private Entities db = new Entities();

        // GET: HasSkills
        public ActionResult Index()
        {
            var hasSkill = db.HasSkill.Include(h => h.Employee).Include(h => h.Skill);
            return View(hasSkill.ToList());
        }

        // GET: HasSkills/Details/5
        public ActionResult Details(int? eid, string sid)
        {
            if (eid == null || sid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HasSkill hasSkill = db.HasSkill.Find(eid, sid);
            if (hasSkill == null)
            {
                return HttpNotFound();
            }
            return View(hasSkill);
        }

        // GET: HasSkills/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.Employee, "EmployeeID", "EmployeeName");
            ViewBag.SkillID = new SelectList(db.Skill, "SkillID", "SkillDescription");
            return View();
        }

        // POST: HasSkills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HasSkillID,EmployeeID,SkillID")] HasSkill hasSkill)
        {
            if (ModelState.IsValid)
            {
                db.HasSkill.Add(hasSkill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Employee, "EmployeeID", "EmployeeName", hasSkill.EmployeeID);
            ViewBag.SkillID = new SelectList(db.Skill, "SkillID", "SkillDescription", hasSkill.SkillID);
            return View(hasSkill);
        }

        // GET: HasSkills/Edit/5
        public ActionResult Edit(int? eid, string sid)
        {
            if (eid == null || sid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HasSkill hasSkill = db.HasSkill.Find(eid, sid);
            if (hasSkill == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employee, "EmployeeID", "EmployeeName", hasSkill.EmployeeID);
            ViewBag.SkillID = new SelectList(db.Skill, "SkillID", "SkillDescription", hasSkill.SkillID);
            return View(hasSkill);
        }

        // POST: HasSkills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HasSkillID,EmployeeID,SkillID")] HasSkill hasSkill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hasSkill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Employee, "EmployeeID", "EmployeeName", hasSkill.EmployeeID);
            ViewBag.SkillID = new SelectList(db.Skill, "SkillID", "SkillDescription", hasSkill.SkillID);
            return View(hasSkill);
        }

        // GET: HasSkills/Delete/5
        public ActionResult Delete(int? eid, string sid)
        {
            if (eid == null || sid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HasSkill hasSkill = db.HasSkill.Find(eid, sid);
            if (hasSkill == null)
            {
                return HttpNotFound();
            }
            return View(hasSkill);
        }

        // POST: HasSkills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int eid, string sid)
        {
            HasSkill hasSkill = db.HasSkill.Find(eid, sid, sid);
            db.HasSkill.Remove(hasSkill);
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
