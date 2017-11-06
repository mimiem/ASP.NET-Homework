namespace MyTinyBlog.Services.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using MyTinyBlog.Data.Models;
    using MyTinyBlog.Web.MyTinyBlog.Web.ViewModels.Blog;
    using System;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class AdminService : Service
    {
        //Roles
        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return this.Context.Roles.ToList();
        }

        public void CreateRole(string v)
        {
            this.Context.Roles.Add(new IdentityRole()
            {
                Name = v
            });

            this.Context.SaveChanges();
        }

        public IdentityRole GetRole(string roleName)
        {
            return this.Context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
        }

        public void DeleteRole(string roleName)
        {
            var thisRole = this.Context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            this.Context.Roles.Remove(thisRole);
            this.Context.SaveChanges();
        }

        public void EditRole(IdentityRole role)
        {
            this.Context.Entry(role).State = EntityState.Modified;
            this.Context.SaveChanges();
        }

        public IEnumerable<SelectListItem> GetRolesForDropDown()
        {
            return this.Context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
        }

        public ApplicationUser AddRoleToUser(string userName)
        {
            ApplicationUser user = this.Context.Users.Where(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            return user;
        }

        //Posts
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
                UrlSlug = post.Title.ToLower().Replace(' ', '_'),
                Category = this.Context.Categories.FirstOrDefault(c => c.Name == post.Category),
                Tags = this.GetOrAssignNewTags(post.Tags)
            };

            this.Context.Posts.Add(postToAdd);
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

        public void EditPost(EditPostViewModel post)
        {

            BlogPost postToEdit = this.Context.Posts.Find(post.Id);
            postToEdit.Id = post.Id;
            postToEdit.Title = post.Title;
            postToEdit.ShortContent = post.ShortContent;
            postToEdit.Content = post.Content;
            postToEdit.Category = this.Context.Categories.FirstOrDefault(c => c.Name == post.Category);
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

        public void RemovePost(int id)
        {
            var post = this.Context.Posts.Find(id);
            this.Context.Posts.Remove(post);
            this.Context.SaveChanges();
        }

        //Categories
        public IEnumerable<SelectListItem> GetCatgoriesForDropdown()
        {
            return this.Context.Categories.OrderBy(c => c.Name).Select(c => new SelectListItem { Value = c.Name.ToString(), Text = c.Name }).ToList();
        }

        public IEnumerable<CategoryViewModel> GetAllCategories()
        {
            IEnumerable<CategoryViewModel> categories = this.Context
                                                            .Categories
                                                            .Select(c => new CategoryViewModel
                                                                {
                                                                    Id = c.Id,
                                                                    Name = c.Name,
                                                                    UrlSlug = c.UrlSlug,
                                                                    Description = c.Description
                                                                })
                                                            .ToList();

            return categories;
        }

        public void AddNewCategory(CategoryViewModel category)
        {
            Category categoryToAdd = new Category
            {
                Name = category.Name,
                UrlSlug = category.UrlSlug,
                Description = category.Description
            };

            this.Context.Categories.Add(categoryToAdd);
            this.Context.SaveChanges();
        }

        public void EditCategory(CategoryViewModel category)
        {
            Category categoryToEdit = this.Context.Categories.Find(category.Id);

            categoryToEdit.Name = category.Name;
            categoryToEdit.UrlSlug = category.UrlSlug;
            categoryToEdit.Description = category.Description;

            this.Context.SaveChanges();
        }

        public void RemoveCategory(int id)
        {
            Category category = this.Context.Categories.Find(id);
            this.Context.Categories.Remove(category);
            this.Context.SaveChanges();
        }

        //Tags
        public void AddNewTag(TagViewModel tag)
        {
            this.Context.Tags.Add(new Tag { Name = tag.Name, UrlSlug = tag.UrlSlug, Description = tag.Description });
            this.Context.SaveChanges();
        }

        public void EditTag(TagViewModel tag)
        {
            Tag tagToEdit = this.Context.Tags.Find(tag.Id);

            tagToEdit.Name = tag.Name;
            tagToEdit.UrlSlug = tag.UrlSlug;
            tagToEdit.Description = tag.Description;

            this.Context.SaveChanges();
        }

        public TagViewModel GetTag(int? id)
        {
            Tag tag = this.Context.Tags.Find(id);

            return new TagViewModel { Name = tag.Name, UrlSlug = tag.UrlSlug, Description = tag.Description };
        }

        public void RemoveTag(int id)
        {
            Tag tag = this.Context.Tags.Find(id);
            this.Context.Tags.Remove(tag);
            this.Context.SaveChanges();
        }

        //
        public bool PostWithSuchTag(int id)
        {
            return this.Context.Tags.Any(t => t.Id == id);
        }

        public bool PostWithSuchCategory(int id)
        {
            return this.Context.Posts.Any(p => p.CategoryId == id);
        }

        private bool CheckIfTagExist(string tag)
        {
            return this.Context.Tags.Any(t => t.Name == tag);
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
        }//

        private Tag GetTag(string tag)
        {
            return this.Context.Tags.FirstOrDefault(t => t.Name == tag);
        }//

       
    }
}
