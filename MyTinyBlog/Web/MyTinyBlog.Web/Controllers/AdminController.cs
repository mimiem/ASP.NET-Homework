namespace MyTinyBlog.Web.MyTinyBlog.Web.Controllers
{
    using Services.Data;
    using MyTinyBlog.Web.ViewModels.Blog;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Net;

    [Authorize]
    public class AdminController : Controller
    {
        private readonly AdminService service;

        public AdminController()
        {
            this.service = new AdminService();
        }

        [HttpGet]
        public ViewResult Manage()
        {
            ViewBag.Title = "My Posts";
            IEnumerable<BlogPostViewModel> posts = this.service.GetAllPosts();

            return View(posts);
        }

        [HttpGet]
        public ViewResult AddPost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPost(CreateNewPostViewModel post)
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }

            this.service.AddNewPost(post);

            var allPosts = this.service.GetAllPosts();

            return RedirectToAction("Posts", "Blog");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EditPostViewModel post = this.service.GetPostForEdit(id);
            
            return View(post);
        }

        [HttpPost]
        public ActionResult Edit(EditPostViewModel post)
        {
            if (ModelState.IsValid)
            {
                this.service.EditPost(post);
                return RedirectToAction("Posts", "Blog");
            }
            return View(post);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DeletePostViewModel post = this.service.GetPostForDelete(id);

            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                this.service.RemovePost(id);
            }
            catch (Exception)
            {
                return RedirectToAction("Delete");
            }

            return RedirectToAction("Manage");
        }
    }
}