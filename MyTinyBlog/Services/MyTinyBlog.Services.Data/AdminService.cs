namespace MyTinyBlog.Services.Data
{
    using MyTinyBlog.Data.Models;
    using MyTinyBlog.Web.MyTinyBlog.Web.ViewModels.Blog;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AdminService : Service
    {
        public IEnumerable<BlogPostViewModel> GetAllPosts()
        {
            IEnumerable<BlogPost> postsNeeded = this.Context.Posts
                                             .Where(p => p.Published)
                                             .OrderByDescending(p => p.CreatedOn)
                                             .ToList();

            var postsVM = postsNeeded.Select(p => new BlogPostViewModel
            {
                Id = p.Id,
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

        public void AddNewPost(CreateNewPostViewModel post)
        {
            BlogPost postToAdd = new BlogPost
            {
                Title = post.Title,
                CreatedOn = DateTime.Now,
                Content = post.Content,
                ShortContent = post.ShortContent,
                Published = true,
                UrlSlug = post.Title.ToLower().Replace(' ','_'), 
                Category = this.GetOrAssignNewCategory(post.Category),
                Tags = this.GetOrAssignNewTags(post.Tags)
            };

            this.Context.Posts.Add(postToAdd);
            this.Context.SaveChanges();
        }

        public void EditPost(EditPostViewModel post)
        {

            BlogPost postToEdit = this.Context.Posts.Find(post.Id);
            postToEdit.Id = post.Id;
            postToEdit.Title = post.Title;
            postToEdit.ShortContent = post.ShortContent;
            postToEdit.Content = post.Content;
            postToEdit.Category = this.GetOrAssignNewCategory(post.Category);
            postToEdit.Modified = DateTime.Now;

            var tags = post.Tags.Split(' ');

            foreach (var tag in tags)
            {
                if (!this.CheckIfTagExist(tag))
                {
                    Tag tagToAdd = this.CreateNewTag(tag);
                    postToEdit.Tags.Add(tagToAdd);
                }
            }

            this.Context.SaveChanges();
        }

        public EditPostViewModel GetPostForEdit(int? id)
        {
            BlogPost post = this.Context.Posts.Find(id);

            return new EditPostViewModel
            {
                Id = post.Id,
                Content = post.Content,
                ShortContent = post.ShortContent,
                Title = post.Title,
                Category = post.Category.Name,
                Tags = String.Join(" ", post.Tags.Select(t => t.Name))
            };
        }

        public DeletePostViewModel GetPostForDelete(int? id)
        {
            BlogPost post = this.Context.Posts.Find(id);

            return new DeletePostViewModel { Title = post.Title, CreatedOn = post.CreatedOn };
        }

        private Category GetOrAssignNewCategory(string category)
        {
            if (this.Context.Categories.Any(c => c.Name == category))
            {
                return this.Context.Categories.FirstOrDefault(c => c.Name == category);
            }

            Category newCategory = new Category
            {
                Name = category,
                UrlSlug = "TODO",
                Description = "TODO"
            };

            this.Context.Categories.Add(newCategory);
            this.Context.SaveChanges();

            return newCategory;
        }

        public void RemovePost(int id)
        {
            var post = this.Context.Posts.Find(id);
            this.Context.Posts.Remove(post);
            this.Context.SaveChanges();
        }

        private ICollection<Tag> GetOrAssignNewTags(string tags)
        {
            ICollection<Tag> neededTags = new HashSet<Tag>();
            var tagSplitted = tags.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); 

            foreach (var tag in tagSplitted)
            {
                Tag currentTag;

                if (this.CheckIfTagExist(tag))
                {
                    currentTag = this.GetTag(tag);
                }
                else
                {
                    currentTag = this.CreateNewTag(tag);
                }

                neededTags.Add(currentTag);
            }

            return neededTags;
        }

        private Tag CreateNewTag(string tag)
        {
            return new Tag { Name = tag, CreatedOn = DateTime.Now, Description = "TODO", UrlSlug = tag.ToLower() }; // TODO for now here tags will be created
        }

        private Tag GetTag(string tag)
        {
            return this.Context.Tags.FirstOrDefault(t => t.Name == tag);
        }

        private bool CheckIfTagExist(string tag)
        {
            return this.Context.Tags.Any(t => t.Name == tag);
        }
    }
}
