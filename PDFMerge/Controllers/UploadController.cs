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
    public class UploadController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Upload/

        public ActionResult Index()
        {
            var recursos = db.Recursos.Include(r => r.Curso).Include(r => r.Materia);
            return View(recursos.ToList());
        }

        //
        // GET: /Upload/Details/5

        public ActionResult Details(int id = 0)
        {
            Recurso recurso = db.Recursos.Find(id);
            if (recurso == null)
            {
                return HttpNotFound();
            }
            return View(recurso);
        }

        //
        // GET: /Upload/Create

        public ActionResult Create()
        {
            ViewBag.CursoId = new SelectList(db.Cursos, "CursoId", "Descripcion");
            ViewBag.MateriaId = new SelectList(db.Materias, "MateriaId", "Descripcion");
            return View();
        }

        //
        // POST: /Upload/Create

        [HttpPost]
        public ActionResult Create(Recurso recurso, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var seq = db.Recursos.Where(x => x.MateriaId == recurso.MateriaId &&
                                                 x.CursoId == recurso.CursoId).Count();

                // Verify that the user selected a file
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = String.Format("{0}.{1}.{2}.pdf", recurso.MateriaId, recurso.CursoId, seq++); // System.IO.Path.GetFileName(file.FileName);
                    recurso.codigo = fileName;
                    FTP.uploadFileUsingFTP("ftp://laggel.site90.com/public_html/PDF/" + fileName, file, "a7673407", "Laggel007");
                }
                
                db.Recursos.Add(recurso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CursoId = new SelectList(db.Cursos, "CursoId", "Descripcion", recurso.CursoId);
            ViewBag.MateriaId = new SelectList(db.Materias, "MateriaId", "Descripcion", recurso.MateriaId);
            return View(recurso);
        }

        //
        // GET: /Upload/Edit/5

        public ActionResult CreateContenido()
        {
            ViewBag.CursoId = new SelectList(db.Cursos, "CursoId", "Descripcion");
            ViewBag.MateriaId = new SelectList(db.Materias, "MateriaId", "Descripcion");
            return View();
        }

        //
        // POST: /Upload/Create

        [HttpPost]
        public ActionResult CreateContenido(Recurso recurso, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var seq = db.Recursos.Where(x => x.MateriaId == recurso.MateriaId &&
                                                 x.CursoId == recurso.CursoId).Count();

                // Verify that the user selected a file
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = String.Format("{0}.{1}.pdf", recurso.MateriaId, recurso.CursoId); // System.IO.Path.GetFileName(file.FileName);
                    recurso.codigo = fileName;
                    FTP.uploadFileUsingFTP("ftp://laggel.site90.com/public_html/PDF/" + fileName, file, "a7673407", "Laggel007");
                }

                db.Recursos.Add(recurso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CursoId = new SelectList(db.Cursos, "CursoId", "Descripcion", recurso.CursoId);
            ViewBag.MateriaId = new SelectList(db.Materias, "MateriaId", "Descripcion", recurso.MateriaId);
            return View(recurso);
        }

        //
        // GET: /Upload/Edit/5


        public ActionResult Edit(int id = 0)
        {
            Recurso recurso = db.Recursos.Find(id);
            if (recurso == null)
            {
                return HttpNotFound();
            }
            ViewBag.CursoId = new SelectList(db.Cursos, "CursoId", "Descripcion", recurso.CursoId);
            ViewBag.MateriaId = new SelectList(db.Materias, "MateriaId", "Descripcion", recurso.MateriaId);
            return View(recurso);
        }

        //
        // POST: /Upload/Edit/5

        [HttpPost]
        public ActionResult Edit(Recurso recurso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recurso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CursoId = new SelectList(db.Cursos, "CursoId", "Descripcion", recurso.CursoId);
            ViewBag.MateriaId = new SelectList(db.Materias, "MateriaId", "Descripcion", recurso.MateriaId);
            return View(recurso);
        }

        //
        // GET: /Upload/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Recurso recurso = db.Recursos.Find(id);
            if (recurso == null)
            {
                return HttpNotFound();
            }
            return View(recurso);
        }

        //
        // POST: /Upload/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Recurso recurso = db.Recursos.Find(id);
            db.Recursos.Remove(recurso);
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