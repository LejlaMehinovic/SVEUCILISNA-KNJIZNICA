using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using SVEUCILISNA_KNJIZNICA.Models;
using SVEUCILISNA_KNJIZNICA.ViewModels;

namespace SVEUCILISNA_KNJIZNICA.Controllers
{
    public class AccountController : Controller
    {
        private Entities db = new Entities();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(KorisnikRegistracija objKorisnikModel)
        {
            if (ModelState.IsValid)
            {
                Korisnik objKorisnik = new Korisnik();
                
                objKorisnik.Ime = objKorisnikModel.Ime;
                objKorisnik.Prezime = objKorisnikModel.Prezime;
                objKorisnik.Mejl = objKorisnikModel.Mejl;
                objKorisnik.Lozinka = objKorisnikModel.Lozinka;
                objKorisnik.UlogaID = 3;
                //objKorisnik.UlogaID = objKorisnikModel.UlogaID = 3;
                db.Korisniks.Add(objKorisnik);
                try { db.SaveChanges(); }
                
                
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        Exception raise = dbEx;
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                string message = string.Format("{0}:{1}",
                                    validationErrors.Entry.Entity.ToString(),
                                    validationError.ErrorMessage);
                                // raise a new exception nesting
                                // the current instance as InnerException
                                raise = new InvalidOperationException(message, raise);
                            }
                        }
                        throw raise;
                    }
                
                objKorisnikModel.SuccesMessage = "Uspješna registracija";
                return RedirectToAction("Index", "Home");
            }


            return View();
        }

        public ActionResult Login()
        {
            LoginModel login=new LoginModel();
            return View(login);
        }


        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {

            
            if (ModelState.IsValid)
            {
                Korisnik k = db.Korisniks.Where(m => m.Mejl == loginModel.Mejl && m.Lozinka == loginModel.Lozinka)
                    .FirstOrDefault();

                if (k == null)
                {
                    ModelState.AddModelError("Error", "Nepostojeći E-mail ili lozinka");
                    return View();
                }

                Session["Email"] = k.Mejl;
                Session["Id"] = k.KorisnikID;
                Session["UlogaId"] = k.UlogaID;


                if (loginModel.Mejl == "anaa@unipu.hr" || loginModel.Mejl == "ivaivic@unipu.hr")
                    return RedirectToAction("Index", "Knjiznicar");
                

                return RedirectToAction("Index", "Student");

            }
            return View();
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}