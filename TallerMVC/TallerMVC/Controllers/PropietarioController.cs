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
    public class PropietarioController : Controller
    {
        private TallerMecanicoEntities db = new TallerMecanicoEntities();

        //
        // GET: /Propietario/

        public ActionResult Index()
        {
            return View(db.Propietario.ToList());
        }

        //
        // GET: /Propietario/Details/5

        public ActionResult Details(int id = 0)
        {
            Propietario propietario = db.Propietario.Find(id);
            if (propietario == null)
            {
                return HttpNotFound();
            }
            return View(propietario);
        }

        //
        // GET: /Propietario/Create

        public ActionResult Create(int id=0)
        {
            if (id > 0)
            {
                ViewBag.Mensaje = "El DNI ingresado ya existe";
                return View();
            }
            return View();
        }

        //
        // POST: /Propietario/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Propietario propietario)
        {   
            int existencia = db.Propietario.Where(p=>p.dni==propietario.dni).Count();
            if (ModelState.IsValid)
            {
                if (existencia <= 0)
                {
                    db.Propietario.Add(propietario);
                    db.SaveChanges();
                    return RedirectToAction("Create", "Vehiculo");
                }
                else
                {
                    
                    return RedirectToAction("Create", "Propietario", new { id = 1 });
                }
            }
           

            return View(propietario);
        }

        //
        // GET: /Propietario/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Propietario propietario = db.Propietario.Find(id);
            if (propietario == null)
            {
                return HttpNotFound();
            }
            return View(propietario);
        }

        //
        // POST: /Propietario/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Propietario propietario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(propietario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(propietario);
        }

        //
        // GET: /Propietario/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Propietario propietario = db.Propietario.Find(id);
            if (propietario == null)
            {
                return HttpNotFound();
            }
            return View(propietario);
        }

        //
        // POST: /Propietario/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Propietario propietario = db.Propietario.Find(id);
            db.Propietario.Remove(propietario);
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