using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcFolhetos.Models;

namespace MvcFolhetos.Controllers
{
    public class FolhetosController : Controller
    {
        private FolhetosDBContext db = new FolhetosDBContext();

        // GET: Folhetos
        public ActionResult Index()
        {
            return View(db.Folhetos.ToList());
        }

        // GET: Folhetos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Folhetos folhetos = db.Folhetos.Find(id);
            if (folhetos == null)
            {
                return HttpNotFound();
            }
            return View(folhetos);
        }

        // GET: Folhetos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Folhetos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FolhetosID,Titulo,Descricao,Pasta,DataInic,DataFim,NomeEmpresa")] Folhetos folhetos)
        {
            if (ModelState.IsValid)
            {
                db.Folhetos.Add(folhetos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(folhetos);
        }

        // GET: Folhetos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Folhetos folhetos = db.Folhetos.Find(id);
            if (folhetos == null)
            {
                return HttpNotFound();
            }
            return View(folhetos);
        }

        // POST: Folhetos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FolhetosID,Titulo,Descricao,Pasta,DataInic,DataFim,NomeEmpresa")] Folhetos folhetos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(folhetos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(folhetos);
        }

        // GET: Folhetos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Folhetos folhetos = db.Folhetos.Find(id);
            if (folhetos == null)
            {
                return HttpNotFound();
            }
            return View(folhetos);
        }

        // POST: Folhetos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Folhetos folhetos = db.Folhetos.Find(id);
            db.Folhetos.Remove(folhetos);
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
