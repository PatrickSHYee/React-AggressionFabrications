using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using React_AggressionFabrications.Models;

namespace React_AggressionFabrications.Controllers
{
    public class FAQController : Controller
    {
        private static readonly IList<CommentModel> _comments;

        static FAQController()
        {
            _comments = new List<CommentModel>()
             {
                new CommentModel
                {
                    Id = 1,
                    Author = "Daniel Lo Nigro",
                    Text = "Hello ReactJS.NET World!"
                },
                new CommentModel
                {
                    Id = 2,
                    Author = "Pete Hunt",
                    Text = "This is one comment"
                },
                new CommentModel
                {
                    Id = 3,
                    Author = "Jordan Walke",
                    Text = "This is *another* comment"
                },
            };
        }

        [Route("comments")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Comments()
        {
            return Json(_comments);
        }

        // Adding new comments
        [Route("comments/new")]
        [HttpPost]
        public ActionResult AddComment(CommentModel comment)
        {
            comment.Id = _comments.Count + 1;
            _comments.Add(comment);
            return Content("Success <?=o)~");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
