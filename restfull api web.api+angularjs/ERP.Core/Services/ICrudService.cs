namespace ERP.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using ERP.Core.Models;

    public interface ICrudService<T> where T : BaseEntity, new()
    {
        int Update(T item);
        int FastUpdate(T item);
        void Save();
        void Delete(int id, bool hard = false);
        void DeleteMany(IEnumerable<int> ids, bool hard = false);
        T Get(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Where(Expression<Func<T, bool>> func, bool showDeleted = false);
        void Restore(int id);
        void Reload(T o);
        void Attach(BaseEntity t);
        void SetDetectChanges(bool value);
        void DetectChanges();
    }
}