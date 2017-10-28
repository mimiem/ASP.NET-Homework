namespace MyTinyBlog.Web.MyTinyBlog.Web.ViewModels.Blog
{
    using System;
    using System.Collections.Generic;

    public class BlogPostViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ShortContent { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UrlSlug { get; set; }

        public CategoryViewModel Category { get; set; }

        public IEnumerable<TagViewModel> Tags { get; set; }
    }
}