using BLL.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPL.Infrastructure.Mappers;
using MvcPL.ViewModels;
using BLL.Interface.Entities;

namespace MvcPL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContentSearchService _searchService;
        private readonly IUserService _userService;
        private readonly IContentService _contentService;

        public HomeController(IUserService userService, IContentService contentService, IContentSearchService searchService)
        {
            _userService = userService;
            _contentService = contentService;
            _searchService = searchService;
        }
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View(_contentService.GetTop(10).Select(x => x?.ToContentViewModel()));
        }
        public ActionResult Search(string input)
        {
            return View("Index",_searchService.Search(input).Select(x => _contentService.GetContentById(x).ToContentViewModel()));
        }
        public ActionResult Content(int id)
        {
            var content = _contentService.GetContentById(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            ViewBag.IsUserVoted = User.Identity.IsAuthenticated ?
                    _userService.UserVoted(User.Identity.Name, content) : true;

            return View(content.ToContentViewModel());
        }
        public ActionResult UserInfo(string name)
        {
            var user = _userService.GetUserEntity(name);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user.ToUserViewModel());
        }
        public ActionResult Users()
        {
            return PartialView("_AllUsers", _userService.GetAllUserEntities().Select(x => x.ToUserViewModel()).OrderBy(x => x.AverageRating));
        }
        [HttpGet]
        public ActionResult CreateContent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateContent(ContentViewModel content)
        {
            var bllContent = content.ToBllContent();
            bllContent.UserId = _userService.GetUserEntity(User.Identity.Name).Id;
             _contentService.Create(bllContent);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult CreateComment(string commentText, int contentId)
        {

            var comment = new CommentEntity()
            {
                Date = DateTime.Now,
                Text = commentText,
                User = _userService.GetUserEntity(User.Identity.Name),
                ContentId = contentId
            };
            _contentService.Create(comment);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Comments", _contentService.GetContentById(contentId).ToContentViewModel().Comments.OrderBy(x => x.Date));
            }
            return RedirectToAction("Content", new { id = contentId });
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int id)
        {
            var content = _contentService.GetContentById(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content.ToContentViewModel());
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(ContentViewModel content)
        {
            _contentService.Update(content.ToBllContent());
            return RedirectToAction("Content", new { Id = content.Id });
        }

        public ActionResult Delete(int id)
        {
            ContentEntity content = _contentService.GetContentById(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            _contentService.Delete(content);
            return RedirectToAction("Index", "Home", null);
        }

        public ActionResult DeleteComment(int id)
        {
            CommentEntity comment = _contentService.GetComment(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            _contentService.Delete(comment);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Comments", _contentService.GetContentById(comment.ContentId).ToContentViewModel().Comments);
            }
            return RedirectToAction("Content", new { id = comment.ContentId });
        }
        [HttpGet]
        public ActionResult EditComment(int id)
        {
            CommentEntity comment = _contentService.GetComment(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return PartialView("_EditComment", comment.ToCommentViewModel());
        }
        [HttpPost]
        public ActionResult EditComment(int id, string text)
        {
            CommentEntity comment = new CommentEntity() { Id = id, Text = text };
            _contentService.Update(comment);
            if (Request.IsAjaxRequest())
            {
                var temp = _contentService.GetContentById(comment.ContentId).ToContentViewModel().Comments;
                return PartialView("_Comments", temp);
            }
            return RedirectToAction("Content", new { id = comment.ContentId });
        }
        public ActionResult Create()
        {
            return View("Create");
        }
        public ActionResult Create(ContentViewModel content)
        {
            if (content != null)
            {
                var temp = _contentService.Create(content.ToBllContent());
                return RedirectToAction("Content", new { Id = temp.Id });
            }
            return HttpNotFound();
        }
        public ActionResult Like(ContentViewModel content)
        {
            var rating = _contentService.UpVoteContent(content.ToBllContent(), _userService.GetUserEntity(User.Identity.Name));
            if (Request.IsAjaxRequest())
            {
                return Content(rating.ToString() + "<style> #voting {display:none} </style > ", "text/html");
            }
            return RedirectToAction("Content", new { id = content.Id });
        }
        public ActionResult Dislike(ContentViewModel content)
        {
            var rating = _contentService.DownVoteContent(content.ToBllContent(), _userService.GetUserEntity(User.Identity.Name));
            if (Request.IsAjaxRequest())
            {
                return Content(rating.ToString() + "<style> #voting {display:none} </style > ", "text/html");
            }
            return RedirectToAction("Content", new { id = content.Id });
        }
    }
}
