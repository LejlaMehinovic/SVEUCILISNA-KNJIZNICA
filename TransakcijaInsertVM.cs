using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SVEUCILISNA_KNJIZNICA.Models;

namespace SVEUCILISNA_KNJIZNICA.ViewModels
{
    public class TransakcijaInsertVM
    {
        public int KorisnikID { get; set; }
        public List<TransakcijaCreateVM> KnjigeList { get; set; }
    }
}