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
    public class VentaController : Controller
    {
        private DB_Carro db = new DB_Carro();

        // GET: Venta
        public ActionResult Index()
        {
            var venta = db.Venta.Include(v => v.Carro).Include(v => v.Usuario);
            return View(venta.ToList());
        }

        // GET: Venta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Venta.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // GET: Venta/Create
        public ActionResult Create()
        {
            ViewBag.id_Carro = new SelectList(db.Carro, "id_Carro", "nombre");
            ViewBag.id_Cliente = new SelectList(db.Usuario, "id_Usuario", "nombre");
            return View();
        }

        // POST: Venta/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Venta,id_Carro,id_Cliente,fecha_Venta")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                db.Venta.Add(venta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_Carro = new SelectList(db.Carro, "id_Carro", "nombre", venta.id_Carro);
            ViewBag.id_Cliente = new SelectList(db.Usuario, "id_Usuario", "nombre", venta.id_Cliente);
            return View(venta);
        }

        // GET: Venta/Create
        public ActionResult Comprar()
        {
            ViewBag.id_Carro = new SelectList(db.Carro, "id_Carro", "nombre");
            ViewBag.id_Cliente = new SelectList(db.Usuario, "id_Usuario", "nombre");
            return View();
        }

        // POST: Venta/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Comprar([Bind(Include = "id_Venta,id_Carro,id_Cliente,fecha_Venta")] Venta venta, int?id)
        {
            if (ModelState.IsValid)
            {
                var id_Cliente = db.Usuario.Find(Session["idUsuario"]);
                venta.id_Cliente = id_Cliente.id_Usuario;
                db.Venta.Add(venta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_Carro = new SelectList(db.Carro, "id_Carro", "nombre", venta.id_Carro);
            ViewBag.id_Cliente = new SelectList(db.Usuario, "id_Usuario", "nombre", venta.id_Cliente);
            return View(venta);
        }

        // GET: Venta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Venta.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_Carro = new SelectList(db.Carro, "id_Carro", "nombre", venta.id_Carro);
            ViewBag.id_Cliente = new SelectList(db.Usuario, "id_Usuario", "nombre", venta.id_Cliente);
            return View(venta);
        }

        // POST: Venta/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Venta,id_Carro,id_Cliente,fecha_Venta")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_Carro = new SelectList(db.Carro, "id_Carro", "nombre", venta.id_Carro);
            ViewBag.id_Cliente = new SelectList(db.Usuario, "id_Usuario", "nombre", venta.id_Cliente);
            return View(venta);
        }

        // GET: Venta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Venta.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // POST: Venta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Venta venta = db.Venta.Find(id);
            db.Venta.Remove(venta);
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
