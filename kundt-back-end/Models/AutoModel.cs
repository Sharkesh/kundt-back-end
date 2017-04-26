using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace kundt_back_end.Models
{
    public class AutoModel
    {
        Regex r = new Regex("^[a-zA-Z0-9]*$");
        public List<tblAuto> autoListe { get; set; }
        public List<tblAusstattung> ausstattungListe { get; set; }
        public List<tblAusstattung> plainAusstattungListe { get; set; }
        public List<tblTyp> typListe { get; set; }
        public List<tblMarke> markeListe { get; set; }
        public List<tblKategorie> kategorieListe { get; set; }
        public List<tblTreibstoff> treibstoffListe { get; set; }
        public List<string> getriebeListe { get; set; }
        public string myGetriebe { get; set; }
        public string myMarke { get; set; }
        public string myTyp { get; set; }
        public string myKategorie { get; set; }
        public string myPS { get;set; }
        public int myBauJahr { get; set; }
        public int myBauJahrVon { get; set; }
        public int myBauJahrBis { get; set; }
        public string myTueren { get; set; }
        public int mySitze { get; set; }
        public decimal myMietPreis { get; set; }
        public decimal myVerkaufsPreis { get; set; }
        public decimal myKilometerStand { get; set; }
        public decimal myKilometerStandVon { get; set; }
        public decimal myKilometerStandBis { get; set; }
        public byte[] myAutobild { get; set; }
        public bool? myAnzeigen { get; set; }
        public string myTreibstoff { get; set; }
        public int myIDAuto { get; set; }
        public tblAusstattung myTblAusstattung { get; set; }
        public List<pAutoBearbeitenAnzeigen2_Result> autoBearbeiten { get; set; }
        public List<pAutoBearbeitenInklFilterFinal2_Result> autoBearbeitenFilter { get; set; }
    }
}