using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SVEUCILISNA_KNJIZNICA.Models;

namespace SVEUCILISNA_KNJIZNICA.Controllers
{
    public class KorisnikController : Controller
    {
        private Entities db = new Entities();

        // GET: Korisnik
        /*
        public ActionResult Index()
        {
            var korisniks = db.Korisniks.Include(k => k.Uloga);
            return View(korisniks.ToList());
        }
        */
        public ActionResult Index()

        {
            var korisniks = db.Korisniks
                .Include(k => k.Uloga);
               
            return View(korisniks.ToList());

        }

        public ActionResult IndexForAdmin()

        {
            var korisniksi = from m in db.Korisniks
                             where m.UlogaID==3
                             select m;
            

            return View(korisniksi.ToList());

        }

        // GET: Korisnik/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Korisnik korisnik = db.Korisniks.Find(id);
            if (korisnik == null)
            {
                return HttpNotFound();
            }
            return View(korisnik);
        }

        // GET: Korisnik/Create
        public ActionResult Create()
        {
            ViewBag.UlogaID = new SelectList(db.Ulogas, "UlogaID", "Naziv");
            return View();
        }

        // POST: Korisnik/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KorisnikID,Ime,Prezime,Lozinka,Mejl,UlogaID")] Korisnik korisnik)
        {
            if (ModelState.IsValid)
            {
                db.Korisniks.Add(korisnik);
                db.SaveChanges();
                return RedirectToAction("IndexForAdmin");
            }

            ViewBag.UlogaID = new SelectList(db.Ulogas, "UlogaID", "Naziv", korisnik.UlogaID);
            return View(korisnik);
        }

        // GET: Korisnik/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Korisnik korisnik = db.Korisniks.Find(id);
            if (korisnik == null)
            {
                return HttpNotFound();
            }
            ViewBag.UlogaID = new SelectList(db.Ulogas, "UlogaID", "Naziv", korisnik.UlogaID);
            return View(korisnik);
        }

        // POST: Korisnik/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KorisnikID,Ime,Prezime,Lozinka,Mejl,UlogaID")] Korisnik korisnik)
        {
            if (ModelState.IsValid)
            {
                db.Entry(korisnik).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexForAdmin");
            }
            ViewBag.UlogaID = new SelectList(db.Ulogas, "UlogaID", "Naziv", korisnik.UlogaID);
            return View(korisnik);
        }

        // GET: Korisnik/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Korisnik korisnik = db.Korisniks.Find(id);
            if (korisnik == null)
            {
                return HttpNotFound();
            }
            return View(korisnik);
        }

        // POST: Korisnik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Korisnik korisnik = db.Korisniks.Find(id);
            db.Korisniks.Remove(korisnik);
            db.SaveChanges();
            return RedirectToAction("IndexForAdmin");
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
