using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kundt_back_end.Models
{
    public class BuchungSearchModel
    {
        
        public string buchungnummer { get; set; }
        public string name { get; set; }
        public string kundennr { get; set; }
        public string ort { get; set; }
        public string plz { get; set; }

        public bool offen { get; set; }
        public bool abgeschlossen { get; set; }
        public bool problem { get; set; }
        
    }
}