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
    public class CarroController : Controller
    {
        private DB_Carro db = new DB_Carro();

        // GET: Carro
        public ActionResult Index()
        {
            var carro = db.Carro.Include(c => c.Marca);
            return View(carro.ToList());
        }

        public ActionResult IndexInvitado()
        {
            //if (Convert.ToInt32(Session["idRol"]) == 2)
            //{
              // return RedirectToAction("Index");
            //}
            //else { 
            var carro = db.Carro.Include(c => c.Marca);
            return View(carro.ToList());
            //}
        }

        public ActionResult IndexCliente()
        {
            var carro = db.Carro.Include(c => c.Marca);
            return View(carro.ToList());
        }

        // GET: Carro/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carro carro = db.Carro.Find(id);
            if (carro == null)
            {
                return HttpNotFound();
            }
            return View(carro);
        }

        // GET: Carro/Create
        public ActionResult Create()
        {
            ViewBag.id_Marca = new SelectList(db.Marca, "id_Marca", "nombre");
            return View();
        }

        // POST: Carro/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Carro,nombre,modelo,id_Marca,precio")] Carro carro)
        {
            if (ModelState.IsValid)
            {
                db.Carro.Add(carro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_Marca = new SelectList(db.Marca, "id_Marca", "nombre", carro.id_Marca);
            return View(carro);
        }

        // GET: Carro/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carro carro = db.Carro.Find(id);
            if (carro == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_Marca = new SelectList(db.Marca, "id_Marca", "nombre", carro.id_Marca);
            return View(carro);
        }

        // POST: Carro/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Carro,nombre,modelo,id_Marca,precio")] Carro carro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_Marca = new SelectList(db.Marca, "id_Marca", "nombre", carro.id_Marca);
            return View(carro);
        }

        // GET: Carro/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carro carro = db.Carro.Find(id);
            if (carro == null)
            {
                return HttpNotFound();
            }
            return View(carro);
        }

        // POST: Carro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Carro carro = db.Carro.Find(id);
            db.Carro.Remove(carro);
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

        public ActionResult Comprar(int?id)
        {
            Session["idCarro"] = id;
            ViewBag.id_Carro = id;
            ViewBag.id_Cliente = new SelectList(db.Usuario, "id_Usuario", "nombre");
            ViewBag.nombre_Carro = db.Carro.Find(id).nombre;
            ViewBag.precio_Carro = db.Carro.Find(id).precio;
            ViewBag.fecha = Convert.ToString(DateTime.Now);
            return View();
        }

        // POST: Venta/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Comprar([Bind(Include = "id_Venta,id_Carro,id_Cliente,fecha_Venta")] Venta venta, int? id)
        {
            if (ModelState.IsValid)
            {
                venta.id_Carro = Convert.ToInt32(Session["idCarro"]);
                var id_Cliente = db.Usuario.Find(Session["idUsuario"]);
                var carro = db.Carro.Find(Convert.ToInt32(Session["idCarro"]));
                ViewBag.precio = carro.precio;
                venta.id_Cliente = id_Cliente.id_Usuario;
                venta.fecha_Venta = Convert.ToString(DateTime.Now);
                db.Venta.Add(venta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_Carro = new SelectList(db.Carro, "id_Carro", "nombre", venta.id_Carro);
            ViewBag.id_Cliente = new SelectList(db.Usuario, "id_Usuario", "nombre", venta.id_Cliente);
            return View(venta);
        }
    }
}
