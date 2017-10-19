namespace MyTinyBlog.Services.Data
{
    using MyTinyBlog.Data.Contracts;
    using MyTinyBlog.Data.Models;
    using MyTinyBlog.Web.MyTinyBlog.Web.ViewModels.Blog;
    using System.Collections.Generic;
    using System.Linq;
    using System;

    public class BlogService : Service
    {
        public ListViewModel GetList(int pageNo)
        {
            // pick latest 10 posts
            IEnumerable<BlogPost> postsNeeded = this.Context
                                             .Posts
                                             .Where(p => p.Published)
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
                Category = this.GetCategory(p.CategoryId),
                //Tags = this.GetTags(p.Tags)
            });

            int totalPosts = this.Context.Posts.Count();

            var listViewModel = new ListViewModel
            {
                Posts = postsVM,
                TotalPosts = totalPosts
            };

            return listViewModel;
        }

        public ListViewModel GetListPostsByCategory(string categorySlug, int pageNo)
        {

            IEnumerable<BlogPost> postsNeeded = this.Context.Posts
                                             .Where(p => p.Published && p.Category.UrlSlug == categorySlug)
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
                Category = this.GetCategory(p.CategoryId),
                //Tags = this.GetTags(p.Tags)
            });

            var listViewModel = new ListViewModel
            {
                Posts = postsVM,
                TotalPosts = postsVM.Count()
            };

            return listViewModel;
        }

        private IEnumerable<TagViewModel> GetTags(ICollection<Tag> targetTags)
        {
            IEnumerable<TagViewModel> tags = this.Context
                                                 .Tags
                                                 .Where(t => targetTags.Contains(t))
                                                 .ToList()
                                                 .Select(t => new TagViewModel
                                                  {
                                                      Name = t.Name,
                                                      UrlSlug = t.UrlSlug
                                                  });

            return tags;
        }

        private CategoryViewModel GetCategory(int categoryId)
        {
            Category category = this.Context.Categories.Find(categoryId);
            CategoryViewModel categoryVM = new CategoryViewModel { Name = category.Name, UrlSlug = category.UrlSlug };

            return categoryVM;
        }
    }
}
