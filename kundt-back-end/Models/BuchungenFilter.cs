using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kundt_back_end.Models
{
    public class BuchungenFilter
    {       
        public int? idbuchung { get; set; }
        public int? idkunde { get; set; }
        public string nachname { get; set; }
        public string ort { get; set; }
        public string plz { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime buchungVon { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime buchungBis { get; set; }
        public string checkStatus { get; set; }
    }
}