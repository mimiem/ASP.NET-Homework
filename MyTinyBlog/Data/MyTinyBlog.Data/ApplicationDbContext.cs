namespace MyTinyBlog.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Migrations;
    using Models;
    using System.Data.Entity;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public IDbSet<BlogPost> Posts { get; set; }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<PostComment> Comments { get; set; }

        public IDbSet<Tag> Tags { get; set; }

    }
}
