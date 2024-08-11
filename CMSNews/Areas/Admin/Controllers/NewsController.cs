using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMSNews.Models.Context;
using CMSNews.Models.Models;
using CMSNews.Service.Service;
using CMSNews.Models.ViewModels;
using Auto_Mapper.App_Start;

namespace CMSNews.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        private DbCMSNewsContext db = new DbCMSNewsContext();
        private NewsGroupService _newsGroupService;
        private NewsService _newsService;
        private UserService _userService;
        public NewsController()
        {
            _newsService = new NewsService(db);
            _newsGroupService = new NewsGroupService(db);
            _userService = new UserService(db);
        }

        // GET: Admin/News
        public ActionResult Index()
        {
            var news = _newsService.GetAll();
            var newsViewModel = AutoMapperConfig.mapper.Map<IEnumerable<News>, List<NewsViewModel>>(news);
            return View(newsViewModel);
        }

        // GET: Admin/News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = _newsService.GetEntity(id.Value);
            if (news == null)
            {
                return HttpNotFound();
            }
            NewsViewModel newsViewModel = AutoMapperConfig.mapper.Map<News, NewsViewModel>(news);
            return View(newsViewModel);
        }

        // GET: Admin/News/Create
        public ActionResult Create()
        {
            ViewBag.NewGroupId = new SelectList(_newsGroupService.GetAll(), "NewGroupId", "NewGroupTitle");
            return View();
        }

        // POST: Admin/News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NewsId,NewsTitle,Description,NewsGroupId")] NewsViewModel newsViewModel, HttpPostedFileBase imgUpload)
        {

            if (ModelState.IsValid)
            {
                #region save Image to server
                string imageName = "nophoto.png";
                if (imgUpload != null && imgUpload.ContentLength > 0)
                {

                    imageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imgUpload.FileName);
                    imgUpload.SaveAs(Server.MapPath("/images/news/" + imageName));
                }
                #endregion
                newsViewModel.ImageName = imageName;

                var news = AutoMapperConfig.mapper.Map<NewsViewModel, News>(newsViewModel);
                news.IsActive = true;
                news.Like = 0;
                news.See = 0;
                news.RegisterDate = DateTime.Now;
                news.UserId = _userService.GetUserId(User.Identity.Name);
                _newsService.Add(news);
                _newsService.Save();
                return RedirectToAction("Index");
            }

            ViewBag.NewGroupId = new SelectList(_newsGroupService.GetAll(), "NewGroupId", "NewGroupTitle");
            ViewBag.UserId = new SelectList(_userService.GetAll(), "UserId", "MobileNumber");
            return View(newsViewModel);
        }

        // GET: Admin/News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = _newsService.GetEntity(id.Value);
            if (news == null)
            {
                return HttpNotFound();
            }
            NewsViewModel newsViewModel = AutoMapperConfig.mapper.Map<News, NewsViewModel>(news);
            ViewBag.NewGroupId = new SelectList(_newsGroupService.GetAll(), "NewGroupId", "NewGroupTitle", news.NewsGroupId);
            return View(newsViewModel);
        }

        // POST: Admin/News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewsId,NewsTitle,Description,ImageName,RegisterDate,IsActive,See,Like,NewsGroupId,UserId")] NewsViewModel newsViewModel,
            HttpPostedFileBase imgUpload)
        {
            if (ModelState.IsValid)
            {
                if (imgUpload != null)
                {
                    if (newsViewModel.ImageName != "nophoto.png")
                    {
                        System.IO.File.Delete(Server.MapPath("/images/news/") + newsViewModel.ImageName);
                    }
                    else
                    {
                        newsViewModel.ImageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imgUpload.FileName);
                    }
                    imgUpload.SaveAs(Server.MapPath("/images/news/") + newsViewModel.ImageName);
                }
                News news = AutoMapperConfig.mapper.Map<NewsViewModel, News>(newsViewModel);
                _newsService.Update(news);
                _newsService.Save();
                return RedirectToAction("Index");
            }
            ViewBag.NewGroupId = new SelectList(_newsGroupService.GetAll(), "NewGroupId", "NewGroupTitle", newsViewModel.NewsGroupId);
            return View(newsViewModel);
        }

        // GET: Admin/News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = _newsService.GetEntity(id.Value);
            if (news == null)
            {
                return HttpNotFound();
            }
            NewsViewModel newsViewModel = AutoMapperConfig.mapper.Map<News, NewsViewModel>(news);
            return View(newsViewModel);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var news = _newsService.GetEntity(id);
            _newsService.Delete(id);
            _newsService.Save();
            if (news.ImageName != "nophoto.png")
            {
                string filePath = Server.MapPath("/images/news-group/") + news.ImageName;
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _newsService.Dispose();
            _newsGroupService.Dispose();
            _userService.Dispose();
        }
    }
}
