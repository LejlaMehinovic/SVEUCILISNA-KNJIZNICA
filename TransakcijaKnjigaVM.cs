using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVEUCILISNA_KNJIZNICA.ViewModels
{
    public class TransakcijaKnjigaVM
    {
        public int KnjigaId { get; set; }
        public string Naziv { get; set; }
        public string Autor { get; set; }
        public int TrenutnaZaliha { get; set; }
    }
}