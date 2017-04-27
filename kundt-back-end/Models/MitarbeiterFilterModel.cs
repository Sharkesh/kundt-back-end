using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kundt_back_end.Models
{
    public class MitarbeiterFilterModel
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public int? MaId { get; set; }
        public string Anrede { get; set; }
    }
}