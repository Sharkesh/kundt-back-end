using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace kundt_back_end.Models
{
    public class GefilterteBuchungen
    {
        public const string SQL = "SELECT * FROM dbo.BuchungFilter(@idbuchung,@nachname,@idkunde,@ort,@plz,@offen,@abgeschlossen,@problem)";

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
            BuchungStatus = reader.GetString(6);
            Problem = reader.GetString(7);
        }

        public int IDBuchung { get; set; }

        public string Vorname { get; set; }

        public string Nachname { get; set; }

        public int IDKunde { get; set; }

        public string Ort { get; set; }

        public string PLZ { get; set; }

        public string BuchungStatus { get; set; }

        public string Problem { get; set; }

    }

}