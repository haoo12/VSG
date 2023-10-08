using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VSG.Models;

namespace VSG.Areas.Admin.Controllers
{
    public class OrderrsController : Controller
    {
        private VSGModel db = new VSGModel();

        // GET: Admin/Orderrs
        public ActionResult Index()
        {
            return View(db.Orderrs.ToList());
        }

        // GET: Admin/Orderrs/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orderr orderr = db.Orderrs.Find(id);
            if (orderr == null)
            {
                return HttpNotFound();
            }
            return View(orderr);
        }

        // GET: Admin/Orderrs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Orderrs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CustomerId,CreatedDate,ShipName,ShipMobile,ShipAddress,ShipEmail,Status")] Orderr orderr)
        {
            if (ModelState.IsValid)
            {
                db.Orderrs.Add(orderr);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orderr);
        }

        // GET: Admin/Orderrs/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orderr orderr = db.Orderrs.Find(id);
            if (orderr == null)
            {
                return HttpNotFound();
            }
            return View(orderr);
        }

        // POST: Admin/Orderrs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CustomerId,CreatedDate,ShipName,ShipMobile,ShipAddress,ShipEmail,Status")] Orderr orderr)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderr).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderr);
        }

        // GET: Admin/Orderrs/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orderr orderr = db.Orderrs.Find(id);
            if (orderr == null)
            {
                return HttpNotFound();
            }
            return View(orderr);
        }

        // POST: Admin/Orderrs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Orderr orderr = db.Orderrs.Find(id);
            db.Orderrs.Remove(orderr);
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
