namespace MyTinyBlog.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using MyTinyBlog.Data.Contracts;
    using System.Data.Entity;
    using System.Linq;

    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDbSet<T> set;

        public Repository(IDbSet<T> set)
        {
            this.set = set;
        }

        public void Add(T entity)
        {
            this.set.Add(entity);
        }

        public IEnumerable<T> All()
        {
            return this.set;
        }

        public bool Any()
        {
            return this.set.Any();
        }

        public bool Any(Expression<Func<T, bool>> expression)
        {
            return this.set.Any(expression);
        }

        public int Count()
        {
            return this.set.Count();
        }

        public int Count(Expression<Func<T, bool>> expression)
        {
            return this.set.Count(expression);
        }

        public T Find(int id)
        {
            return this.set.Find(id);
        }

        public T First()
        {
            return this.set.First();
        }

        public T First(Expression<Func<T, bool>> expression)
        {
            return this.set.First(expression);
        }

        public T FirstOrDeafault()
        {
            return this.set.FirstOrDefault();
        }

        public T FirstOrDeafault(Expression<Func<T, bool>> expression)
        {
            return this.set.FirstOrDefault(expression);
        }

        public void Remove(T entity)
        {
            this.set.Remove(entity);
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> expression)
        {
            return this.set.Where(expression);
        }
    }
}
