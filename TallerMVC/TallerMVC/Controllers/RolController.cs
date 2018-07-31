using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TallerMVC.Models;

namespace TallerMVC.Controllers
{
    public class RolController : Controller
    {
        private TallerMecanicoEntities db = new TallerMecanicoEntities();

        //
        // GET: /Rol/

        public ActionResult Index()
        {
            return View(db.Rol.ToList());
        }

        //
        // GET: /Rol/Details/5

        public ActionResult Details(int id = 0)
        {
            Rol rol = db.Rol.Find(id);
            if (rol == null)
            {
                return HttpNotFound();
            }
            return View(rol);
        }

        //
        // GET: /Rol/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Rol/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Rol rol)
        {
            if (ModelState.IsValid)
            {
                db.Rol.Add(rol);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rol);
        }

        //
        // GET: /Rol/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Rol rol = db.Rol.Find(id);
            if (rol == null)
            {
                return HttpNotFound();
            }
            return View(rol);
        }

        //
        // POST: /Rol/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Rol rol)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rol).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rol);
        }

        //
        // GET: /Rol/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Rol rol = db.Rol.Find(id);
            if (rol == null)
            {
                return HttpNotFound();
            }
            return View(rol);
        }

        //
        // POST: /Rol/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rol rol = db.Rol.Find(id);
            db.Rol.Remove(rol);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}