namespace MyTinyBlog.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

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
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}