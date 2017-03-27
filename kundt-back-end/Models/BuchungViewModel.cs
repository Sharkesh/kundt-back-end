using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kundt_back_end.Models
{
    public class BuchungViewModel
    {
        public int IDBuchung { get; set; }
        public int IDKunde { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string PLZ { get; set; }
        public string Ort { get; set; }
        public string BuchungStatus { get; set; }
    }
}