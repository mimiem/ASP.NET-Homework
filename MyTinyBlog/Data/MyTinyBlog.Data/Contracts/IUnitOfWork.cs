namespace MyTinyBlog.Data.Contracts
{
    using Models;

    public interface IUnitOfWork
    {
        IRepository<ApplicationUser> Users { get; }

        IRepository<BlogPost> Posts { get; }

        IRepository<Category> Categories { get; }

        IRepository<PostComment> Comments { get; }

        IRepository<Tag> Tags { get; }

        int Commit();
    }
}
