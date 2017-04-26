using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Web;

namespace kundt_back_end.Models
{
    public class KundenUebersichtFilterModel 
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public int? KundenNr { get; set; }
        public string Plz { get; set; }
        public string Ort { get; set; }
    }
}