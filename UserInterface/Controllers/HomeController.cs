using BussinessLogic;
using SharedLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace UserInterface.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<EventViewModel> Events;
            if (Session["UserId"] != null)
            {
                Events = new EventLogic().getAllEvents(Convert.ToInt32(Session["UserId"]));
            }
            else
            {
                Events = new EventLogic().getAllEvents(null);
            }

            return View(Events);
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EventViewModel eventViewModel = new EventLogic().getEventDetails(id);

            bool SessionFlag = Session["UserId"] == null ? true : false;
            if (SessionFlag)
            {
                if (eventViewModel.Type == Accessibility.Public)
                {
                    return View(eventViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                if (eventViewModel.Type == Accessibility.Public || Convert.ToInt32(Session["UserId"]) == eventViewModel.UserId)
                {
                    return RedirectToAction("Details/" + eventViewModel.EventId, "Event");
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View("About");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize(Users = "myadmin@bookevents.com")]
        public ActionResult AllEvents()
        {
            List<EventViewModel> AllEventsInDb = new EventLogic().getAllEventsAdmin();
            return View(AllEventsInDb);
        }

        public ActionResult Support()
        {
            return Redirect("http://helpdesk.nagarro.com/");
        }
    }
}