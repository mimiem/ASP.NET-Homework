namespace MyTinyBlog.Web.MyTinyBlog.Web.Controllers
{
    using Services.Data;
    using MyTinyBlog.Web.ViewModels.Blog;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

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
        public ViewResult AddPost(BlogPostViewModel post)
        {
            if (!ModelState.IsValid)
            {
                //return View(post);
            }
            return View();
        }

        public ViewResult Edit()
        {
            //to do
            return View();
        }

        public ViewResult Delete()
        {
            //to do
            return View();
        }
    }
}