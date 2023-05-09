using DataAccess;
using DataAccess.Models;
using SharedLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic
{
    public class CommentLogic
    {
        public CommentsViewModel UploadComment(string UserEmailId , CommentsViewModel comment)
        {
            CommentModel userComment= new CommentModel()
            {
                Email = UserEmailId,
                Text = comment.Text,
                EventId = comment.EventId,
                timeStamp = DateTime.Now
            };

            CommentModel databaseComment = new DatabaseOperations().AddCommentToDatabase(userComment);

            return comment;
        }

        public List<CommentsViewModel> GetComments(int? id)
        {
            List<CommentModel> listOfCommentsFromDatabase   = new DatabaseOperations().GetCommentsFromDb(id);

            List<CommentsViewModel> listOfCommentsViewModel = new List<CommentsViewModel>();

            CommentsViewModel viewModelComment;

            foreach(var currentComment in listOfCommentsFromDatabase)
            {
                viewModelComment = new CommentsViewModel()
                {
                    Email = currentComment.Email,
                    Text = currentComment.Text,
                    TimeStamp = currentComment.timeStamp,
                    EventId = currentComment.EventId
                };
                listOfCommentsViewModel.Add(viewModelComment);
            }

            return listOfCommentsViewModel;
        }

    }
}
