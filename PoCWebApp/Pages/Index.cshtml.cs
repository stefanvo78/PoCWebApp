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
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;

namespace PoCWebApp.Pages
{
    public class TableModel
    {
        public bool Chosen { get; set; }
    }
    
    public class IndexModel : PageModel
    {
        private static readonly HttpClient client = new HttpClient();

        [BindProperty]
        public int Standort { get; set; }

        [BindProperty]
        public string Message { get; set; }

        [BindProperty]
        public DateTime Date { get; set; }

        [BindProperty]
        public double? MinPropability { get; set; }

        [BindProperty]
        public List<TableModel> TableRows { get; set; }

        [BindProperty]
        public List<SAPData> SAPDataList { get; set; }

        private IConfigurationRoot ConfigRoot;

        private readonly SAPContext _SAPContext;

        private readonly ILogger<IndexModel> _logger;
        
        public List<string> Standorte; 

        public IndexModel(ILogger<IndexModel> logger, SAPContext sapcontext, IConfiguration configRoot)
        {
            
            ConfigRoot = (IConfigurationRoot)configRoot;
            _SAPContext = sapcontext;
            _logger = logger;
            
            if ( SAPDataList == null)
                SAPDataList = new List<SAPData>();

            Standorte = new List<string>();
            Standorte.Add("1005");
            Standorte.Add("1006");
            Standorte.Add("1007");
            Standorte.Add("1008");
            Standorte.Add("1009");
            Standorte.Add("1080");
            Standorte.Add("1110");
            Standorte.Add("1180");
            Standorte.Add("1181");
        }

        public void OnGet()
        {
            
        }

        public void OnPostSave()
        {
            
        }
        public async void OnPostSearch()
        {
            
            ViewData["message"] = this.Message;
            ViewData["date"] = this.Date == DateTime.MinValue ? "" : this.Date.ToString();
            ViewData["standort"] = this.Standort;
            ViewData["minpropability"] = this.MinPropability;
            
            List <SAPData> temp = new List<SAPData>();

            try
            {


                string body = "{\"data\": [\"" + this.Message  +"\"" + "," +  this.Standort +"]}";
                var content = new StringContent(body);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                if (!client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ConfigRoot["Token"]);
                }

                var mlModelURL = ConfigRoot["MLModelURL"];
                
                var response = client.PostAsync(mlModelURL, content).Result;

                var responseString = response.Content.ReadAsStringAsync().Result;
                string result = JsonConvert.DeserializeObject<string>(responseString);
                Dictionary<string, double> mapResult = JsonConvert.DeserializeObject<Dictionary<string, double>>(result);

                using (_SAPContext)
                {
                    temp = (List<SAPData>)_SAPContext.SapData
                            .Where(sd => sd.Standortwerk == this.Standort &&
                                    ((Date == DateTime.MinValue) ||
                                   ((sd.SAPStoerungsbeginn != null && (sd.SAPStoerungsbeginn >= Date.AddHours(-8) && sd.SAPStoerungsbeginn <= Date.AddHours(8))) ||
                                       (sd.SAPStoerungsbeginn == null && sd.ProperEreignisbeginn != null && (sd.ProperEreignisbeginn >= Date.AddHours(-8) && sd.ProperEreignisbeginn <= Date.AddHours(8))))
                                       ))
                            .ToList();
                }


                HashSet<string> added = new HashSet<string>();
                int len = temp.Count;
                for (int i = len - 1; i >= 0; i--)
                {
                    if (!added.Contains(temp[i].KKS))
                    {
                        if (mapResult.ContainsKey(temp[i].KKS) && 
                            ( MinPropability == null || ( MinPropability != null && mapResult[temp[i].KKS] >= MinPropability / 100)))
                        {
                            temp[i].Propability = mapResult[temp[i].KKS] * 100;
                            added.Add(temp[i].KKS);
                        }
                        else
                        {
                            temp.RemoveAt(i);
                        }
                    }
                    else
                    {
                        temp.RemoveAt(i);
                    }
                }



                SAPDataList.AddRange(temp.OrderByDescending(d => d.Propability));
                
            }
            catch ( Exception ex)
            {
                ViewData["error"] = ex.Message + Environment.NewLine + ex.ToString();
            }

           
        }
    }
}
