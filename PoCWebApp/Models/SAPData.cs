using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoCWebApp.Models
{
    [Keyless]
    public class SAPData
    {
        public int Standortwerk { get; set; }
        public string KKS { get; set; }
        public string KKSBezeichnung { get; set; }
        public DateTime? SAPStoerungsbeginn { get; set; }
        public DateTime? ProperEreignisbeginn { get; set; }

    }
}
