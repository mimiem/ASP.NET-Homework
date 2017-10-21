namespace MyTinyBlog.Web.Controllers
{
    using Microsoft.AspNet.Identity;
    using MyTinyBlog.Web.ViewModels.Blog;
    using Services.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class BlogController : Controller
    {

        private readonly BlogService service;

        public BlogController()
        {
            this.service = new BlogService();
        }

        /// <summary>
        /// Default value of p is 1. Represents page number
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [HttpGet]
        public ViewResult Posts(int p = 1)
        {
            IEnumerable<BlogPostViewModel> latestPosts = this.service.GetLatestPosts(p);

            ViewBag.Title = "Latest Posts";

            return View("List", latestPosts);
        }

        [HttpGet]
        public ViewResult Category(string category, int p = 1)
        {
            IEnumerable<BlogPostViewModel> postsByCategory = this.service.GetPostsByCategory(category, p);
            //if (listViewModel.Posts.Count() <= 0)
            //    throw new HttpException(404, "Category not found");

            ViewBag.Title = String.Format(@"Latest posts on category ""{0}""",
                                postsByCategory.First().Category.Name);

            return View("List", postsByCategory);
        }

        [HttpGet]
        public ViewResult Tag(string tag, int p = 1)
        {
            IEnumerable<BlogPostViewModel> postsByTag = this.service.GetListPostsByTag(tag, p);

            //if (viewModel.Tag == null)
            //    throw new HttpException(404, "Tag not found");

            TagViewModel tagVM = this.service.GetTag(tag);

            ViewBag.Title = String.Format(@"Latest posts tagged on ""{0}""",
                tagVM.Name);
            return View("List", postsByTag);
        }

        public ViewResult Search(string text, int p = 1)
        {
            ViewBag.Title = String.Format(@"Lists of posts found
                        for search text ""{0}""", text);

            IEnumerable<BlogPostViewModel> seearchedPosts = this.service.PostForSearch(text, p);
            return View("List", seearchedPosts);
        }

        [HttpGet]
        public ViewResult Post(int year, int month, string title)
        {
            BlogPostViewModel post = this.service.GetPost(year, month, title);

            //if (post == null)
            //    throw new HttpException(404, "Post not found");

            //if (post.Published == false && User.Identity.IsAuthenticated == false)
            //    throw new HttpException(401, "The post is not published");

            return View(post);
        }

        [HttpGet]
        public ViewResult MyPosts()
        {
            IEnumerable<BlogPostViewModel> posts = this.service.GetAllPosts();

            ViewBag.Title = "My Posts";

            return View("MyPosts", posts);
        }

        //[OutputCache]
        [ChildActionOnly]
        public PartialViewResult Sidebars()
        {
            WidgetViewModel widgetViewModel = this.service.CreateWidgetViewModel();
            return PartialView("_Sidebars", widgetViewModel);
        }
    }
}