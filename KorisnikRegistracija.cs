using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SVEUCILISNA_KNJIZNICA.ViewModels
{
    public class KorisnikRegistracija
    {

        [Display(Name = "Ime: ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ime je obavezno.")]

        public string Ime { get; set; }

        [Display(Name = "Prezime: ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Prezime je obavezno.")]

        public string Prezime { get; set; }

        [Display(Name = "Lozinka: ")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Lozinka je obavezna.")]

        public string Lozinka { get; set; }

        [Display(Name = "E-mail: ")]
        [DataType(DataType.EmailAddress)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "E-mail je obavezan.")]

        public string Mejl { get; set; }
        public string SuccesMessage { get; set; }

        [Display(Name = "Potvrdi lozinku: ")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Potvrdite lozinku.")]
        [Compare("Lozinka", ErrorMessage = "Lozinka i potvrđena lozinka trebaju biti iste! ")]

        public string PotvrdiLozinku { get; set; }
    }
}