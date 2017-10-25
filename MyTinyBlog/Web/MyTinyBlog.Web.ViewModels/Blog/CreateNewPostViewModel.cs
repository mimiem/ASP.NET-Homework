namespace MyTinyBlog.Web.MyTinyBlog.Web.ViewModels.Blog
{
    using System.Web.Mvc;

    public class CreateNewPostViewModel
    {
        public string Title { get; set; }

        [AllowHtml]
        public string ShortContent { get; set; }

        [AllowHtml]
        public string Content { get; set; }

        public string Category { get; set; }

        public string Tags { get; set; }
    }
}
