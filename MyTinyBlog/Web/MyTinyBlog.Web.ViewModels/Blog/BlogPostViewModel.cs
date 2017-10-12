namespace MyTinyBlog.Web.MyTinyBlog.Web.ViewModels.Blog
{
    using System;
    using System.Collections.Generic;

    public class BlogPostViewModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string ShortContent { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UrlSlug { get; set; }

        public string CategoryName { get; set; }

        public string CategoryUrlSlug { get; set; }

        public IEnumerable<TagViewModel> Tags { get; set; }
    }
}