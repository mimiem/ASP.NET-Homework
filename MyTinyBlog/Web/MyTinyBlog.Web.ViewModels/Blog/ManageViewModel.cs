namespace MyTinyBlog.Web.MyTinyBlog.Web.ViewModels.Blog
{
    using System.Collections.Generic;

    public class ManageViewModel
    {
        public IEnumerable<BlogPostViewModel> Posts { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public IEnumerable<TagViewModel> Tags { get; set; }
    }
}
