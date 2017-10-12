namespace MyTinyBlog.Web.MyTinyBlog.Web.ViewModels.Blog
{
    using System.Collections.Generic;

    public class ListViewModel
    {
        public IEnumerable<BlogPostViewModel> Posts { get; set; }
        public int TotalPosts { get; set; }
    }
}