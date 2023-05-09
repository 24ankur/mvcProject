using BussinessLogic;
using SharedLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace UserInterface.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
    

        // GET: Event/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DetailsViewModel eventViewModel = new DetailsViewModel();

            eventViewModel.Events = new EventLogic().getEventDetails(id);

            eventViewModel.comments = new CommentLogic().GetComments(id);

            bool verifyForInvitedUsers = new EventLogic().verifyInvitedUsers(Convert.ToInt32(Session["UserId"]),id);

            if (eventViewModel.Events.UserId == Convert.ToInt32(Session["UserId"]) || eventViewModel.Events.Type == Accessibility.Public || User.Identity.Name=="myadmin@bookevents.com" || verifyForInvitedUsers)
            {
                return View(eventViewModel);
            }
            else
            {
                return View("NotFound");
            }
        }
        

        // GET: Event/Create
        public ActionResult Create()
        {
            if (User.Identity.Name != "myadmin@bookevents.com")
                return View();
            else return View("NotFound");
        }

        // POST: Event/Create
        // Done
        [HttpPost]
        public ActionResult Create(EventViewModel requestedEvent)
        {
            if (ModelState.IsValid)
            {
                int result = new EventLogic().CreateEvent(requestedEvent, Convert.ToInt32(Session["UserId"]));
                if (result > 0)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Could Not Create Event. Please Try Again ! ");
                }
            }
            return View();
        }

        // GET: Event/Edit/5
        public ActionResult Edit(int? id,string invitedto)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            EventViewModel eventViewModel = new EventLogic().getEventDetails(id);
            if ((eventViewModel.UserId == Convert.ToInt32(Session["UserId"]) || User.Identity.Name == "myadmin@bookevents.com") && eventViewModel.Date > DateTime.Now)
            {
                return View(eventViewModel);
            }
            else
            {
                return View("NotFound");
            }

        }

        // POST: Event/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "UserId,EventId")] EventViewModel newEventData, int id,string invitedto)
        {
            if (ModelState.IsValid)
            {
                newEventData.EventId = id;
                newEventData.UserId = Convert.ToInt32(Session["UserId"]);
                new EventLogic().EditEvent(newEventData,invitedto);
            }
            else
            {
                ModelState.AddModelError("", "Wait ! Something is Wrong");
                return View();
            }
            return RedirectToAction("MyEvents", "Event");
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            EventViewModel eventViewModel = new EventLogic().getEventDetails(id);
            if (eventViewModel.UserId == Convert.ToInt32(Session["UserId"]) || User.Identity.Name == "myadmin@bookevents.com")
            {
                return View(eventViewModel);
            }
            else
            {
                return View("NotFound");
            }
        }

        // POST: Event/Delete/5
        [HttpPost]
        public ActionResult Delete(int id )
        {
            new EventLogic().DeleteEvent(id);
            
            return RedirectToAction("MyEvents", "Event");
        }


        public ActionResult MyEvents()
        {
            List<EventViewModel> userEvents = new EventLogic().GetEvents(Convert.ToInt32(Session["UserId"]));
            return View(userEvents);
        }

        public ActionResult Invitations()
        {
            List<EventViewModel> userInvitations = new EventLogic().GetInvitations(Convert.ToInt32(Session["UserId"]));
            return View(userInvitations);
        }
       



    }
}
