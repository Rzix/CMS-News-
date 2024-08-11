using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMSNews.Models.Context;
using CMSNews.Models.Models;
using CMSNews.Models.ViewModels;
using CMSNews.Service.Service;
using CMSNews.App_Start;
using Auto_Mapper.App_Start;

namespace CMSNews.Areas.Admin.Controllers
{
    public class CommentsController : Controller
    {
        private DbCMSNewsContext db = new DbCMSNewsContext();
        CommentSevice _commentSevice;

        public CommentsController()
        {
            _commentSevice = new CommentSevice(db);
        }
        public ActionResult Index()
        {
            var comments = _commentSevice.GetAll();
            var commentViewModels = AutoMapperConfig.mapper.Map<IEnumerable<Comment>, List<CommentViewModel>>(comments);
            return View(commentViewModels);
        }

        // GET: Admin/Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Coments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }



        // GET: Admin/Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = _commentSevice.GetEntity(id.Value);
            if (comment == null)
            {
                return HttpNotFound();
            }
            var commentViewModel = AutoMapperConfig.mapper.Map<Comment, CommentViewModel>(comment);
            return View(commentViewModel);
        }

        // POST: Admin/Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentId,CommentText,Name,Email,Registerdate,IsActive,NewsId")] CommentViewModel commentViewModel )
        {
            if (ModelState.IsValid)
            {
                var comment = AutoMapperConfig.mapper.Map<CommentViewModel, Comment>(commentViewModel);
                _commentSevice.Update(comment);
                _commentSevice.Save();
                return RedirectToAction("Index");
            }
            return View(commentViewModel);
        }

        // GET: Admin/Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Coments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Admin/Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Coments.Find(id);
            db.Coments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
