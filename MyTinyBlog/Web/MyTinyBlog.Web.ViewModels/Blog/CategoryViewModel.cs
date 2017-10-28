using System.ComponentModel.DataAnnotations;

namespace MyTinyBlog.Web.MyTinyBlog.Web.ViewModels.Blog
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(15, ErrorMessage = "{0} can be 15 symbols long")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string UrlSlug { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Description { get; set; }
    }
}
