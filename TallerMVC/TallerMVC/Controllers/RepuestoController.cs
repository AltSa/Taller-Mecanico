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
    public class RepuestoController : Controller
    {
        private TallerMecanicoEntities db = new TallerMecanicoEntities();

        //
        // GET: /Repuesto/

        public ActionResult Index()

        {
            
            return View(db.Repuesto.ToList());
        }

        //
        // GET: /Repuesto/Details/5

        public ActionResult Details(int id = 0)
        {
            Repuesto repuesto = db.Repuesto.Find(id);
            if (repuesto == null)
            {
                return HttpNotFound();
            }
            return View(repuesto);
        }

        //
        // GET: /Repuesto/Create

        public ActionResult Create(int id=0)
        {
            if (id>0)
            {
                ViewBag.RepuestoRepetido = "El repuesto ingresado ya existe";
                return View();
            }
            
            return View();
        }

        //
        // POST: /Repuesto/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Repuesto repuesto)
        {   
            int existencia = db.Repuesto.Where(r=>r.nombre== repuesto.nombre).Count();
            if (ModelState.IsValid)
            {
                if (existencia <= 0)
                {
                    db.Repuesto.Add(repuesto);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Repuesto");
                }
                else {
                    
                    return RedirectToAction("Create", "Repuesto",new{id=1});
                }
            }

            return View(repuesto);
        }

        //
        // GET: /Repuesto/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Repuesto repuesto = db.Repuesto.Find(id);
            if (repuesto == null)
            {
                return HttpNotFound();
            }
            return View(repuesto);
        }

        //
        // POST: /Repuesto/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Repuesto repuesto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(repuesto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(repuesto);
        }

        //
        // GET: /Repuesto/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Repuesto repuesto = db.Repuesto.Find(id);
            if (repuesto == null)
            {
                return HttpNotFound();
            }
            return View(repuesto);
        }

        //
        // POST: /Repuesto/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Repuesto repuesto = db.Repuesto.Find(id);
            db.Repuesto.Remove(repuesto);
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