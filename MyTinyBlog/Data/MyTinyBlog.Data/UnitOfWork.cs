namespace MyTinyBlog.Data
{
    using MyTinyBlog.Data.Contracts;
    using MyTinyBlog.Data.Models;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        private IRepository<ApplicationUser> users;
        private IRepository<Category> categories;
        private IRepository<PostComment> comments;
        private IRepository<BlogPost> posts;
        private IRepository<Tag> tags;

        public UnitOfWork()
        {
            this.context = new ApplicationDbContext();
        }

        public IRepository<Category> Categories
       => this.categories ?? (this.categories = new Repository<Category>(this.context.Categories));

        public IRepository<PostComment> Comments
        => this.comments ?? (this.comments = new Repository<PostComment>(this.context.Comments));

        public IRepository<BlogPost> Posts
        => this.posts ?? (this.posts = new Repository<BlogPost>(this.context.Posts));

        public IRepository<Tag> Tags
        => this.tags ?? (this.tags = new Repository<Tag>(this.context.Tags));

        public IRepository<ApplicationUser> Users
        => this.users ?? (this.users = new Repository<ApplicationUser>(this.context.Users));

        public int Commit()
        {
            return this.context.SaveChanges();
        }
    }
}
