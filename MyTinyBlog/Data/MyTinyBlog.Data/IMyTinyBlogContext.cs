using MyTinyBlog.Data.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


namespace MyTinyBlog.Data
{
    public interface IMyTinyBlogContext : IDisposable
    {
        IDbSet<BlogPost> BlogPosts { get; set; }

        IDbSet<Page> Pages { get; set; }

        IDbSet<Tag> Tags { get; set; }

        IDbSet<Setting> Settings { get; set; }

        IDbSet<PostComment> PostComments { get; set; }

        IDbSet<ApplicationUser> Users { get; set; }

        IDbSet<Video> Videos { get; set; }

        int SaveChanges();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
