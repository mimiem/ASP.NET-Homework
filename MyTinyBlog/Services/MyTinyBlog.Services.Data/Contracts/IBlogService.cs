namespace MyTinyBlog.Services.Data.Contracts
{
    using MyTinyBlog.Data.Models;
    using MyTinyBlog.Web.MyTinyBlog.Web.ViewModels.Blog;
    using System.Collections.Generic;

    public interface IBlogService
    {
        IEnumerable<BlogPostViewModel> GetLatestPosts(int pageNo);

        IEnumerable<BlogPostViewModel> GetPostsByCategory(string categorySlug, int pageNo);

        IEnumerable<BlogPostViewModel> GetListPostsByTag(string tagSlug, int pageNo);

        BlogPostViewModel GetPost(int year, int month, string titleSlug);
    }
}
