using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLogic.ViewModels
{
    public class CommentsViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Can't Add Empty Comments")]
        [MinLength(3)]
        public string Text { get; set; }

        public int EventId { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Email { get; set; }
    }
}
