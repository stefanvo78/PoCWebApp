using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        [NotMapped]
        public double? Propability { get; set; }


        public SAPData( 
            int Standortwerk, 
            string KKS, 
            string KKSBezeichnung, 
            DateTime? SAPStoerungsbeginn, 
            DateTime? ProperEreignisbeginn)
        {
            this.Standortwerk = Standortwerk;
            this.KKS = KKS;
            this.KKSBezeichnung = KKSBezeichnung;
            this.SAPStoerungsbeginn = SAPStoerungsbeginn;
            this.ProperEreignisbeginn = ProperEreignisbeginn;
            this.Propability = 0.0;
        }
    }
}
