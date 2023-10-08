using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VSG.Model;
using VSG.Models;

namespace VSG.Areas.Admin.Controllers
{
    public class Shoe_ServiceController : Controller
    {
        private VSGModel db = new VSGModel();

        // GET: Admin/Shoe_Service
        public ActionResult Index()
        {
            return View(db.Shoe_Services.ToList());
        }

        // GET: Admin/Shoe_Service/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shoe_Service shoe_Service = db.Shoe_Services.Find(id);
            if (shoe_Service == null)
            {
                return HttpNotFound();
            }
            return View(shoe_Service);
        }

        // GET: Admin/Shoe_Service/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Shoe_Service/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Price,Description,Image,CategoryId")] Shoe_Service shoe_Service)
        {
            if (ModelState.IsValid)
            {
                db.Shoe_Services.Add(shoe_Service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shoe_Service);
        }

        // GET: Admin/Shoe_Service/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shoe_Service shoe_Service = db.Shoe_Services.Find(id);
            if (shoe_Service == null)
            {
                return HttpNotFound();
            }
            return View(shoe_Service);
        }

        // POST: Admin/Shoe_Service/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Price,Description,Image,CategoryId")] Shoe_Service shoe_Service)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shoe_Service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shoe_Service);
        }

        // GET: Admin/Shoe_Service/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shoe_Service shoe_Service = db.Shoe_Services.Find(id);
            if (shoe_Service == null)
            {
                return HttpNotFound();
            }
            return View(shoe_Service);
        }

        // POST: Admin/Shoe_Service/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Shoe_Service shoe_Service = db.Shoe_Services.Find(id);
            db.Shoe_Services.Remove(shoe_Service);
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
