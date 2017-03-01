using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kundt_back_end.Models
{
    public class BuchungsUebersicht
    {
        public List<tblBuchung> buchungAbholung { get; set; }
        public List<tblBuchung> buchungRueckgabe { get; set; }
        public List<tblBuchung> buchungProblem { get; set; }
    }
}