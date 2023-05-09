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
        //somechangesI want to check in my repo on git
        public int Id { get; set; }
        public int EventId { get; set; }

        //will create new branch then i will commit this code
        public string Text { get; set; }
        public string Email { get; set; }
        public DateTime timeStamp { get; set; }
    }
}
