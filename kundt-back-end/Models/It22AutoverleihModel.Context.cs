﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace kundt_back_end.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class it22AutoverleihEntities : DbContext
    {
        public it22AutoverleihEntities()
            : base("name=it22AutoverleihEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<tblAusstattung> tblAusstattung { get; set; }
        public virtual DbSet<tblAuto> tblAuto { get; set; }
        public virtual DbSet<tblBuchung> tblBuchung { get; set; }
        public virtual DbSet<tblEyecatcher> tblEyecatcher { get; set; }
        public virtual DbSet<tblHistorie> tblHistorie { get; set; }
        public virtual DbSet<tblKategorie> tblKategorie { get; set; }
        public virtual DbSet<tblKunde> tblKunde { get; set; }
        public virtual DbSet<tblLand> tblLand { get; set; }
        public virtual DbSet<tblLogin> tblLogin { get; set; }
        public virtual DbSet<tblMarke> tblMarke { get; set; }
        public virtual DbSet<tblMitarbeiter> tblMitarbeiter { get; set; }
        public virtual DbSet<tblPLZOrt> tblPLZOrt { get; set; }
        public virtual DbSet<tblTreibstoff> tblTreibstoff { get; set; }
        public virtual DbSet<tblTyp> tblTyp { get; set; }
    
        public virtual ObjectResult<BuchungAbholung_Result> BuchungAbholung()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<BuchungAbholung_Result>("BuchungAbholung");
        }
    
        public virtual ObjectResult<BuchungProblem_Result> BuchungProblem()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<BuchungProblem_Result>("BuchungProblem");
        }
    
        public virtual ObjectResult<BuchungRueckgabe_Result> BuchungRueckgabe()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<BuchungRueckgabe_Result>("BuchungRueckgabe");
        }
    
        public virtual int BuchungUpdate(Nullable<int> id, Nullable<System.DateTime> buchungvon, Nullable<System.DateTime> buchungbis, string buchungstatus, Nullable<bool> storno, Nullable<bool> versicherung)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var buchungvonParameter = buchungvon.HasValue ?
                new ObjectParameter("buchungvon", buchungvon) :
                new ObjectParameter("buchungvon", typeof(System.DateTime));
    
            var buchungbisParameter = buchungbis.HasValue ?
                new ObjectParameter("buchungbis", buchungbis) :
                new ObjectParameter("buchungbis", typeof(System.DateTime));
    
            var buchungstatusParameter = buchungstatus != null ?
                new ObjectParameter("buchungstatus", buchungstatus) :
                new ObjectParameter("buchungstatus", typeof(string));
    
            var stornoParameter = storno.HasValue ?
                new ObjectParameter("storno", storno) :
                new ObjectParameter("storno", typeof(bool));
    
            var versicherungParameter = versicherung.HasValue ?
                new ObjectParameter("versicherung", versicherung) :
                new ObjectParameter("versicherung", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("BuchungUpdate", idParameter, buchungvonParameter, buchungbisParameter, buchungstatusParameter, stornoParameter, versicherungParameter);
        }
    
        public virtual ObjectResult<OffeneBuchungenTodayPlus13_Result> OffeneBuchungenTodayPlus13()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<OffeneBuchungenTodayPlus13_Result>("OffeneBuchungenTodayPlus13");
        }
    
        public virtual int pKundeEdit(Nullable<int> idkunde, string vorname, string nachname, string strasse, string plz, string ort, string email, string telefon, string passnr, Nullable<System.DateTime> gebdat)
        {
            var idkundeParameter = idkunde.HasValue ?
                new ObjectParameter("idkunde", idkunde) :
                new ObjectParameter("idkunde", typeof(int));
    
            var vornameParameter = vorname != null ?
                new ObjectParameter("vorname", vorname) :
                new ObjectParameter("vorname", typeof(string));
    
            var nachnameParameter = nachname != null ?
                new ObjectParameter("nachname", nachname) :
                new ObjectParameter("nachname", typeof(string));
    
            var strasseParameter = strasse != null ?
                new ObjectParameter("strasse", strasse) :
                new ObjectParameter("strasse", typeof(string));
    
            var plzParameter = plz != null ?
                new ObjectParameter("plz", plz) :
                new ObjectParameter("plz", typeof(string));
    
            var ortParameter = ort != null ?
                new ObjectParameter("ort", ort) :
                new ObjectParameter("ort", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var telefonParameter = telefon != null ?
                new ObjectParameter("telefon", telefon) :
                new ObjectParameter("telefon", typeof(string));
    
            var passnrParameter = passnr != null ?
                new ObjectParameter("passnr", passnr) :
                new ObjectParameter("passnr", typeof(string));
    
            var gebdatParameter = gebdat.HasValue ?
                new ObjectParameter("gebdat", gebdat) :
                new ObjectParameter("gebdat", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pKundeEdit", idkundeParameter, vornameParameter, nachnameParameter, strasseParameter, plzParameter, ortParameter, emailParameter, telefonParameter, passnrParameter, gebdatParameter);
        }
    
        public virtual ObjectResult<pKundenAnzeigen_Result> pKundenAnzeigen(string searchName, Nullable<int> searchKundenNr, string searchOrt, string searchPLZ, string searchBuchung)
        {
            var searchNameParameter = searchName != null ?
                new ObjectParameter("searchName", searchName) :
                new ObjectParameter("searchName", typeof(string));
    
            var searchKundenNrParameter = searchKundenNr.HasValue ?
                new ObjectParameter("searchKundenNr", searchKundenNr) :
                new ObjectParameter("searchKundenNr", typeof(int));
    
            var searchOrtParameter = searchOrt != null ?
                new ObjectParameter("searchOrt", searchOrt) :
                new ObjectParameter("searchOrt", typeof(string));
    
            var searchPLZParameter = searchPLZ != null ?
                new ObjectParameter("searchPLZ", searchPLZ) :
                new ObjectParameter("searchPLZ", typeof(string));
    
            var searchBuchungParameter = searchBuchung != null ?
                new ObjectParameter("searchBuchung", searchBuchung) :
                new ObjectParameter("searchBuchung", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pKundenAnzeigen_Result>("pKundenAnzeigen", searchNameParameter, searchKundenNrParameter, searchOrtParameter, searchPLZParameter, searchBuchungParameter);
        }
    
        [DbFunction("it22AutoverleihEntities", "BuchungFilter")]
        public virtual IQueryable<BuchungFilter_Result> BuchungFilter(Nullable<int> searchBuchungNr, string searchName, Nullable<int> searchKundeNr, string searchOrt, string searchPLZ, Nullable<bool> checkOffen, Nullable<bool> checkAbgeschlossen, Nullable<bool> checkProblem)
        {
            var searchBuchungNrParameter = searchBuchungNr.HasValue ?
                new ObjectParameter("searchBuchungNr", searchBuchungNr) :
                new ObjectParameter("searchBuchungNr", typeof(int));
    
            var searchNameParameter = searchName != null ?
                new ObjectParameter("searchName", searchName) :
                new ObjectParameter("searchName", typeof(string));
    
            var searchKundeNrParameter = searchKundeNr.HasValue ?
                new ObjectParameter("searchKundeNr", searchKundeNr) :
                new ObjectParameter("searchKundeNr", typeof(int));
    
            var searchOrtParameter = searchOrt != null ?
                new ObjectParameter("searchOrt", searchOrt) :
                new ObjectParameter("searchOrt", typeof(string));
    
            var searchPLZParameter = searchPLZ != null ?
                new ObjectParameter("searchPLZ", searchPLZ) :
                new ObjectParameter("searchPLZ", typeof(string));
    
            var checkOffenParameter = checkOffen.HasValue ?
                new ObjectParameter("checkOffen", checkOffen) :
                new ObjectParameter("checkOffen", typeof(bool));
    
            var checkAbgeschlossenParameter = checkAbgeschlossen.HasValue ?
                new ObjectParameter("checkAbgeschlossen", checkAbgeschlossen) :
                new ObjectParameter("checkAbgeschlossen", typeof(bool));
    
            var checkProblemParameter = checkProblem.HasValue ?
                new ObjectParameter("checkProblem", checkProblem) :
                new ObjectParameter("checkProblem", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<BuchungFilter_Result>("[it22AutoverleihEntities].[BuchungFilter](@searchBuchungNr, @searchName, @searchKundeNr, @searchOrt, @searchPLZ, @checkOffen, @checkAbgeschlossen, @checkProblem)", searchBuchungNrParameter, searchNameParameter, searchKundeNrParameter, searchOrtParameter, searchPLZParameter, checkOffenParameter, checkAbgeschlossenParameter, checkProblemParameter);
        }
    
        public virtual int pAutoHinzufuegen(Nullable<short> baujahr, string pS, string getriebe, string tueren, Nullable<byte> sitze, Nullable<decimal> mietpreis, Nullable<decimal> verkaufPreis, Nullable<decimal> kilometerstand, byte[] autobild, Nullable<bool> anzeigen, string treibstoff, string typ, string kategorie)
        {
            var baujahrParameter = baujahr.HasValue ?
                new ObjectParameter("Baujahr", baujahr) :
                new ObjectParameter("Baujahr", typeof(short));
    
            var pSParameter = pS != null ?
                new ObjectParameter("PS", pS) :
                new ObjectParameter("PS", typeof(string));
    
            var getriebeParameter = getriebe != null ?
                new ObjectParameter("Getriebe", getriebe) :
                new ObjectParameter("Getriebe", typeof(string));
    
            var tuerenParameter = tueren != null ?
                new ObjectParameter("Tueren", tueren) :
                new ObjectParameter("Tueren", typeof(string));
    
            var sitzeParameter = sitze.HasValue ?
                new ObjectParameter("Sitze", sitze) :
                new ObjectParameter("Sitze", typeof(byte));
    
            var mietpreisParameter = mietpreis.HasValue ?
                new ObjectParameter("Mietpreis", mietpreis) :
                new ObjectParameter("Mietpreis", typeof(decimal));
    
            var verkaufPreisParameter = verkaufPreis.HasValue ?
                new ObjectParameter("VerkaufPreis", verkaufPreis) :
                new ObjectParameter("VerkaufPreis", typeof(decimal));
    
            var kilometerstandParameter = kilometerstand.HasValue ?
                new ObjectParameter("Kilometerstand", kilometerstand) :
                new ObjectParameter("Kilometerstand", typeof(decimal));
    
            var autobildParameter = autobild != null ?
                new ObjectParameter("Autobild", autobild) :
                new ObjectParameter("Autobild", typeof(byte[]));
    
            var anzeigenParameter = anzeigen.HasValue ?
                new ObjectParameter("Anzeigen", anzeigen) :
                new ObjectParameter("Anzeigen", typeof(bool));
    
            var treibstoffParameter = treibstoff != null ?
                new ObjectParameter("Treibstoff", treibstoff) :
                new ObjectParameter("Treibstoff", typeof(string));
    
            var typParameter = typ != null ?
                new ObjectParameter("Typ", typ) :
                new ObjectParameter("Typ", typeof(string));
    
            var kategorieParameter = kategorie != null ?
                new ObjectParameter("Kategorie", kategorie) :
                new ObjectParameter("Kategorie", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pAutoHinzufuegen", baujahrParameter, pSParameter, getriebeParameter, tuerenParameter, sitzeParameter, mietpreisParameter, verkaufPreisParameter, kilometerstandParameter, autobildParameter, anzeigenParameter, treibstoffParameter, typParameter, kategorieParameter);
        }
    
        public virtual int pAusstattungZuAuto2(Nullable<int> daten)
        {
            var datenParameter = daten.HasValue ?
                new ObjectParameter("daten", daten) :
                new ObjectParameter("daten", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pAusstattungZuAuto2", datenParameter);
        }
    
        public virtual int pMitarbeiterEditieren(ObjectParameter iDLogin, string email, string passwort, string rolle, Nullable<bool> deaktiviert, string mAVorname, string mANachname, string mAAnrede)
        {
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var passwortParameter = passwort != null ?
                new ObjectParameter("Passwort", passwort) :
                new ObjectParameter("Passwort", typeof(string));
    
            var rolleParameter = rolle != null ?
                new ObjectParameter("Rolle", rolle) :
                new ObjectParameter("Rolle", typeof(string));
    
            var deaktiviertParameter = deaktiviert.HasValue ?
                new ObjectParameter("Deaktiviert", deaktiviert) :
                new ObjectParameter("Deaktiviert", typeof(bool));
    
            var mAVornameParameter = mAVorname != null ?
                new ObjectParameter("MAVorname", mAVorname) :
                new ObjectParameter("MAVorname", typeof(string));
    
            var mANachnameParameter = mANachname != null ?
                new ObjectParameter("MANachname", mANachname) :
                new ObjectParameter("MANachname", typeof(string));
    
            var mAAnredeParameter = mAAnrede != null ?
                new ObjectParameter("MAAnrede", mAAnrede) :
                new ObjectParameter("MAAnrede", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pMitarbeiterEditieren", iDLogin, emailParameter, passwortParameter, rolleParameter, deaktiviertParameter, mAVornameParameter, mANachnameParameter, mAAnredeParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> pNeuenMitarbeiterAnlegen(string email, string passwort, Nullable<bool> deaktiviert, string mAVorname, string mANachname, string mAAnrede)
        {
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var passwortParameter = passwort != null ?
                new ObjectParameter("Passwort", passwort) :
                new ObjectParameter("Passwort", typeof(string));
    
            var deaktiviertParameter = deaktiviert.HasValue ?
                new ObjectParameter("Deaktiviert", deaktiviert) :
                new ObjectParameter("Deaktiviert", typeof(bool));
    
            var mAVornameParameter = mAVorname != null ?
                new ObjectParameter("MAVorname", mAVorname) :
                new ObjectParameter("MAVorname", typeof(string));
    
            var mANachnameParameter = mANachname != null ?
                new ObjectParameter("MANachname", mANachname) :
                new ObjectParameter("MANachname", typeof(string));
    
            var mAAnredeParameter = mAAnrede != null ?
                new ObjectParameter("MAAnrede", mAAnrede) :
                new ObjectParameter("MAAnrede", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("pNeuenMitarbeiterAnlegen", emailParameter, passwortParameter, deaktiviertParameter, mAVornameParameter, mANachnameParameter, mAAnredeParameter);
        }
    }
}
