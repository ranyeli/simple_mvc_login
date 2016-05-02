using SimpleLogInSystem.Data.CoreDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleLogInSystem.Data.CoreDb.Entities;
using System.Web.Security;
using System.Collections.Specialized;

namespace SimpleLogInSystem.Web.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(Models.UserModel user)
        {
            if (ModelState.IsValid)
            {
                if (IsValid(user.Email, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.Email, true);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Log In data is incorrect.");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(Models.UserModel user)
        {
            if (ModelState.IsValid)
            {
                using (var db = new CoreDbContext())
                {
                    var crypto = new SimpleCrypto.PBKDF2();
                    var encryPass = crypto.Compute(user.Password);
                    var sysUser = db.Users.Create();
                    sysUser.Email = user.Email;
                    sysUser.Password = encryPass;
                    sysUser.PasswordSalt = crypto.Salt;

                    db.Users.Add(sysUser);
                    db.SaveChanges();
                    FormsAuthentication.SetAuthCookie(user.Email, true);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        private bool IsValid(string email, string password)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            var isValid = false;

            using (var db = new CoreDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Email == email);

                if (user != null)
                {
                    if (user.Password == crypto.Compute(password, user.PasswordSalt))
                    {
                        isValid = true;
                    }
                }
            }

            return isValid;
        }
    }
}