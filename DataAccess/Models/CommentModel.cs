using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string Text { get; set; }
        public string Email { get; set; }
        public DateTime timeStamp { get; set; }
    }
}
