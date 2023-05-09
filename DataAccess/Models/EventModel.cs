using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public enum Accessibility
    {
        Public,
        Private
    }

    public class EventModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string TitleOfTheBook { get; set; }

        public DateTime Date { get; set; }

        public string Location { get; set; }

        public DateTime StartTime { get; set; }

        public Accessibility Type { get; set; }

        public int? DurationInHours { get; set; }

        public string Description { get; set; }

        public string OtherDetails { get; set; }

        public string InviteByEmails { get; set; }

        public virtual ICollection<UserModel> Users { get; set; }
    }
}
