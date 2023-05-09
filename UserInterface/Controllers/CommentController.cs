using BussinessLogic;
using SharedLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UserInterface.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }

        

        [HttpPost]
        public ActionResult LeaveComment(CommentsViewModel comment)
        {
            JsonResult result = new JsonResult();
            if (ModelState.IsValid)
            {
                try
                {
                    CommentsViewModel LogicResult = new CommentLogic().UploadComment(User.Identity.Name, comment);
                    result.Data = new { Success = LogicResult };
                }

                catch (Exception ex)
                {
                    result.Data = new { Success = false, Message = ex.Message };
                }
                return result;
            }
            else
            {
                ModelState.AddModelError("", "Can't add Empty Comment");
                result.Data = new { Success = false};
                return View();
            }
        }
    }
}




