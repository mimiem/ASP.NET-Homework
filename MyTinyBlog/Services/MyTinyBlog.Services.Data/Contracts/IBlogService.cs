namespace MyTinyBlog.Services.Data.Contracts
{
    using MyTinyBlog.Data.Models;
    using MyTinyBlog.Web.MyTinyBlog.Web.ViewModels.Blog;
    using System.Collections.Generic;

    public interface IBlogService
    {
        ListViewModel GetList(int pageNo);

        ListViewModel GetListPostsByCategory(string categorySlug, int pageNo);

        IEnumerable<TagViewModel> GetTags(ICollection<Tag> targetTags);

        CategoryViewModel GetCategory(int categoryId);
    }
}
