using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kundt_back_end.Models
{
    public class KundeEditModel
    {
        //Nicht fertig - Endl
       
        public int idkunde { get; set; }

        public string Vorname { get; set; }

        public string Nachname { get; set; }

        public string Straße { get; set; }

        public string Plz { get; set; }

        public  string Ort { get; set; }

        public string Email { get; set; }
        public string Telefon { get; set; }
        public string PassNr { get; set; }
        public System.DateTime GebDat { get; set; }

        //??
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)]

    }
    
}