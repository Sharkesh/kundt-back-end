using System;
using System.Data;
using System.Data.Entity;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.DynamicData;


namespace kundt_back_end.Models
{

    public class KundeEditModel : it22AutoverleihEntities
    {
        //fertig soweit
         [Required]
        public int idkunde { get; set; }

        [Required(ErrorMessage = "Bitte Vorname eingeben")]
        [MinLength(2, ErrorMessage = "Vorname muss mindesten 2 Zeichen enthalten")]
        [MaxLength(100, ErrorMessage = "Vorname darf nicht mehr als 100 Zeichen enthalten")]
        [RegularExpression("^[a-zA-Z''-'\\s]{1,40}$", ErrorMessage = "Bitte nur Buchstaben eingeben")]
        public string Vorname { get; set; }

        [Required(ErrorMessage = "Bitte Nachname eingeben")]
        [MinLength(2, ErrorMessage = "Nachname muss min. 2 Buchstaben haben")]
        [MaxLength(100, ErrorMessage = "Nachname darf nicht mehr als 100 Zeichen enthalten")]
        [RegularExpression("^[a-zA-Z''-'\\s]{1,40}$", ErrorMessage = "Bitte nur Buchstaben eingeben")]
        public string Nachname { get; set; }

        [Required(ErrorMessage = "Bitte Straßenname eingeben")]
        [MinLength(2, ErrorMessage = "Straßenname muss mindesten 2 Zeichen enthalten")]
        [MaxLength(100, ErrorMessage = "Straßenname darf nicht mehr als 100 Zeichen enthalten")]
        public string Straße { get; set; }

        [Required(ErrorMessage = "Bitte PLZ eingeben")]
        [Range(0, 999999, ErrorMessage = "Bitte eine gültige PLZ eingeben")]
        [MinLength(2, ErrorMessage = "PLZ muss min. 2 Zeichen haben")]
        [MaxLength(30, ErrorMessage = "PLZ darf nicht mehr als 30 Zeichen enthalten")]
        public string Plz { get; set; }
       

        public string Ort { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Bitte Telefonnummer eingeben")]
        [MinLength(2, ErrorMessage = "Telefonnummer muss mindesten 2 Zeichen enthalten")]
        [MaxLength(100, ErrorMessage = "Telefonnummer darf nicht mehr als 100 Zeichen enthalten")]
        [Phone(ErrorMessage = "Bitte Telefonnummer in anderer Form eingeben")]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Bitte Passnummer eingeben")]
        [MinLength(2, ErrorMessage = "Passnummer muss mindesten 2 Zeichen enthalten")]
        [MaxLength(100, ErrorMessage = "Passnummer darf nicht mehr als 100 Zeichen enthalten")]
        [RegularExpression("^(?!^0+$)[a-zA-Z0-9]{3,20}$", ErrorMessage = "Bitte eine gültige Passnummer eingeben")]
        public string PassNr { get; set; }

        [Required(ErrorMessage = "Bitte Passnummer eingeben")]
        //[RegularExpression("^(?!^0+$)[a-zA-Z0-9]{3,20}$", ErrorMessage = "Bitte Geburtstag eingeben (Z.b.: )")]
        public System.DateTime GebDat { get; set; }


    }

   
}