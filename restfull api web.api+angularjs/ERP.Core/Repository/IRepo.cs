namespace ERP.Core.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using ERP.Core.Models;

    public interface IRepo<T>
    {
        T Get(int id);
        IQueryable<T> GetAll(bool showDeleted = false);
        IQueryable<T> Where(Expression<Func<T, bool>> predicate, bool showDeleted = false);
        T Insert(T o);
        void Save();
        void Delete(T o, bool hard );
        void Restore(T o);
        void Attach(BaseEntity b, bool add = false);
        void Reload(T o);
        void SetDetectChanges(bool value);

        void DetectChanges();
    }
}