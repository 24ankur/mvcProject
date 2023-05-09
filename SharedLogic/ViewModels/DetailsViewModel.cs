using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLogic.ViewModels
{
    public class DetailsViewModel
    {
        public EventViewModel Events { get; set; }

        public IEnumerable<CommentsViewModel> comments { get; set; }
    }
}
