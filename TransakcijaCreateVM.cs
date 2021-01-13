using SVEUCILISNA_KNJIZNICA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVEUCILISNA_KNJIZNICA.ViewModels
{
    public class TransakcijaCreateVM
    {
        public int KorisnikId { get; set; }
        public List<SelectListVm> Korisnici { get; set; }
        public List<KnjigaTransakcijaCreateVM> DostupneKnjige { get; set; }

        public List<KnjigaTransakcijaCreateVM> OdabraneKnjige { get; set; }
    }
}