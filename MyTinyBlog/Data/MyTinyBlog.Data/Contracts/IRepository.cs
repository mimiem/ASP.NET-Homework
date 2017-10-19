namespace MyTinyBlog.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IRepository<T>
    {
        IEnumerable<T> All();

        void Add(T entity);

        T First();

        T First(Expression<Func<T, bool>> expression);

        T FirstOrDeafault();

        T FirstOrDeafault(Expression<Func<T, bool>> expression);

        IEnumerable<T> Where(Expression<Func<T, bool>> expression);

        bool Any();

        bool Any(Expression<Func<T, bool>> expression);

        T Find(int id);

        int Count();

        int Count(Expression<Func<T, bool>> expression);

        void Remove(T entity);
    }
}
