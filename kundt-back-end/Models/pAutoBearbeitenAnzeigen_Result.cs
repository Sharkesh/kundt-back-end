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
    
    public partial class pAutoBearbeitenAnzeigen_Result
    {
        public int IDAuto { get; set; }
        public string PS { get; set; }
        public short Baujahr { get; set; }
        public decimal Kilometerstand { get; set; }
        public string Getriebe { get; set; }
        public string Tueren { get; set; }
        public byte Sitze { get; set; }
        public decimal MietPreis { get; set; }
        public decimal VerkaufPreis { get; set; }
        public byte[] AutoBild { get; set; }
        public bool Anzeigen { get; set; }
        public int FKKategorie { get; set; }
        public int FKTreibstoff { get; set; }
        public int FKTyp { get; set; }
    }
}
