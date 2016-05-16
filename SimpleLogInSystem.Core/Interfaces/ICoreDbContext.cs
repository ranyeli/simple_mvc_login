using SimpleLogInSystem.Core.CoreDb.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLogInSystem.Core.Interfaces
{
    public interface ICoreDbContext
    {
       DbSet<User> Users { get; set; }
        int SaveChanges();
    }
}
