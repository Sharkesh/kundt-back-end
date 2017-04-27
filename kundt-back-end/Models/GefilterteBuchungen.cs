using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace kundt_back_end.Models
{
    public class GefilterteBuchungen
    {
        public const string SQL = "SELECT * FROM dbo.BuchungFilter(@idbuchung,@nachname,@idkunde,@ort,@plz,@buchungVon,@buchungBis,@offen,@abgeschlossen,@problem,@all)";

        public GefilterteBuchungen()
        {

        }
        public GefilterteBuchungen(SqlDataReader reader)
        {
            IDBuchung = reader.GetInt32(0);
            Vorname = reader.GetString(1);
            Nachname = reader.GetString(2);
            IDKunde = reader.GetInt32(3);
            Ort = reader.GetString(4);
            PLZ = reader.GetString(5);
            BuchungVon = reader.GetDateTime(6);
            BuchungBis = reader.GetDateTime(7);
            BuchungStatus = reader.GetString(8);
            Problem = reader.GetString(9);
        }

        public int IDBuchung { get; set; }

        public string Vorname { get; set; }

        public string Nachname { get; set; }

        public int IDKunde { get; set; }

        public string Ort { get; set; }

        public string PLZ { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BuchungVon { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BuchungBis { get; set; }

        public string BuchungStatus { get; set; }

        public string Problem { get; set; }

    }

}