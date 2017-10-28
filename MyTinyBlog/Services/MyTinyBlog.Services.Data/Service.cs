namespace MyTinyBlog.Services.Data
{
    using MyTinyBlog.Data;
    using MyTinyBlog.Data.Models;
    using System.Collections.Generic;
    using System.Linq;
    using Web.MyTinyBlog.Web.ViewModels.Blog;

    public abstract class Service
    {
        protected Service()
        {
            this.Context = new ApplicationDbContext();
        }

        protected ApplicationDbContext Context { get; }

        public CategoryViewModel GetCategory(int? categoryId)
        {
            Category category = this.Context.Categories.Find(categoryId);
            CategoryViewModel categoryVM = new CategoryViewModel { Name = category.Name, UrlSlug = category.UrlSlug };

            return categoryVM;
        } //

        protected IEnumerable<TagViewModel> GetTags(ICollection<Tag> targetTags)
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
                if (tag.Name == t.Name)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
