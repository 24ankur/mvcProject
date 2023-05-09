using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLogic.ViewModels
{
    public enum Accessibility
    {
        Public,
        Private
    }

    public class EventViewModel
    {
        public int EventId { get; set; }
        public int UserId { get; set; }


        [Required]
        [Display(Name = "Title Of the Book")]
        public string TitleOfTheBook { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }


        [Required]
        public string Location { get; set; }


        [Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        public Accessibility Type { get; set; }

        [Display(Name = "Duration in Hours")]
        [Range(1, 4)]
        public int? DurationInHours { get; set; }

        [MaxLength(50)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Other Details")]
        public string OtherDetails { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Invite By Emails", Prompt = "Space Seperated Emails")]
        public string InviteByEmails { get; set; }
    }
}
