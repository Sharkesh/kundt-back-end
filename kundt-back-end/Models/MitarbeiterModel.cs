using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kundt_back_end.Models
{
    public class MitarbeiterModel
    {

        public int IDMitarbeiter { get; set; }
        [Required(ErrorMessage = "Bitte Vorname Eingebe")]
        [StringLength(100, MinimumLength = 2)]
        public string MitarbeiterVorname { get; set; }
        [Required(ErrorMessage = "Bitte Nachname Eingeben")]
        [StringLength(100, MinimumLength = 2)]
        public string MitarbeiterNachname { get; set; }
        public string MitarbeiterAnrede { get; set; }
        [Required(ErrorMessage = "Bitte Email Eingeben")]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Muss nach einer Email Schmecken!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Bitte Passwort Eingeben")]
        public string Passwort { get; set; }
        public bool Deaktiviert { get; set; }
    }
}