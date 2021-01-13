using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVEUCILISNA_KNJIZNICA.ViewModels
{
    public class TransakcijeIndexVm
    {
        public int TransakcijaId { get; set; }
        public string Korisnik { get; set; }
        public string Knjiga { get; set; }
        public string Barkod { get; set; }
        public DateTime Datum { get; set; }
        
    }
}