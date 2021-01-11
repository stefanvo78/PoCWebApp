using Microsoft.EntityFrameworkCore;
using PoCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace PoCWebApp.Context
{
    public class SAPContext : DbContext
    {
        private IConfiguration _configuration;

        public SAPContext(DbContextOptions<SAPContext> options)
            : base(options)
        {
        }


        public DbSet<SAPData> SapData { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
            //=> options.UseSqlServer("Server=tcp:sqlserverrwe.database.windows.net,1433;Initial Catalog=sap-proper;Persist Security Info=False;User ID=rwe;Password=Demo123$4567;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            //=> options.UseSqlServer("Server=tcp:rwesqlserver.database.windows.net,1433;Initial Catalog=sap-proper-a;Persist Security Info=False;User ID=ralf;Password=Demo123$4567;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            //=> options.UseSqlServer("Server=tcp:rwesqlserver.privatelink.database.windows.net,1433;Initial Catalog=sap-proper-a;Persist Security Info=False;User ID=ralf;Password=Demo123$4567;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

    
    }
}
