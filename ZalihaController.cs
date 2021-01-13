using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SVEUCILISNA_KNJIZNICA.Models;

namespace SVEUCILISNA_KNJIZNICA.Controllers
{
    public class ZalihaController : Controller
    {
        private Entities db = new Entities();

        // GET: Zaliha
        public ActionResult Index()
        {
            var zalihas = db.Zalihas.Include(z => z.Knjiga);

            return View(zalihas.ToList());
        }

        // GET: Zaliha/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zaliha zaliha = db.Zalihas.Find(id);
            if (zaliha == null)
            {
                return HttpNotFound();
            }
            return View(zaliha);
        }

        // GET: Zaliha/Create
        public ActionResult Create()
        {
            ViewBag.KnjigaID = new SelectList(db.Knjigas, "KnjigaID", "Naziv");
            return View();
        }

        // POST: Zaliha/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]








        public ActionResult Create([Bind(Include = "ZalihaID,KnjigaID,StanjeZaliha")] Zaliha zaliha)
        {
            if (ModelState.IsValid)
            {
                Zaliha provjera = db.Zalihas.Where(x => x.KnjigaID == zaliha.KnjigaID).FirstOrDefault();
                if(provjera!=null)
                {
                    provjera.StanjeZaliha = provjera.StanjeZaliha + zaliha.StanjeZaliha;
                    db.Entry(provjera).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                db.Zalihas.Add(zaliha);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KnjigaID = new SelectList(db.Knjigas, "KnjigaID", "Naziv", zaliha.KnjigaID);
            return View(zaliha);
        }

        // GET: Zaliha/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zaliha zaliha = db.Zalihas.Find(id);
            if (zaliha == null)
            {
                return HttpNotFound();
            }
            ViewBag.KnjigaID = new SelectList(db.Knjigas, "KnjigaID", "Naziv", zaliha.KnjigaID);
            return View(zaliha);
        }

        // POST: Zaliha/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ZalihaID,KnjigaID,StanjeZaliha,Barkod")] Zaliha zaliha)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zaliha).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KnjigaID = new SelectList(db.Knjigas, "KnjigaID", "Naziv", zaliha.KnjigaID);
            return View(zaliha);
        }


        // GET: Zaliha/Edit/5
      
        
      

        // GET: Zaliha/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zaliha zaliha = db.Zalihas.Find(id);
            if (zaliha == null)
            {
                return HttpNotFound();
            }
            return View(zaliha);
        }

        // POST: Zaliha/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Zaliha zaliha = db.Zalihas.Find(id);
            db.Zalihas.Remove(zaliha);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult IndexForStudent()
        {
            var zalihas = db.Zalihas.Include(z => z.Knjiga).Where(z => z.StanjeZaliha > 0);

            return View(zalihas.ToList());
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
