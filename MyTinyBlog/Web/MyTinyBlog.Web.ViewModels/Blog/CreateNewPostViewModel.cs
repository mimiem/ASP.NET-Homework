namespace MyTinyBlog.Web.MyTinyBlog.Web.ViewModels.Blog
{
    using MyTinyBlog.Web.ViewModels.Blog;
    using System.Collections.Generic;

    public class CreateNewPostViewModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public CategoryViewModel Category { get; set; }

        public IEnumerable<TagViewModel> Tags { get; set; }
    }
}
