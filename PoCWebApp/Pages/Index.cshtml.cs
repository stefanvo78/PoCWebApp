using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PoCWebApp.Context;
using PoCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace PoCWebApp.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public int Standort { get; set; }

        [BindProperty]
        public string Message { get; set; }

        [BindProperty]
        public DateTime Date { get; set; }

        public List<SAPData> SAPData { get; set; } 

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            SAPData = new List<SAPData>();
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {

        }
        public void OnPostSearch()
        {
            using (var db = new SAPContext())
            {
                SAPData = db.SapData
                                        .Where( sd => sd.Standortwerk == this.Standort && 
                                                (( sd.SAPStoerungsbeginn != null && sd.SAPStoerungsbeginn >= Date.AddHours(-8)) ||
                                                 (sd.SAPStoerungsbeginn == null && sd.ProperEreignisbeginn !=null && sd.ProperEreignisbeginn >= Date.AddHours(-8))))
                                        .ToList();
            }
        }
    }
}
