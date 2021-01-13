using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SVEUCILISNA_KNJIZNICA.Models
{
    public class LoginModel
    {
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Lozinka je obavezna.")]
        [Display(Name = "E-mail: ")]
        public string Mejl { get; set; }
        [Display(Name = "Lozinka: ")]
        [DataType(DataType.Password)]
        public string Lozinka { get; set; }
        public int Id { get; set; }
    }
}