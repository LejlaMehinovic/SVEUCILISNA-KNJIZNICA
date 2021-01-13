using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SVEUCILISNA_KNJIZNICA.Models;
using SVEUCILISNA_KNJIZNICA.ViewModels;

namespace SVEUCILISNA_KNJIZNICA.Controllers
{
    public class TransakcijaController : Controller
    {
        private Entities db = new Entities();


        // GET: Transakcija
        public ActionResult Index()
        {
            /*List<TransakcijeIndexVm> model = db.Transakcijas.Select(x => new TransakcijeIndexVm
            {
                Datum = x.DatumTransakcije,
                Knjiga=x.Knjiga.Naziv,
                Korisnik=x.Korisnik.Ime+" "+x.Korisnik.Prezime+" ("+x.Korisnik.Mejl+")",
                TransakcijaId=x.TransakcijaID,
                Barkod=x.Knjiga.Barkod

            }).ToList();
            //var transakcijas = db.Transakcijas.Include(t => t.Knjiga).Include(t => t.Korisnik);
            return View(model);*/
            List<TransakcijeIndexVm> model1 = db.Transakcijas
                        .Join(
                            db.Transakcija_Audit,
                            k => k.TransakcijaID,
                            f => f.TransakcijaID,
                            (k, f) => new { Transakcija = k, Transakcija_Audit = f })
                        .Where(p => p.Transakcija_Audit.Status == "0").Select(x => new TransakcijeIndexVm
                        {
                            Datum = x.Transakcija.DatumTransakcije,
                            Knjiga = x.Transakcija.Knjiga.Naziv,
                            Korisnik = x.Transakcija.Korisnik.Ime + " " + x.Transakcija.Korisnik.Prezime + " (" + x.Transakcija.Korisnik.Mejl + ")",
                            TransakcijaId = x.Transakcija.TransakcijaID,
                            Barkod = x.Transakcija.Knjiga.Barkod

                        }).OrderByDescending(a => a.Datum).ToList();
            return View(model1);



        }



        public ActionResult IndexHistorije()
        {
            //var query = db.Transakcijas
            //            .Join(
            //                db.Transakcija_Audit,
            //                k=>k.TransakcijaID,
            //                f=>f.TransakcijaID,
            //                (k,f)=> new { Transakcija = k, Transakcija_Audit = f })
            //            .Where(p=>p.Transakcija_Audit.Status=="0").Select(x => new TransakcijeIndexVm
            //            {
            //                Datum = x.Transakcija.DatumTransakcije,
            //                Knjiga = x.Transakcija.Knjiga.Naziv,
            //                Korisnik = x.Transakcija.Korisnik.Ime + " " + x.Transakcija.Korisnik.Prezime + " (" + x.Transakcija.Korisnik.Mejl + ")",
            //                TransakcijaId = x.Transakcija.TransakcijaID,
            //                Barkod = x.Transakcija.Knjiga.Barkod

            //            }).OrderByDescending(a => a.Datum).ToList();


            List<TransakcijeIndexVm> model1 = db.Transakcijas
                        .Join(
                            db.Transakcija_Audit,
                            k => k.TransakcijaID,
                            f => f.TransakcijaID,
                            (k, f) => new { Transakcija = k, Transakcija_Audit = f })
                        .Where(p => p.Transakcija_Audit.Status != "0").Select(x => new TransakcijeIndexVm
                        {
                            Datum = x.Transakcija.DatumTransakcije,
                            Knjiga = x.Transakcija.Knjiga.Naziv,
                            Korisnik = x.Transakcija.Korisnik.Ime + " " + x.Transakcija.Korisnik.Prezime + " (" + x.Transakcija.Korisnik.Mejl + ")",
                            TransakcijaId = x.Transakcija.TransakcijaID,
                            Barkod = x.Transakcija.Knjiga.Barkod

                        }).OrderByDescending(a => a.Datum).ToList();

            //List<TransakcijeIndexVm> model = db.Transakcijas.Select(x => new TransakcijeIndexVm
            //{
            //    Datum = x.DatumTransakcije,
            //    Knjiga=x.Knjiga.Naziv,
            //    Korisnik=x.Korisnik.Ime+" "+x.Korisnik.Prezime+" ("+x.Korisnik.Mejl+")",
            //    TransakcijaId=x.TransakcijaID,
            //    Barkod=x.Knjiga.Barkod

            //}).OrderByDescending(a=>a.Datum).ToList();
            //var transakcijas = db.Transakcijas.Include(t => t.Knjiga).Include(t => t.Korisnik);
            return View(model1);
        }

