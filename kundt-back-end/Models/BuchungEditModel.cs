using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kundt_back_end.Models
{
    public class BuchungEditModel
    {
        public int IDBuchung { get; set; }        
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public System.DateTime BuchungVon { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public System.DateTime BuchungBis { get; set; }
        public bool Versicherung { get; set; }
        public int FKKunde { get; set; }
        public int FKAuto { get; set; }
        public string BuchungStatus { get; set; }
        public bool Storno { get; set; }
    }
}