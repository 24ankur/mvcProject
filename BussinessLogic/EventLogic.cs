using DataAccess;
using DataAccess.Models;
using SharedLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace BussinessLogic
{
    public class EventLogic
    {
        public int CreateEvent(EventViewModel requestedEvent , int id)
        {
            EventModel NewEvent = new EventModel()
            {
                TitleOfTheBook = requestedEvent.TitleOfTheBook,
                Date = requestedEvent.Date,
                Location = requestedEvent.Location,
                StartTime = requestedEvent.StartTime,
                Type = (DataAccess.Models.Accessibility)requestedEvent.Type,
                DurationInHours = requestedEvent.DurationInHours,
                Description = requestedEvent.Description,
                OtherDetails = requestedEvent.OtherDetails,
                InviteByEmails = requestedEvent.InviteByEmails,
                UserId = id
            };

            int result = new DatabaseOperations().AddEventToDatabase(NewEvent);

            return result;
        }


        public List<EventViewModel> GetEvents(int userId)
        {
            EventViewModel userEvent; 

            List<EventModel> dbResult = new DatabaseOperations().MyEventsInDb(userId);

            List<EventViewModel> resultToController = new List<EventViewModel>();


            foreach(var currentEvent in dbResult)
            {
                userEvent = new EventViewModel()
                {
                    EventId = currentEvent.Id,
                    UserId = currentEvent.UserId,
                    TitleOfTheBook = currentEvent.TitleOfTheBook,
                    Date = currentEvent.Date,
                    Location = currentEvent.Location,
                    StartTime = currentEvent.StartTime,
                    Type = (SharedLogic.ViewModels.Accessibility)currentEvent.Type,
                    DurationInHours = currentEvent.DurationInHours,
                    Description = currentEvent.Description,
                    OtherDetails = currentEvent.OtherDetails,
                    InviteByEmails = currentEvent.InviteByEmails,
                };

                resultToController.Add(userEvent);
            }

            return resultToController;
        }


        public List<EventViewModel> getAllEvents(int? id)
        {
            EventViewModel userEvent;

            List<EventModel> dbResult = new DatabaseOperations().getEventsFromDB(id);

            List<EventViewModel> resultToController = new List<EventViewModel>();


            foreach (var currentEvent in dbResult)
            {
                userEvent = new EventViewModel()
                {
                    EventId = currentEvent.Id,
                    UserId = currentEvent.UserId,
                    TitleOfTheBook = currentEvent.TitleOfTheBook,
                    Date = currentEvent.Date,
                    Location = currentEvent.Location,
                    StartTime = currentEvent.StartTime,
                    Type = (SharedLogic.ViewModels.Accessibility)currentEvent.Type,
                    DurationInHours = currentEvent.DurationInHours,
                    Description = currentEvent.Description,
                    OtherDetails = currentEvent.OtherDetails,
                    InviteByEmails = currentEvent.InviteByEmails,
                };

                resultToController.Add(userEvent);
            }

            return resultToController;
        }


        public EventViewModel getEventDetails(int? id)
        {
            EventViewModel eventViewModel;
            EventModel currentEvent = new DatabaseOperations().getEventDetailsFromDB(id);
            if (currentEvent == null)
            {
                return null;
            }

            eventViewModel = new EventViewModel()
            {
                EventId = currentEvent.Id,
                UserId = currentEvent.UserId,
                TitleOfTheBook = currentEvent.TitleOfTheBook,
                Date = currentEvent.Date,
                Location = currentEvent.Location,
                StartTime = currentEvent.StartTime,
                Type = (SharedLogic.ViewModels.Accessibility)currentEvent.Type,
                DurationInHours = currentEvent.DurationInHours,
                Description = currentEvent.Description,
                OtherDetails = currentEvent.OtherDetails,
                InviteByEmails = currentEvent.InviteByEmails,
            };

            return eventViewModel;
        }

        public void EditEvent(EventViewModel requestedEvent, string invitedTo)
        {
            string[] newData = requestedEvent.InviteByEmails.Replace(" ", "").Split(',');
            string[] oldData = new string[] {""};
            if (invitedTo != null)
                oldData = invitedTo.Replace(" ", "").Split(',');

            var EmailIDsHS = new HashSet<string>(newData);
            var PreviousEmailIDs = new HashSet<string>(oldData);

            EventModel EditEvent = new EventModel()
            {
                TitleOfTheBook = requestedEvent.TitleOfTheBook,
                Date = requestedEvent.Date,
                Location = requestedEvent.Location,
                StartTime = requestedEvent.StartTime,
                Type = (DataAccess.Models.Accessibility)requestedEvent.Type,
                DurationInHours = requestedEvent.DurationInHours,
                Description = requestedEvent.Description,
                OtherDetails = requestedEvent.OtherDetails,
                InviteByEmails = requestedEvent.InviteByEmails,
                UserId = requestedEvent.UserId,
                Id = requestedEvent.EventId
            };

            new DatabaseOperations().EditEventInDb(EditEvent , EmailIDsHS , PreviousEmailIDs);
        }

        public void DeleteEvent(int id)
        {
            new DatabaseOperations().DelteEventInDb(id);
        }

        public List<EventViewModel> GetInvitations(int id)
        {
            List<EventModel> Invitations = new DatabaseOperations().GetInvitationsFromDb(id);

            EventViewModel userEvent;

            List<EventViewModel> resultToController = new List<EventViewModel>();


            foreach (var currentEvent in Invitations)
            {
                userEvent = new EventViewModel()
                {
                    EventId = currentEvent.Id,
                    UserId = currentEvent.UserId,
                    TitleOfTheBook = currentEvent.TitleOfTheBook,
                    Date = currentEvent.Date,
                    Location = currentEvent.Location,
                    StartTime = currentEvent.StartTime,
                    Type = (SharedLogic.ViewModels.Accessibility)currentEvent.Type,
                    DurationInHours = currentEvent.DurationInHours,
                    Description = currentEvent.Description,
                    OtherDetails = currentEvent.OtherDetails,
                    InviteByEmails = currentEvent.InviteByEmails,
                };

                resultToController.Add(userEvent);
            }

            return resultToController;
        }

        public List<EventViewModel> getAllEventsAdmin()
        {
            EventViewModel userEvent;

            List<EventModel> dbResult = new DatabaseOperations().AllEventsInDb();

            List<EventViewModel> resultToController = new List<EventViewModel>();


            foreach (var currentEvent in dbResult)
            {
                userEvent = new EventViewModel()
                {
                    EventId = currentEvent.Id,
                    UserId = currentEvent.UserId,
                    TitleOfTheBook = currentEvent.TitleOfTheBook,
                    Date = currentEvent.Date,
                    Location = currentEvent.Location,
                    StartTime = currentEvent.StartTime,
                    Type = (SharedLogic.ViewModels.Accessibility)currentEvent.Type,
                    DurationInHours = currentEvent.DurationInHours,
                    Description = currentEvent.Description,
                    OtherDetails = currentEvent.OtherDetails,
                    InviteByEmails = currentEvent.InviteByEmails,
                };

                resultToController.Add(userEvent);
            }

            return resultToController;
        }

        public bool verifyInvitedUsers(int UserId , int? EventId)
        {
            return new DatabaseOperations().VerifyInvitedUser(UserId,EventId);
        }
    }
}
