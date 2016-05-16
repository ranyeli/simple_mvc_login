using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SimpleLogInSystem.Core.CoreDb.Entities;
using System.Configuration;
using SimpleLogInSystem.Core.Interfaces;

namespace SimpleLogInSystem.Core.CoreDb
{
    public class CoreDbContext:DbContext
    {
       public virtual DbSet<User> Users { get; set; }

        public CoreDbContext():base("name=coreConnection")
        {
            //Database.Connection.ConnectionString = ConfigurationManager.AppSettings["connectionString"];
        }
    }
}
