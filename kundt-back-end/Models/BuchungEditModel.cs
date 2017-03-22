using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kundt_back_end.Models
{
    public class BuchungEditModel
    {
        public BuchungEditModel()
        {
            options = new SelectList(new[] { "erstellt", "abgeholt", "zurueck" });
        }
        /// Abklären ob die Anotationen nötig sind oder ob man sie weglassen kann??
        public int IDBuchung { get; set; }
        public System.DateTime BuchungAm { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public System.DateTime BuchungVon { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public System.DateTime BuchungBis { get; set; }
        public Nullable<int> Tage { get; set; }
        public bool Versicherung { get; set; }
        public int FKKunde { get; set; }
        public int FKAuto { get; set; }
        public string BuchungStatus { get; set; }
        public bool Storno { get; set; }
        public SelectList options { get; set; }
        public decimal MietPreis { get; set; }
    }
}