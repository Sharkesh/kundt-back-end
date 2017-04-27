using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace kundt_back_end.Models
{
    public class KundenUebersichtContainerModel
    {
        public ObjectResult<pKundenAnzeigen_Result> kundenlist { get; set; }
        public KundenUebersichtFilterModel filtermodel { get; set; }
    }
}