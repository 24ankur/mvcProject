using DataAccess.Models;
using DataAccess.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DatabaseOperations
    {
        private DAL.ApplicationContext db = new DAL.ApplicationContext();


        // Users

        public int AddUserToDatabase(UserModel requstedUser)
        {
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;

            var FoundUser = db.Users.FirstOrDefault(x => x.Email == requstedUser.Email);

            if (FoundUser!=null)
            {
                return 0;
            }
            else
            {
                requstedUser.Password = AEScrypt.Encrypt(requstedUser.Password);
                db.Users.Add(requstedUser);
                return db.SaveChanges();
            }
        }

        public int VerifyLoginUser(UserModel requestedUser)
        {
            var FoundUser = db.Users.FirstOrDefault(x => x.Email == requestedUser.Email);

            if (FoundUser != null)
            {
                var UserPassword = AEScrypt.Decrypt(FoundUser.Password);
                if(UserPassword == requestedUser.Password)
                {
                    return FoundUser.Id;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }


        // Events

        public int AddEventToDatabase(EventModel requstedEvent)
        {
            if(requstedEvent.InviteByEmails == null)
            {
                db.Events.Add(requstedEvent);
                return db.SaveChanges();
            }
            
            string[] invitedTo = requstedEvent.InviteByEmails.Replace(" ", "").Split(',');
            requstedEvent.Users = new List<UserModel>();

            foreach(var email in invitedTo)
            {
                var result = (from user in db.Users where user.Email == email select user).FirstOrDefault();

                requstedEvent.Users.Add(result);
            }

            db.Events.Add(requstedEvent);
            return db.SaveChanges();
        }


        public List<EventModel> MyEventsInDb(int userId)
        {
            List<EventModel> myEvents = db.Events.Where(x => x.UserId == userId).OrderByDescending(x => x.Date).ToList();
            return myEvents;
        }

        public EventModel getEventDetailsFromDB(int? id)
        {
            EventModel MyEvent;
            MyEvent = db.Events.FirstOrDefault(x => x.Id == id);
            return MyEvent;
        }

        public void EditEventInDb(EventModel requestedEvent, HashSet<string> EmailIDsHS , HashSet<string> PreviousEmailIDs)
        {
            EventModel modelToEdit = db.Events.SingleOrDefault(x => x.Id == requestedEvent.Id);

            foreach (var user in db.Users)
            {

                if (EmailIDsHS.Contains(user.Email))
                {
                    if (!PreviousEmailIDs.Contains(user.Email))
                    {
                        modelToEdit.Users.Add(user);
                    }
                }
                else
                {
                    if (PreviousEmailIDs.Contains(user.Email))
                    {
                        modelToEdit.Users.Remove(user);
                    }
                }
            }

            db.Entry(modelToEdit).CurrentValues.SetValues(requestedEvent);
            db.SaveChanges();
        }

        public void DelteEventInDb(int id)
        {
            EventModel modelToDelete = db.Events.SingleOrDefault(x => x.Id == id);
            db.Events.Remove(modelToDelete);
            db.SaveChanges();
        }


        public List<EventModel> GetInvitationsFromDb(int id)
        {
            List<EventModel> Invitations = db.Users
                      .Where(c => c.Id == id)
                      .SelectMany(c => c.Events).ToList();

            return Invitations;
        }

        public bool VerifyInvitedUser(int UserId ,int? EventId)
        {
            var userInEvent = db.Events.Where(c => c.Id == EventId).SelectMany(e => e.Users);
            
            foreach(var user in userInEvent)
            {
                if (user.Id == UserId)
                {
                    return true;
                }
            }
            return false;
        }





        // For Home Controller

        public List<EventModel> getEventsFromDB(int? id)
        {
            List<EventModel> PublicEvents;

            if (id == null)
            {
                PublicEvents = db.Events.Where(x => x.Type == Accessibility.Public).ToList();
                return PublicEvents;
            }
            else
            {
                PublicEvents = db.Events.Where(x => x.Type == Accessibility.Public || x.UserId == id).ToList();
                return PublicEvents;
            }
        }


        // Comments Section

        public CommentModel AddCommentToDatabase(CommentModel userComment)
        {
            CommentModel comment = db.Comments.Add(userComment);
            db.SaveChanges();

            return comment;
        }

        public List<CommentModel> GetCommentsFromDb(int? id)
        {
            List<CommentModel> commentsList = db.Comments.Where(x => x.EventId == id).OrderByDescending(x => x.timeStamp).ToList();
            return commentsList;
        }

        public List<EventModel> AllEventsInDb()
        {
            List<EventModel> myEvents = db.Events.ToList();
            return myEvents;
        }

    }
}
