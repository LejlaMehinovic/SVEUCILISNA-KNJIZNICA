using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVEUCILISNA_KNJIZNICA.ViewModels
{
    public class KnjigaTransakcijaCreateVM
    {
        public int KnjigaID { get; set; }
        public string Naziv { get; set; }
        public string Barkod { get; set; }
        public string Autor { get; set; }
    }
}