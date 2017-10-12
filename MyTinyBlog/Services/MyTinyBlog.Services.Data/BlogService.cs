namespace MyTinyBlog.Services.Data
{
    using MyTinyBlog.Data.Contracts;
    using MyTinyBlog.Data.Models;
    using MyTinyBlog.Web.MyTinyBlog.Web.ViewModels.Blog;
    using System.Collections.Generic;
    using System.Linq;

    public class BlogService
    {
        public ListViewModel GetList(int p, IRepository<BlogPost> posts, IRepository<Category> categories, IRepository<Tag> tags)
        {
            // pick latest 10 posts
            var postsVM = this.Posts(p - 1, 10, posts, categories, tags);

            var totalPosts = posts.All().Count();

            var listViewModel = new ListViewModel
            {
                Posts = postsVM,
                TotalPosts = totalPosts
            };

            return listViewModel;
        }

        private IEnumerable<BlogPostViewModel> Posts(int pageNo, int pageSize, IRepository<BlogPost> posts, IRepository<Category> categories, IRepository<Tag> tags)
        {
            var postsVM = posts.All()
                               .Where(p => p.IsDeleted == false)
                               .OrderByDescending(p=>p.CreatedOn)
                               .Skip(pageNo * pageSize)
                               .Take(pageSize)
                               .ToList()
                               .Select(p => new BlogPostViewModel
                               {
                                   Title = p.Title,
                                   Content = p.Content,
                                   ShortContent = p.ShortContent,
                                   CreatedOn = p.CreatedOn,
                                   UrlSlug = p.UrlSlug,
                                   CategoryName = categories.GetById(p.CategoryId).Name,
                                   CategoryUrlSlug = categories.GetById(p.CategoryId).UrlSlug,
                                   Tags = tags.All().Where(t => (p.Tags.Select(tag=>tag.Name)).Contains(t.Name)).Select(t => new TagViewModel { Name = t.Name, UrlSlug = t.UrlSlug})
                               });

            return postsVM;

           
        }

    }
}
