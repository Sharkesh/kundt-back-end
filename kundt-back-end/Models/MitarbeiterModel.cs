using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kundt_back_end.Models
{
    public class MitarbeiterModel
    {
        public MitarbeiterModel()
        { // Anrede Optionen geben die verschiedenen Angaben für das Dropdown an!
            AnredeOptionen = new SelectList(new[] { "Herr", "Frau" });
        }
        // Hier gibt es haufenweise Clienseitige Validierungen die durch [] angekündigt werden
        public int IDMitarbeiter { get; set; }

        [Display(Name ="Vorname")]
        [Required(ErrorMessage = "Bitte Vorname Eingebe")]
        [StringLength(100, MinimumLength = 2)]
        public string MAVorname { get; set; }

        [Display(Name ="Nachname")]
        [Required(ErrorMessage = "Bitte Nachname Eingeben")]
        [StringLength(100, MinimumLength = 2)]
        public string MANachname { get; set; }

        [Display(Name = "Anrede")]
        public string MAAnrede { get; set; }

        [Required(ErrorMessage = "Bitte Email Eingeben")]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Muss einer Email ähneln! bsp: test@user.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bitte Passwort Eingeben")]
        public string Passwort { get; set; }

        public string EmailAlt { get; set; }

        public SelectList AnredeOptionen { get; set; }

        public bool Deaktiviert { get; set; }

        public char Rolle { get; set; }
    }
}