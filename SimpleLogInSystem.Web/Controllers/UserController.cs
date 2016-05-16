using SimpleLogInSystem.Core.CoreDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleLogInSystem.Core.CoreDb.Entities;
using System.Web.Security;
using System.Data.Entity;
using SimpleLogInSystem.Core.Services;

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
            var validate = new UserValidationService();

            if (!validate.IsUserValid(ModelState, user)) return View();

            FormsAuthentication.SetAuthCookie(user.Email, true);
            return RedirectToAction("Index", "Home");
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
    }
}