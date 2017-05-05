using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kundt_back_end.Models
{
    public class BuchungenCM
    {
        public List<GefilterteBuchungen> buchungenlist { get; set; }
        public BuchungenFilter filtermodel { get; set; }
    }
}