using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PDFMerge.Models;

namespace PDFMerge.Controllers
{
    public class MateriaController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Materia/

        public ActionResult Index()
        {
            return View(db.Materias.ToList());
        }

        //
        // GET: /Materia/Details/5

        public ActionResult Details(int id = 0)
        {
            Materia materia = db.Materias.Find(id);
            if (materia == null)
            {
                return HttpNotFound();
            }
            return View(materia);
        }

        //
        // GET: /Materia/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Materia/Create

        [HttpPost]
        public ActionResult Create(Materia materia)
        {
            if (ModelState.IsValid)
            {
                db.Materias.Add(materia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(materia);
        }

        //
        // GET: /Materia/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Materia materia = db.Materias.Find(id);
            if (materia == null)
            {
                return HttpNotFound();
            }
            return View(materia);
        }

        //
        // POST: /Materia/Edit/5

        [HttpPost]
        public ActionResult Edit(Materia materia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(materia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(materia);
        }

        //
        // GET: /Materia/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Materia materia = db.Materias.Find(id);
            if (materia == null)
            {
                return HttpNotFound();
            }
            return View(materia);
        }

        //
        // POST: /Materia/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Materia materia = db.Materias.Find(id);
            db.Materias.Remove(materia);
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