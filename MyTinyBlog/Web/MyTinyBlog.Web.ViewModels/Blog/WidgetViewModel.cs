namespace MyTinyBlog.Web.MyTinyBlog.Web.ViewModels.Blog
{
    using System.Collections.Generic;

    public class WidgetViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public IEnumerable<TagViewModel> Tags { get; set; }

        public IEnumerable<BlogPostViewModel> LatestPosts { get; set; }
    }
}
