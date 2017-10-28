namespace MyTinyBlog.Web.MyTinyBlog.Web.ViewModels.Blog
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class CreateNewPostViewModel
    {
        [Required(ErrorMessage = "{0} is required")]
        [MinLength(3, ErrorMessage = "{0} must be at least 3 symbols long")]
        [MaxLength(30, ErrorMessage = "{0} must be max 30 symbols long")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Short description is required")]
        [Display(Name = "Short description")]
        [AllowHtml]
        public string ShortContent { get; set; }


        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Content")]
        [AllowHtml]
        public string Content { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public string Category { get; set; }

        [Required(ErrorMessage = "At least one tag is required")]
        [Display(Name = "Tags")]
        public string Tags { get; set; }
    }
}
