using System.Web.Mvc;

namespace MyTinyBlog.Web.MyTinyBlog.Web.ViewModels.Blog
{
    public class EditPostViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [AllowHtml]
        public string Content { get; set; }

        [AllowHtml]
        public string ShortContent { get; set; }

        public string Category { get; set; }

        public string Tags { get; set; }
    }
}
