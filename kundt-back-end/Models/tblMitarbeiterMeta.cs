using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kundt_back_end.Models
{
    [MetadataType(typeof(tblMitarbeiterMeta))]
    public partial class tblMitarbeiter
    {

    }

    public class tblMitarbeiterMeta
    {
        [Display(Name = "Vorname")]
        public string MAVorname { get; set; }
        [Display(Name = "Nachname")]
        public string MANachname { get; set; }
        [Display(Name = "Anrede")]
        public string MAAnrede { get; set; }
    }
}