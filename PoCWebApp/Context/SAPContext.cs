using Microsoft.EntityFrameworkCore;
using PoCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoCWebApp.Context
{
    public class SAPContext: DbContext
    {
        public DbSet<SAPData> SapData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=tcp:rwepocsqlserver.database.windows.net,1433;Initial Catalog=dbRWEPoC;Persist Security Info=False;User ID=stefan;Password=RWEPoC123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    }
}
