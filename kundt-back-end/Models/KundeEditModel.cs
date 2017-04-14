using System;
using System.Data;
using System.Data.Entity;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.DynamicData;


namespace kundt_back_end.Models
{

    public class KundeEditModel
    {
        //Nicht fertig - Endl

        [Required]
        public int idkunde { get; set; }

        [Required(ErrorMessage = "Bitte Vorname eingeben")]
        [MinLength(2, ErrorMessage = "Vorname muss mindesten 2 Zeichen enthalten")]
        [MaxLength(100, ErrorMessage = "Vorname darf nicht mehr als 100 Zeichen enthalten")]
        [RegularExpression("^[a-zA-Z''-'\\s]{1,40}$", ErrorMessage = "Bitte nur Buchstaben eingeben")]
        public string Vorname { get; set; }

        [Required(ErrorMessage = "Bitte Nachname eingeben")]
        [MinLength(2, ErrorMessage = "Nachname muss min. 2 Buchstaben haben")]
        [MaxLength(100, ErrorMessage = "Nachname darf nicht mehr als 100 Zeichen enthalten")]
        public string Nachname { get; set; }

        [Required(ErrorMessage = "Bitte Straßenname eingeben")]
        [MinLength(2, ErrorMessage = "Straßenname muss mindesten 2 Zeichen enthalten")]
        [MaxLength(100, ErrorMessage = "Straßenname darf nicht mehr als 100 Zeichen enthalten")]
        public string Straße { get; set; }

        [Required(ErrorMessage = "Bitte PLZ eingeben")]
        [MinLength(2, ErrorMessage = "PLZ muss min. 2 Buchstaben haben")]
        [MaxLength(30, ErrorMessage = "PLZ darf nicht mehr als 30 Zeichen enthalten")]
        //[RegularExpression("^(\\d{5}-\\d{4}|\\d{5}|\\d{9})$|^([a-zA-Z]\\d[a-zA-Z] \\d[a-zA-Z]\\d)$", ErrorMessage = "Bitte nur Zahlen eingeben")] - noch nicht richtig
        public string Plz { get; set; }

        public string Ort { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Bitte Telefonnummer eingeben")]
        [MinLength(2, ErrorMessage = "Telefonnummer muss mindesten 2 Zeichen enthalten")]
        [MaxLength(100, ErrorMessage = "Telefonnummer darf nicht mehr als 100 Zeichen enthalten")]
        [Phone(ErrorMessage = "Bitte Telefonnummer in anderer Form eingeben")]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Bitte Passnummer eingeben")]
        [MinLength(2, ErrorMessage = "Passnummer muss mindesten 2 Zeichen enthalten")]
        [MaxLength(100, ErrorMessage = "Passnummer darf nicht mehr als 100 Zeichen enthalten")]
        [RegularExpression("^(?!^0+$)[a-zA-Z0-9]{3,20}$", ErrorMessage = "Bitte eine gültige Passnummer eingeben")]
        public string PassNr { get; set; }

        [Required(ErrorMessage = "Bitte Passnummer eingeben")]
        //[RegularExpression("^(?!^0+$)[a-zA-Z0-9]{3,20}$", ErrorMessage = "Bitte Geburtstag eingeben (Z.b.: )")]
        public System.DateTime GebDat { get; set; }

        //??
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)]

    }

    //[MetadataType(typeof(KundeEditModelMetadata))]
    //public partial class KundeEditModelllll
    //{
    //    partial void OnContextCreated()
    //    {
    //        this.SavingChanges += new System.EventHandler(OnSavingChanges);
    //    }

    //    public void OnSavingChanges(KundeEditModel kundChange, System.EventArgs e)
    //    {
    //        var stateManager = ((KundeEditModel) kundChange).ObjectStateManager;
    //        var changedEntities =
    //            ObjectStateManager.GetObjectStateEntries(EntityState.Modified);
    //        foreach (ObjectStateEntry stateEntryEntity in
    //            changedEntities)
    //        {
    //            if (stateEntryEntity.Entity is Product)
    //            {
    //                Product prod = (Product) stateEntryEntity.Entity;
    //                rValidateDate(prod.SellStartDate);
    //                rValidateDate(prod.SellEndDate);
    //                rValidateDate(prod.DiscontinuedDate);
    //            }
    //        }
    //    }

    //    void ValidateVorname(KundeEditModel value)
    //    {
    //        string Vorname = (string) value;
    //        if (Vorname.Length <= 0)
    //            throw new ValidationException("Bitte geben Sie einen Vornamen ein");
    //    }

    //    public class KundeEditModelMetadata
    //    {
    //        [Required(AllowEmptyStrings = false)] public string Vorname;
    //    }

    //}
}