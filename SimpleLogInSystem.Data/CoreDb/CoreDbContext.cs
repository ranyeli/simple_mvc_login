using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SimpleLogInSystem.Data.CoreDb.Entities;
using System.Configuration;

namespace SimpleLogInSystem.Data.CoreDb
{
    public class CoreDbContext:DbContext
    {
       public DbSet<User> Users { get; set; }

        public CoreDbContext():base("name=coreConnection")
        {
            //Database.Connection.ConnectionString = ConfigurationManager.AppSettings["connectionString"];
        }
    }
}
