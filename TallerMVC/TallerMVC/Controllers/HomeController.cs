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
    public class HomeController : Controller
    {
        private TallerMecanicoEntities db = new TallerMecanicoEntities();

        //
        // GET: /Home/

        public ActionResult Index(int  id=0)
        {   
            if(id>0)
            {
            
                ViewBag.MensajeError = "Ingrese un usuario y contraseña validos";
                return View();
            
            }
            return View();
        }

        //
        // GET: /Home/Details/5

        public ActionResult Details(int id = 0)
        {
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            ViewBag.idRol = new SelectList(db.Rol, "idRol", "descripcion");
            return View();
        }

        //
        // POST: /Home/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idRol = new SelectList(db.Rol, "idRol", "descripcion", usuario.idRol);
            return View(usuario);
        }

        //
        // GET: /Home/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.idRol = new SelectList(db.Rol, "idRol", "descripcion", usuario.idRol);
            return View(usuario);
        }

        //
        // POST: /Home/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idRol = new SelectList(db.Rol, "idRol", "descripcion", usuario.idRol);
            return View(usuario);
        }

        //
        // GET: /Home/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        //
        // POST: /Home/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        

        [HttpPost]
        public ActionResult Loguearse(FormCollection form)
        {
            int primeravez = 0;
            string nombre = form["nombre"];
            string pass = form["password"];
            bool user = db.Usuario.Where(u => u.nombreUsuario == nombre && u.password==pass).Any();
            

            if (user)
            {   
                Usuario usuario = db.Usuario.Where(u => u.nombreUsuario == nombre).First();
                Session["idUser"] = usuario.idUsuario.ToString();
                Session["userRol"] = usuario.Rol.idRol.ToString();
                int tipo=usuario.Rol.idRol;
                switch (tipo)
                { 
                    case 1:
                        return RedirectToAction("Index", "OrdenDeTrabajo");
                        break;
                    case 2:
                        return RedirectToAction("Index", "Usuario");
                        break;

                    default:
                        return RedirectToAction("Index", "OrdenDeTrabajo");
                        break;              
                }
                
            }
            else
            {
                primeravez++;
                
                return RedirectToAction("Index", "Home", new { id = primeravez });
            }
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}