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
    }
}
