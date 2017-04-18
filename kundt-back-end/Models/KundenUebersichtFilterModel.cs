using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Web;

namespace kundt_back_end.Models
{
    public class KundenUebersichtFilterModel 
    {

        public string KundenName { get; set; }
        public int? KundenNr { get; set; }
        public string Plz { get; set; }
        public string Ort { get; set; }

        public string Buchungsstatus { get; set; }
        

        public List<string> BuStat =  new List<string>()
        {
            "Alle",
            "Erstellt",
            "Abgeholt",
            "Zurueck"
        };

      




    }
}