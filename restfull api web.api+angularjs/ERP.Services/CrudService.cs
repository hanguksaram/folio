using System;
using System.Collections.Generic;
using Dynamitey;

namespace ERP.Services
{
    using System.Linq.Expressions;

    using ERP.Core.Models;
    using ERP.Core.Repository;
    using ERP.Core.Services;

    using Omu.ValueInjecter;

    public class CrudService<T> : ICrudService<T> where T : BaseEntity, new()
    {
        protected IRepo<T> Repo;

        public CrudService(IRepo<T> repo)
        {
            this.Repo = repo;
        }

        public void SetDetectChanges(bool value)
        {
            this.Repo.SetDetectChanges(value);
        }

        public void DetectChanges()
        {
            this.Repo.DetectChanges();
        }
        public IEnumerable<T> GetAll()
        {
            return this.Repo.GetAll();
        }

        public T Get(int id)
        {
            return this.Repo.Get(id);
        }
        public void Reload(T o)
        {
             this.Repo.Reload(o);
        }

        public virtual int Update(T item)
        {
            if (item.ID == 0)
            {
                var newItem = this.Repo.Insert(item);
                this.Repo.Save();
                return newItem.ID;
            }

            var itm = this.Get(item.ID);
            var date = itm.CreateDate;
            itm.InjectFrom(item);
            itm.CreateDate = date;
            this.Repo.Save();
            return item.ID;
        }

        public virtual int FastUpdate(T item)
        {
            if (item.ID == 0)
            {
                return 0;
            }
            var itm = this.Get(item.ID);
            var date = itm.CreateDate;
            itm.InjectFrom(item);
            itm.CreateDate = date;

            
            return item.ID;
        }
        
        public void Save()
        {
            
            this.Repo.Save();
        }

        public virtual void Delete(int id, bool hard = false)
        {
            this.Repo.Delete(this.Repo.Get(id), hard);
            this.Repo.Save();
        }
        public virtual void DeleteMany(IEnumerable<int> ids, bool hard = false)
        {
            foreach (var id in ids)
            {
                this.Repo.Delete(this.Repo.Get(id), hard);
            }
            
            this.Repo.Save();
        }

        public void Restore(int id)
        {
            this.Repo.Restore(this.Repo.Get(id));
            this.Repo.Save();
        }

        public void Attach(BaseEntity t)
        {
            this.Repo.Attach(t, true);
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> predicate, bool showDeleted = false)
        {
            return this.Repo.Where(predicate, showDeleted);
        }
    }
}