        //razduzivanje knjige
        //razduzivanje knjige
        public ActionResult RazduziKnjigu(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Transakcija_Audit audit = db.Transakcija_Audit.Where(x => x.TransakcijaID == id).FirstOrDefault();

            audit.Status = "1";



            var trans = db.Transakcijas.Where(y => y.TransakcijaID == id).Select(y => y.KnjigaID).First();

            int i = Convert.ToInt32(trans);




            Zaliha zal = db.Zalihas.Where(x => x.KnjigaID == i).FirstOrDefault();

            zal.StanjeZaliha++;




            db.SaveChanges();


            return RedirectToAction("Index");
        }
        public ActionResult IndexForSpecificUser()
        {
            //Session["Email"] = k.Mejl;
            //Session["Id"] = k.KorisnikID;
            //Session["UlogaId"] = k.UlogaID;

            int id = (int)Session["Id"];

            List<TransakcijeIndexVm> model = db.Transakcijas
                .Where(a => a.KorisnikID == id)
                .Select(x => new TransakcijeIndexVm
                {
                    Datum = x.DatumTransakcije,
                    Knjiga = x.Knjiga.Naziv,
                    Korisnik = x.Korisnik.Ime + " " + x.Korisnik.Prezime + " (" + x.Korisnik.Mejl + ")",
                    TransakcijaId = x.TransakcijaID,
                    Barkod = x.Knjiga.Barkod

                })
                .ToList();
            //var transakcijas = db.Transakcijas.Include(t => t.Knjiga).Include(t => t.Korisnik);
            return View(model);
        }




        public ActionResult DodajKnjigu(KnjigaTransakcijaCreateVM knjiga)
        {
            bool isAdded = false;
            if (Session["cart"] == null)
            {
                List<KnjigaTransakcijaCreateVM> li = new List<KnjigaTransakcijaCreateVM>();
                li.Add(knjiga);
                Session["cart"] = li;
                ViewBag.cart = li.Count();
                Session["count"] = 1;
            }
            else
            {
                List<KnjigaTransakcijaCreateVM> li = (List<KnjigaTransakcijaCreateVM>)Session["cart"];
                KnjigaTransakcijaCreateVM provjera = li.Where(c => c.KnjigaID == knjiga.KnjigaID).FirstOrDefault();
                if (provjera == null)
                {
                    li.Add(knjiga);
                    Session["cart"] = li;
                    ViewBag.cart = li.Count();
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                    isAdded = true;
                }
            }
            return RedirectToAction("Create", isAdded);
        }

        public ActionResult UkloniKnjigu(KnjigaTransakcijaCreateVM knjiga)
        {
            List<KnjigaTransakcijaCreateVM> li = (List<KnjigaTransakcijaCreateVM>)Session["cart"];
            KnjigaTransakcijaCreateVM knjigaZaBrisati = li.Where(x => x.KnjigaID == knjiga.KnjigaID).FirstOrDefault();
            li.Remove(knjigaZaBrisati);

            Session["cart"] = li;
            ViewBag.cart = li.Count();
            Session["count"] = 1;

            return RedirectToAction("Create");
        }

        // GET: Transakcija/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transakcija transakcija = db.Transakcijas.Find(id);
            if (transakcija == null)
            {
                return HttpNotFound();
            }
            return View(transakcija);
        }

        // GET: Transakcija/Create


