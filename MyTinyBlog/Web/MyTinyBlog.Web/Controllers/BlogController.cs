namespace MyTinyBlog.Web.Controllers
{
    using Data.Contracts;
    using Data.Models;
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
        public ViewResult Posts(int p = 1)
        {
            ListViewModel listViewModel = this.service.GetList(p);

            ViewBag.Title = "Latest Posts";

            return View("List", listViewModel);
        }

        public ViewResult Category(string category, int p = 1)
        {
            ListViewModel listViewModel = this.service.GetListPostsByCategory(category, p);

            //if (listViewModel.Posts.Count() <= 0)
            //    throw new HttpException(404, "Category not found");

            ViewBag.Title = String.Format(@"Latest posts on category ""{0}""",
                                listViewModel.Posts.First().Category.Name);

            return View("List", listViewModel);
        }

    }
}