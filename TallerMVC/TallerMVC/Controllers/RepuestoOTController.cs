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
    public class RepuestoOTController : Controller
    {
        private TallerMecanicoEntities db = new TallerMecanicoEntities();

        //
        // GET: /RepuestoOT/

        public ActionResult Index()
        {
            var repuestoot = db.RepuestoOT.Include(r => r.OrdenDeTrabajo).Include(r => r.Repuesto);
            return View(repuestoot.ToList());
        }

        //
        // GET: /RepuestoOT/Details/5

        public ActionResult Details(int id = 0)
        {
            RepuestoOT repuestoot = db.RepuestoOT.Find(id);

            if (repuestoot == null)
            {
                return HttpNotFound();
            }
            return View(repuestoot);
        }

        //
        // GET: /RepuestoOT/Create

        public ActionResult Create()
        {
            ViewBag.idOrdenDeTrabajo = new SelectList(db.OrdenDeTrabajo, "idOrdenDeTrabajo", "detalleFalla");
            ViewBag.idRepuesto = new SelectList(db.Repuesto, "idRepuesto", "nombre");
            return View();
        }

        //
        // POST: /RepuestoOT/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RepuestoOT repuestoot,int idOrdenDeTrabajo)
        {
            repuestoot.idOrdenDeTrabajo = idOrdenDeTrabajo;

            if (ModelState.IsValid)
            {
                db.RepuestoOT.Add(repuestoot);
                db.SaveChanges();
                return RedirectToAction("Index","OrdenDeTrabajo");
            }

            ViewBag.idOrdenDeTrabajo = new SelectList(db.OrdenDeTrabajo, "idOrdenDeTrabajo", "detalleFalla", repuestoot.idOrdenDeTrabajo);
            ViewBag.idRepuesto = new SelectList(db.Repuesto, "idRepuesto", "nombre", repuestoot.idRepuesto);
            return View(repuestoot);
        }

        //
        // GET: /RepuestoOT/Edit/5

        public ActionResult Edit(int id = 0)
        {
            RepuestoOT repuestoot = db.RepuestoOT.Find(id);
            if (repuestoot == null)
            {
                return HttpNotFound();
            }
            ViewBag.idOrdenDeTrabajo = new SelectList(db.OrdenDeTrabajo, "idOrdenDeTrabajo", "detalleFalla", repuestoot.idOrdenDeTrabajo);
            ViewBag.idRepuesto = new SelectList(db.Repuesto, "idRepuesto", "nombre", repuestoot.idRepuesto);
            return View(repuestoot);
        }

        //
        // POST: /RepuestoOT/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RepuestoOT repuestoot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(repuestoot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idOrdenDeTrabajo = new SelectList(db.OrdenDeTrabajo, "idOrdenDeTrabajo", "detalleFalla", repuestoot.idOrdenDeTrabajo);
            ViewBag.idRepuesto = new SelectList(db.Repuesto, "idRepuesto", "nombre", repuestoot.idRepuesto);
            return View(repuestoot);
        }

        //
        // GET: /RepuestoOT/Delete/5

        public ActionResult Delete(int id = 0)
        {
            RepuestoOT repuestoot = db.RepuestoOT.Find(id);
            if (repuestoot == null)
            {
                return HttpNotFound();
            }
            return View(repuestoot);
        }

        //
        // POST: /RepuestoOT/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RepuestoOT repuestoot = db.RepuestoOT.Find(id);
            db.RepuestoOT.Remove(repuestoot);
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