        public ActionResult Create(bool? error)
        {
            TransakcijaCreateVM transakcija = new TransakcijaCreateVM();
            transakcija.Korisnici = db.Korisniks.Select(x => new SelectListVm
            {
                id = x.KorisnikID,
                tekstZaPrikaz = x.Ime + " " + x.Prezime + " " + x.Mejl
            }).ToList();

            ViewBag.KorisnikId = new SelectList(db.Korisniks.Select(x => new SelectListVm
            {
                id = x.KorisnikID,
                tekstZaPrikaz = x.Ime + " " + x.Prezime+" "+x.Mejl
            }).ToList(), "id", "tekstZaPrikaz");

            transakcija.OdabraneKnjige = new List<KnjigaTransakcijaCreateVM>();
            transakcija.DostupneKnjige = new List<KnjigaTransakcijaCreateVM>();
            transakcija.KorisnikId = 0;

            transakcija.DostupneKnjige = db.Knjigas/*Where(z=>z.Zalihas.Select(k=>k.KnjigaID=k.KnjigaID)*/.Select(x => new KnjigaTransakcijaCreateVM
            {
                KnjigaID = x.KnjigaID,
                Autor = x.Autor,
                Barkod = x.Barkod,
                Naziv = x.Naziv
            }).ToList();

            
            transakcija.OdabraneKnjige = (List<KnjigaTransakcijaCreateVM>)Session["cart"];
            if (transakcija.OdabraneKnjige == null)
                transakcija.OdabraneKnjige = new List<KnjigaTransakcijaCreateVM>();

            //ViewBag.KnjigaId = new SelectList(db.Knjige, "KnjigaId", "Naziv");
            //ViewBag.KorisnikId = new SelectList(db.Korisnici, "KorisnikId", "Ime");
            if (error == true)
                ViewBag.warrning = "Knjigu koju želite dodati već se nalazi u listi odabranih knjiga!";

            return View(transakcija);
        }

        

        // POST: Transakcija/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KorisnikId")] TransakcijaCreateVM transakcije)
        {

            List<KnjigaTransakcijaCreateVM> li = (List<KnjigaTransakcijaCreateVM>)Session["cart"];
            if (li != null)
            {

                foreach (KnjigaTransakcijaCreateVM item in li)
                {
                    Transakcija tNova = new Transakcija();
                    tNova.DatumTransakcije = DateTime.Now;
                    tNova.KnjigaID = item.KnjigaID;
                    //tNova.KorisnikID = Session["korisnikId"];
                    tNova.KorisnikID = transakcije.KorisnikId;
                    db.Transakcijas.Add(tNova);

                    Zaliha temp = db.Zalihas.Where(x => x.KnjigaID == item.KnjigaID).FirstOrDefault();
                    temp.StanjeZaliha--;

                }
                db.SaveChanges();
                Session["cart"] = null;
                return RedirectToAction("Index");
            }
            else
            {
                TransakcijaCreateVM transakcija = new TransakcijaCreateVM();
                transakcija.OdabraneKnjige = new List<KnjigaTransakcijaCreateVM>();
                transakcija.DostupneKnjige = new List<KnjigaTransakcijaCreateVM>();
                transakcija.KorisnikId = 0;

                transakcija.DostupneKnjige = db.Knjigas/*Where(z=>z.Zalihas.Select(k=>k.KnjigaID=k.KnjigaID)*/.Select(x => new KnjigaTransakcijaCreateVM
                {
                    KnjigaID = x.KnjigaID,
                    Autor = x.Autor,
                    Barkod = x.Barkod,
                    Naziv = x.Naziv
                }).ToList();

                transakcija.OdabraneKnjige = (List<KnjigaTransakcijaCreateVM>)Session["cart"];
                return View(transakcije);
            }
        }

        public JsonResult GetSearchValue(string search)
        {
     
            List<Korisnik> allsearch = db.Korisniks.Where(x => x.Ime.StartsWith(search)).Select(x => new Korisnik
            {
               
                Ime = x.Ime
            }).ToList();
            return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // POST: Transakcija/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateR(Transakcija transkacija)
        {
        
            return View();
        }


   

        // GET: Transakcija/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transakcija transakcija = db.Transakcijas.Find(id);
            if (transakcija == null)
            {
                return HttpNotFound();
            }
            ViewBag.KnjigaID = new SelectList(db.Knjigas, "KnjigaID", "Naziv", transakcija.KnjigaID);
            ViewBag.KorisnikID = new SelectList(db.Korisniks, "KorisnikID", "Ime", transakcija.KorisnikID);
            return View(transakcija);
        }

        // POST: Transakcija/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransakcijaID,DatumTransakcije,KorisnikID,KnjigaID")] Transakcija transakcija)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transakcija).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KnjigaID = new SelectList(db.Knjigas, "KnjigaID", "Naziv", transakcija.KnjigaID);
            ViewBag.KorisnikID = new SelectList(db.Korisniks, "KorisnikID", "Ime", transakcija.KorisnikID);
            return View(transakcija);
        }

        // GET: Transakcija/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transakcija transakcija = db.Transakcijas.Find(id);
            if (transakcija == null)
            {
                return HttpNotFound();
            }
            return View(transakcija);
        }

        // POST: Transakcija/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transakcija transakcija = db.Transakcijas.Find(id);
            db.Transakcijas.Remove(transakcija);
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
