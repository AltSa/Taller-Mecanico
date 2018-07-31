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
    public class OrdenDeTrabajoController : Controller
    {
        private TallerMecanicoEntities db = new TallerMecanicoEntities();

        //
        // GET: /OrdenDeTrabajo/

        public ActionResult Index()
        {
            var ordendetrabajo = db.OrdenDeTrabajo.Include(o => o.Usuario).Include(o => o.Vehiculo);
            return View(ordendetrabajo.ToList());
        }

        //
        // GET: /OrdenDeTrabajo/Details/5

        public ActionResult Details(int idOrdenDeTrabajo = 0)
        {

            OrdenDeTrabajo ordendetrabajo = db.OrdenDeTrabajo.Find(idOrdenDeTrabajo);
            foreach (RepuestoOT repuestoOT in db.RepuestoOT.Where(r => r.idOrdenDeTrabajo == idOrdenDeTrabajo))
            {
                ordendetrabajo.RepuestoOT.Add(repuestoOT);
            }
            if (ordendetrabajo == null)
            {
                return HttpNotFound();
            }
            return View(ordendetrabajo);
        }

        //
        // GET: /OrdenDeTrabajo/Create

        public ActionResult Create()
        {
            ViewBag.idUsuario = new SelectList(db.Usuario, "idUsuario", "nombreUsuario");
            ViewBag.idVehiculo = new SelectList(db.Vehiculo, "idVehiculo", "nroPatente");
            return View();
        }

        //
        // POST: /OrdenDeTrabajo/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrdenDeTrabajo ordendetrabajo)
        {
            if (ModelState.IsValid)
            {
               

                ordendetrabajo.fechaIngreso = DateTime.Now;
                ordendetrabajo.idUsuario = int.Parse(Session["idUser"].ToString());

                db.OrdenDeTrabajo.Add(ordendetrabajo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idUsuario = new SelectList(db.Usuario, "idUsuario", "nombreUsuario", ordendetrabajo.idUsuario);
            ViewBag.idVehiculo = new SelectList(db.Vehiculo, "idVehiculo", "nroPatente", ordendetrabajo.idVehiculo);
            return View(ordendetrabajo);
        }

        //
        // GET: /OrdenDeTrabajo/Edit/5

        public ActionResult Edit(int id = 0)
        {
            OrdenDeTrabajo ordendetrabajo = db.OrdenDeTrabajo.Find(id);
            if (ordendetrabajo == null)
            {
                return HttpNotFound();
            }
            ViewBag.idUsuario = new SelectList(db.Usuario, "idUsuario", "nombreUsuario", ordendetrabajo.idUsuario);
            ViewBag.idVehiculo = new SelectList(db.Vehiculo, "idVehiculo", "nroPatente", ordendetrabajo.idVehiculo);
            return View(ordendetrabajo);
        }

        //
        // POST: /OrdenDeTrabajo/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrdenDeTrabajo ordendetrabajo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordendetrabajo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idUsuario = new SelectList(db.Usuario, "idUsuario", "nombreUsuario", ordendetrabajo.idUsuario);
            ViewBag.idVehiculo = new SelectList(db.Vehiculo, "idVehiculo", "nroPatente", ordendetrabajo.idVehiculo);
            return View(ordendetrabajo);
        }

        //
        // GET: /OrdenDeTrabajo/Delete/5

        public ActionResult Delete(int id = 0)
        {
            OrdenDeTrabajo ordendetrabajo = db.OrdenDeTrabajo.Find(id);
            if (ordendetrabajo == null)
            {
                return HttpNotFound();
            }
            return View(ordendetrabajo);
        }

        //
        // POST: /OrdenDeTrabajo/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrdenDeTrabajo ordendetrabajo = db.OrdenDeTrabajo.Find(id);
            db.OrdenDeTrabajo.Remove(ordendetrabajo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Close(int idOrdenDeTrabajo = 0)
        {
            float precioTotal = 0;
            OrdenDeTrabajo orden = db.OrdenDeTrabajo.Find(idOrdenDeTrabajo);
            foreach (RepuestoOT repuestoOT in db.RepuestoOT.Where(r => r.idOrdenDeTrabajo == idOrdenDeTrabajo))
            {
                precioTotal = precioTotal + (float)(repuestoOT.Repuesto.costo * repuestoOT.cantidadDeRepuesto) + (float)(repuestoOT.cantidadHorasTrabajo * 350);
            }
            orden.total = precioTotal;

            db.SaveChanges();
            return RedirectToAction("Index");  
        }

        [HttpPost]
        public ActionResult filtrar(FormCollection form)
        {
            
            string nombreUsuario = form["nombreUsuario"];
            
            if (db.OrdenDeTrabajo.Where(a=>a.Usuario.nombreUsuario==nombreUsuario).Count()>0)
            {
                var orden = db.OrdenDeTrabajo.Where(o => o.Usuario.nombreUsuario == nombreUsuario);

                return View("Index",orden.ToList());

            }
            else {
                return RedirectToAction("Index","OrdenDeTrabajo");
            }

            
        }


    }
}