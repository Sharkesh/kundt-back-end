//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class tblBuchung
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblBuchung()
        {
            this.tblHistorie = new HashSet<tblHistorie>();
        }
    
        public int IDBuchung { get; set; }
        public System.DateTime BuchungAm { get; set; }
        public System.DateTime BuchungVon { get; set; }
        public System.DateTime BuchungBis { get; set; }
        public bool Versicherung { get; set; }
        public int FKKunde { get; set; }
        public int FKAuto { get; set; }
        public Nullable<int> Tage { get; set; }
        public string BuchungStatus { get; set; }
        public bool Storno { get; set; }
    
        public virtual tblAuto tblAuto { get; set; }
        public virtual tblKunde tblKunde { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblHistorie> tblHistorie { get; set; }
    }
}
