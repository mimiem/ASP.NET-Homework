namespace MyTinyBlog.Services.Data
{
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
                Tags = this.GetTags(p.Tags)
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
                Tags = this.GetTags(p.Tags)
            });

            var listViewModel = new ListViewModel
            {
                Posts = postsVM,
                TotalPosts = postsVM.Count()
            };

            return listViewModel;
        }

        public BlogPostViewModel GetPost(int year, int month, string titleSlug)
        {
            BlogPost searchedPost = this.Context.Posts
                        .Where(p => p.CreatedOn.Year == year && p.CreatedOn.Month == month && p.UrlSlug.Equals(titleSlug))
                        .First();

            BlogPostViewModel postVM = new BlogPostViewModel
            {
                Title = searchedPost.Title,
                UrlSlug = searchedPost.UrlSlug,
                Content = searchedPost.Content,
                ShortContent = searchedPost.ShortContent,
                CreatedOn = searchedPost.CreatedOn,
                Category = this.GetCategory(searchedPost.CategoryId),
                Tags = this.GetTags(searchedPost.Tags)
            };

            return postVM;
        }

        public WidgetViewModel CreateWidgetViewModel()
        {
            WidgetViewModel widget = new WidgetViewModel
            {
                Categories = this.GetAllCategories(),
                Tags = this.GetAllTags(),
                LatestPosts = this.GetAllPosts()
            };

            return widget;
        }

        private IEnumerable<BlogPostViewModel> GetAllPosts()
        {
            // pick latest 10 posts
            IEnumerable<BlogPost> postsNeeded = this.Context
                                             .Posts
                                             .Where(p => p.Published)
                                             .OrderByDescending(p => p.CreatedOn)
                                             .Take(5)
                                             .ToList();

            var postsVM = postsNeeded.Select(p => new BlogPostViewModel
            {
                Title = p.Title,
                Content = p.Content,
                ShortContent = p.ShortContent,
                CreatedOn = p.CreatedOn,
                UrlSlug = p.UrlSlug,
                Category = this.GetCategory(p.CategoryId),
                Tags = this.GetTags(p.Tags)
            });

            return postsVM;
        }

        private IEnumerable<TagViewModel> GetAllTags()
        {
            return this.Context.Tags.OrderBy(t => t.Name).ToList().Select(c => new TagViewModel
            {
                Name = c.Name,
                UrlSlug = c.UrlSlug
            });
        }

        private IEnumerable<CategoryViewModel> GetAllCategories()
        {
            return this.Context.Categories.OrderBy(p => p.Name).ToList().Select(c => new CategoryViewModel
            {
                Name = c.Name,
                UrlSlug = c.UrlSlug
            });
        }

        public ListViewModel PostForSearch(string text, int pageNo)
        {
            IEnumerable<BlogPost> postsNeeded = this.Context.Posts
                                             .Where(p => p.Published && (p.Title.Contains(text) 
                                             || p.Category.Name.Equals(text) 
                                             || p.Tags.Any(t => t.Name.Equals(text))))
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
                Tags = this.GetTags(p.Tags)
            });

            var listViewModel = new ListViewModel
            {
                Posts = postsVM,
                TotalPosts = postsVM.Count()
            };

            return listViewModel;
        }

        public TagViewModel GetTag(string tagSlug)
        {
            Tag tag = this.Context.Tags.FirstOrDefault(t => t.UrlSlug == tagSlug);
            TagViewModel tagVM = new TagViewModel
            {
                Name = tag.Name, UrlSlug = tag.UrlSlug
            };

            return tagVM;
        }

        public ListViewModel GetListPostsByTag(string tagSlug, int pageNo)
        {
            IEnumerable<BlogPost> postsNeeded = this.Context.Posts
                                             .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
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
                Tags = this.GetTags(p.Tags)
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
                                                 .ToList()
                                                 .Where(t => this.ContainsTag(targetTags, t))
                                                 .Select(t => new TagViewModel
                                                  {
                                                      Name = t.Name,
                                                      UrlSlug = t.UrlSlug
                                                  });

            return tags;
        }

        private bool ContainsTag(ICollection<Tag> targetTags, Tag t)
        {
            foreach (var tag in targetTags)
            {
                if(tag.Name == t.Name)
                {
                    return true;
                }
            }

            return false;
        }

        private CategoryViewModel GetCategory(int categoryId)
        {
            Category category = this.Context.Categories.Find(categoryId);
            CategoryViewModel categoryVM = new CategoryViewModel { Name = category.Name, UrlSlug = category.UrlSlug };

            return categoryVM;
        }
    }
}
