using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using System.Data.Entity;

namespace WebUI.Controllers
{
    public class SupportController : BaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.MenuItem = "support";
            List<Comment> coment = db.Comments.Where(c => c.CustId == Cust.CustId).ToList();
            return View(coment);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Comment cmt)
        {
            Comment comment = new Comment();
            comment.Note = cmt.Note;
            comment.CustId = Cust.CustId;
            db.Comments.Add(comment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Answer(int id)
        {
            Comment comment = db.Comments.Find(id);
            return View(comment);
        }
        [HttpPost]
        public ActionResult Answer(Comment cmt)
        {
            Comment comment = db.Comments.Find(cmt.CommentId);
            comment.Response = cmt.Response;
            db.Entry(comment).State = EntityState.Modified;
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