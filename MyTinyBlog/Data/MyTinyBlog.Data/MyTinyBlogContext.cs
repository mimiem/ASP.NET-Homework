namespace MyTinyBlog.Data
{
    using Migrations;
    using MyTintBlog.Data.Models;
    using System.Data.Entity;

    public class MyTinyBlogContext : DbContext
    {
        // Your context has been configured to use a 'MyTinyBlogContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'MyTinyBlog.Data.MyTinyBlogContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'MyTinyBlogContext' 
        // connection string in the application configuration file.
        public MyTinyBlogContext()
            : base("name=MyTinyBlogContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MyTinyBlogContext, Configuration>());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public static MyTinyBlogContext Create()
        {

            return new MyTinyBlogContext();

        }

        public IDbSet<BlogPost> BlogPosts { get; set; }

        public IDbSet<Page> Pages { get; set; }

        public IDbSet<Tag> Tags { get; set; }

        public IDbSet<PostComment> PostComments { get; set; }

        public IDbSet<Setting> Settings { get; set; }

        public IDbSet<Video> Videos { get; set; }

    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}