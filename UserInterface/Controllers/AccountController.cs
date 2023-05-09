using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BussinessLogic;
using SharedLogic.ViewModels;

namespace UserInterface.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult SignUp()
        {
            return View("SignUp");
        }


        [HttpPost]
        public ActionResult SignUp(UserViewModel requestedUser)
        {
            if (ModelState.IsValid)
            {
                bool userCreated = new UserLogic().CreateUser(requestedUser);
                if (userCreated)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("","Email Already Exists !");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }


        public ActionResult Login()
        {
            return View("Login");
        }


        [HttpPost]
        public ActionResult Login([Bind(Include = "Email,Password")]UserViewModel requestedUser)
        {
            int userLogin = new UserLogic().LoginUser(requestedUser);

            if(userLogin != -1)
            {
                FormsAuthentication.SetAuthCookie(requestedUser.Email, false);
                Session["UserId"] = userLogin;
                return RedirectToAction("MyEvents","Event");
                //return RedirectToAction("Index","Home");
            }

            else
            {
                ModelState.AddModelError("","Invalid User !");
            }
            return View();
        }


        public ActionResult SignOut()
        {
            //HttpContext.Request.Cookies.Remove();
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }

    }
}