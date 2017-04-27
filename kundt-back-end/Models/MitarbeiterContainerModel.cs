using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace kundt_back_end.Models
{
    public class MitarbeiterContainerModel
    {
        public List<pMAAnzeigen_Result> malist { get; set; }
        public MitarbeiterFilterModel mafilter { get; set; }
    }
}