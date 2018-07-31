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
    public class VehiculoController : Controller
    {
        private TallerMecanicoEntities db = new TallerMecanicoEntities();

        //
        // GET: /Vehiculo/

        public ActionResult Index()
        {
            var vehiculo = db.Vehiculo.Include(v => v.Propietario);
            return View(vehiculo.ToList());
        }

        //
        // GET: /Vehiculo/Details/5

        public ActionResult Details(int id = 0)
        {
            Vehiculo vehiculo = db.Vehiculo.Find(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            return View(vehiculo);
        }

        //
        // GET: /Vehiculo/Create

        public ActionResult Create(int id=0)
        {   
            if(id>0)
                
            {
                ViewBag.VehiculoRepetido = "La patente vehiculo ingresado ya existe";
                ViewBag.idPropietario = new SelectList(db.Propietario, "idPropietario", "LetAB");
                return View();
            }


            ViewBag.idPropietario = new SelectList(db.Propietario, "idPropietario", "LetAB");
            return View();
        }

        //
        // POST: /Vehiculo/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vehiculo vehiculo)
        {

            int existencia = db.Vehiculo.Where(v => v.nroPatente == vehiculo.nroPatente).Count();

            if (ModelState.IsValid)
            {
                if (existencia <= 0)
                {
                    db.Vehiculo.Add(vehiculo);
                    db.SaveChanges();
                    return RedirectToAction("Create", "OrdenDeTrabajo");
                }
                else
                {
                    
                    return RedirectToAction("Create", "Vehiculo", new { id = existencia });
                }

            }

            ViewBag.idPropietario = new SelectList(db.Propietario, "idPropietario", "LetAB", vehiculo.idPropietario);
            return View(vehiculo);
        }


       

        //
        // GET: /Vehiculo/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Vehiculo vehiculo = db.Vehiculo.Find(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            ViewBag.idPropietario = new SelectList(db.Propietario, "idPropietario", "nombre", vehiculo.idPropietario);
            return View(vehiculo);
        }

        //
        // POST: /Vehiculo/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Vehiculo vehiculo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehiculo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idPropietario = new SelectList(db.Propietario, "idPropietario", "nombre", vehiculo.idPropietario);
            return View(vehiculo);
        }

        //
        // GET: /Vehiculo/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Vehiculo vehiculo = db.Vehiculo.Find(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            return View(vehiculo);
        }

        //
        // POST: /Vehiculo/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehiculo vehiculo = db.Vehiculo.Find(id);
            db.Vehiculo.Remove(vehiculo);
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