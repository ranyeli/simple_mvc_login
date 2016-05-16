using SimpleLogInSystem.Core.CoreDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using SimpleLogInSystem.Core.CoreDb.Entities;
using SimpleLogInSystem.Core.Interfaces;

namespace SimpleLogInSystem.Core.Services
{
    public class UserValidationService
    {
        public virtual bool UserExistence(IUserModel cred)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            var exists = false;
            var user = GetUser(cred);

            if (user != null)
            {
                if (user.Password == crypto.Compute(cred.Password, user.PasswordSalt))
                {
                    exists = true;
                }
            }

            return exists;
        }
        public bool IsUserValid(ModelStateDictionary state, IUserModel user)
        {
            var isValid = false;

            if (state.IsValid)
            {
                if (UserExistence(user))
                    isValid = true;
            }
            else
                state.AddModelError("", "Log In data is incorrect.");

            return isValid;
        }

        public User GetUser(IUserModel user)
        {
            using (var db = new CoreDbContext())
            {
                var result = db.Users.FirstOrDefault(u => u.Email == user.Email);

                return result;
            }

        }
    }
}
