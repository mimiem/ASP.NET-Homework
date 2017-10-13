namespace MyTinyBlog.Services.Data
{
    using MyTinyBlog.Data.Contracts;
    using MyTinyBlog.Data.Models;
    using MyTinyBlog.Web.MyTinyBlog.Web.ViewModels.Blog;
    using System.Collections.Generic;
    using System.Linq;
    using System;

    public class BlogService
    {
        public ListViewModel GetList(int pageNo, IRepository<BlogPost> posts, IRepository<Category> categories, IRepository<Tag> tags)
        {
            // pick latest 10 posts
            List<BlogPost> postsNeeded = posts.All()
                               .Where(p => p.IsDeleted == false)
                               .OrderByDescending(p => p.CreatedOn)
                               .Skip((pageNo - 1) * 10)
                               .Take(10)
                               .ToList();

            var postsVM = postsNeeded.Select(p => new BlogPostViewModel
            {
                Title = p.Title,
                Content = p.Content,
                ShortContent = p.ShortContent,
                CreatedOn = p.CreatedOn,
                UrlSlug = p.UrlSlug,
                Category = this.GetCategory(p.CategoryId, categories),
                Tags = this.GetTags(p.Tags, tags)
            });

            int totalPosts = posts.All().Count();

            var listViewModel = new ListViewModel
            {
                Posts = postsVM,
                TotalPosts = totalPosts
            };

            return listViewModel;
        }

        public ListViewModel GetListPostsByCategory(string categorySlug, int pageNo, IRepository<BlogPost> posts, 
            IRepository<Category> categories, IRepository<Tag> tags)
        {

            List<BlogPost> postsNeeded = posts.All()
                               .Where(p => p.IsDeleted == false && p.Category.UrlSlug == categorySlug)
                               .OrderByDescending(p => p.CreatedOn)
                               .Skip((pageNo - 1) * 10)
                               .Take(10)
                               .ToList();

            var postsVM = postsNeeded.Select(p => new BlogPostViewModel
            {
                Title = p.Title,
                Content = p.Content,
                ShortContent = p.ShortContent,
                CreatedOn = p.CreatedOn,
                UrlSlug = p.UrlSlug,
                Category = this.GetCategory(p.CategoryId, categories),
                Tags = this.GetTags(p.Tags, tags)
            });

            var listViewModel = new ListViewModel
            {
                Posts = postsVM,
                TotalPosts = postsVM.Count()
            };

            return listViewModel;
        }

        private IEnumerable<TagViewModel> GetTags(ICollection<Tag> targetTags, IRepository<Tag> baseTags)
        {
            List<Tag> tags = baseTags.All().ToList();
            List<TagViewModel> resultTags = new List<TagViewModel>();

            foreach (var item in tags)
            {
                if (targetTags.Contains(item))
                {
                    resultTags.Add(new TagViewModel { Name = item.Name, UrlSlug = item.UrlSlug });
                }
            }

            return resultTags;
        }

        private CategoryViewModel GetCategory(int categoryId, IRepository<Category> categories)
        {
            Category category = categories.GetById(categoryId);
            CategoryViewModel categoryVM = new CategoryViewModel { Name = category.Name, UrlSlug = category.UrlSlug };

            return categoryVM;
        }
    }
}
