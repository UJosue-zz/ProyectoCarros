using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoCarros.Models;

namespace ProyectoCarros.Controllers
{
    public class UsuarioController : Controller
    {
        private DB_Carro db = new DB_Carro();

        // GET: Usuario
        public ActionResult Index()
        {
            var usuario = db.Usuario.Include(u => u.Rol);
            return View(usuario.ToList());
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            ViewBag.id_Rol = new SelectList(db.Rol, "id_Rol", "nombre");
            return View();
        }

        // POST: Usuario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Usuario,nombre,nick,contrasena,id_Rol")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
                        
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_Rol = new SelectList(db.Rol, "id_Rol", "nombre", usuario.id_Rol);
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Usuario,nombre,nick,contrasena,id_Rol")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_Rol = new SelectList(db.Rol, "id_Rol", "nombre", usuario.id_Rol);
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        //Login
        public ActionResult Login(Usuario usuario)
        {
            var usr = db.Usuario.FirstOrDefault(u => u.contrasena == usuario.contrasena && u.nick == usuario.nick);
            if(usr!= null)
            {
                Session["username"] = usr.nombre;
                Session["idUsuario"] = usr.id_Usuario;
                Session["idRol"] = usr.id_Rol;
                return VerificarSesion();
            } else
            {
                ModelState.AddModelError("", "Verifique sus credenciales");
            }
            return View();
        }

        //Verificar Sesion
        public ActionResult VerificarSesion()
        {
            if (Session["username"] != null)
            {
                return RedirectToAction("IndexCliente", "Carro");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
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
        // GET: Usuario/Create
        public ActionResult Registrar()
        {
            ViewBag.id_Rol = new SelectList(db.Rol, "id_Rol", "nombre");
            return View();
        }

        // POST: Usuario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrar([Bind(Include = "id_Usuario,nombre,nick,contrasena,id_Rol")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuario.Add(usuario);
                db.SaveChanges();
                Session["username"] = usuario.nombre;
                Session["idUsuario"] = usuario.id_Usuario;
                Session["idRol"] = usuario.id_Rol;
                return VerificarSesion();
            }

            return View(usuario);
        }

        public ActionResult Salir()
        {
            Session.Abandon();

            return Redirect("../Carro/IndexInvitado");
        }
    }
}
