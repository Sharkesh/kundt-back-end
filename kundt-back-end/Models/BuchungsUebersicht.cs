using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kundt_back_end.Models
{
    /// Generiert ein neues Model zur verwendung in der Index View
    /// welches 3 Abfrageergebnise in Listen speichert.
    public class BuchungsUebersicht
    {
        public List<BuchungAbholung_Result> buchungAbholung { get; set; }
        public List<BuchungRueckgabe_Result> buchungRueckgabe { get; set; }
        public List<BuchungProblem_Result> buchungProblem { get; set; }
    }
}