using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kundt_back_end.Models
{
    [MetadataType(typeof(tblBuchungEditMeta))]
    public partial class tblBuchung
    {

    }

    public class tblBuchungEditMeta
    {
        [Display(Name = "Buchungsnummer")]
        public int IDBuchung { get; set; }
        public System.DateTime BuchungAm { get; set; }
        public System.DateTime BuchungVon { get; set; }
        public System.DateTime BuchungBis { get; set; }
        public bool Versicherung { get; set; }
        public int FKKunde { get; set; }
        public int FKAuto { get; set; }
        public Nullable<int> Tage { get; set; }
        public string BuchungStatus { get; set; }
        public bool Storno { get; set; }
    }
}