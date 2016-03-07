using LiveStreamProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LiveStreamProject.Controllers
{

    public class HomeController : Controller
    {
        LiveStreamDBEntities DB = new LiveStreamDBEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserProfile()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.Message = "Login";

            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModelcs user)
        {
            if (ModelState.IsValid)
            {
                if (IsValid(user.Email, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Login Data is incorrect.");
                }
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.Message = "Registration";

            return View();
        }

        [HttpPost]
        public ActionResult Register(UserModelcs user)
        {
            if (ModelState.IsValid)
            {
                //try { 

                var crypto = new SimpleCrypto.PBKDF2();
                var encrypt = crypto.Compute(user.Password);
                var sysuser = DB.LoginTBLs.Create();

                sysuser.UserID = Guid.NewGuid();
                sysuser.Email = user.Email;
                sysuser.Password = encrypt;
                sysuser.Password = crypto.Salt;
                sysuser.UserType = UserModelcs.UserTypes.User.ToString();

                DB.LoginTBLs.Add(sysuser);

                //var profile = DB.ProfileTBLs.Create();

                //profile.Created = DateTime.Now;
                //profile.IsActive = true;

                //DB.ProfileTBLs.Add(profile);

                DB.SaveChanges();

                //}
                //catch (DbEntityValidationException dbEx)
                //{
                //    foreach (var validationErrors in dbEx.EntityValidationErrors)
                //    {
                //        foreach (var validationError in validationErrors.ValidationErrors)
                //        {
                //            Trace.TraceInformation("Property: {0} Error: {1}",
                //                                    validationError.PropertyName,
                //                                    validationError.ErrorMessage);
                //        }
                //    }
                //}

                //int ID = (from i in DB.ProfileTBLs select i.ProfileID).Last();

                //string querystr = "UPDATE LoginTBL SET ProfileID=@ID WHERE Email=@sysuser.UserID";

                //DB.ProfileTBLs.SqlQuery(querystr);
                //DB.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Test(test a)
        {
            var test = DB.Tests.Create();

            a.Test = test.Test1;
            DB.Tests.Add(test);
            DB.SaveChanges();

            return RedirectToAction("Index", "Home");

        }

        public ActionResult Stream()
        {
            return View();
        }

        public ActionResult Chat()
        {
            return View();
        }

        public ActionResult Chatworking()
        {
            return View();
        }


        private bool IsValid(string email, string password)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            bool IsValid = false;

            using (var db = new LiveStreamDBEntities())
            {
                var user = db.LoginTBLs.FirstOrDefault(u => u.Email == email);

                if (user != null)
                {
                    if (user.Password == crypto.Compute(password, user.PasswordSalt))
                    {
                        IsValid = true;
                    }
                }
            }
            return IsValid;
        }
    }
}