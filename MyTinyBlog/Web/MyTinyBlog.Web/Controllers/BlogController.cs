﻿namespace MyTinyBlog.Web.Controllers
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
        private readonly IRepository<BlogPost> posts;
        private readonly IRepository<Category> categories;
        private readonly IRepository<Tag> tags;


        private readonly BlogService service;

        public BlogController(IRepository<BlogPost> posts, IRepository<Category> categories, IRepository<Tag> tags)
        {
            this.posts = posts;
            this.categories = categories;
            this.tags = tags;
            this.service = new BlogService();
        }

        /// <summary>
        /// Default value of p is 1. Represents page number
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public ViewResult Posts(int p = 1)
        {
            ListViewModel listViewModel = this.service.GetList(p, this.posts, this.categories, this.tags);

            ViewBag.Title = "Latest Posts";

            return View("List", listViewModel);
        }

        public ViewResult Category(string category, int p = 1)
        {
            ListViewModel listViewModel = this.service.GetListPostsByCategory(category, p, this.posts, this.categories, this.tags);

            //if (listViewModel.Posts.Count() <= 0)
            //    throw new HttpException(404, "Category not found");

            ViewBag.Title = String.Format(@"Latest posts on category ""{0}""",
                                listViewModel.Posts.First().Category.Name);

            return View("List", listViewModel);
        }

    }
}