namespace MyTinyBlog.Services.Data
{
    using MyTinyBlog.Data;

    public abstract class Service
    {
        protected Service()
        {
            this.Context = new ApplicationDbContext();
        }

        protected ApplicationDbContext Context { get; }
    }
}
