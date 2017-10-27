namespace MyTinyBlog.Web.MyTinyBlog.Web.ViewModels.Blog
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class EditPostViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "{0} must be at least 3 symbols long")]
        [MaxLength(30, ErrorMessage = "{0} must be max 30 symbols long")]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        public string Content { get; set; }

        [Required]
        [AllowHtml]
        public string ShortContent { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Tags { get; set; }
    }
}
