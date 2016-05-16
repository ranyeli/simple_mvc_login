using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLogInSystem.Core.Interfaces
{
    public interface IUserModel
    {
        string Email { get; set; }
        string Password { get; set; }
    }
}
