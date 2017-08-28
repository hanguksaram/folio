using System;
using System.Dynamic;
using System.Linq;

namespace ERP.SqlServer
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;
    using System.Linq.Expressions;

    using ERP.Core;
    using ERP.Core.Logger;
    using ERP.Core.Models;
    using ERP.Core.Repository;

    using Omu.ValueInjecter;

    public class Repo<T> : IRepo<T>
        where T : BaseEntity, new()
    {
        protected readonly DbContext DbContext;
        public IAuthProvider authProvider { get; set; }

        public Repo(IDbContextFactory f, IAuthProvider auth)
        {
            this.DbContext = f.GetContext();
            authProvider = auth;
        }

        public void Save()
        {
            var entityes = this.DbContext.ChangeTracker.Entries().Where(x => (x.State == EntityState.Modified || x.State == EntityState.Added || x.State == EntityState.Deleted));
            foreach (DbEntityEntry entity in entityes)
            {
                if (entity.Entity is BaseEntity)
                {
                    var e = entity.Entity as BaseEntity;

                    if (e.ID > 0 && entity.State == EntityState.Added)
                    {
                        entity.State = EntityState.Unchanged;
                        continue;
                    }

                    e.ModifyBy = this.authProvider.CurrentUser;
                    e.ModifyDate = DateTime.UtcNow;

                    if (e.CreatedBy == null)
                    {
                        e.CreatedBy = e.ModifyBy;
                        e.CreateDate = e.ModifyDate;
                    }

                    if (e.CreateDate <= DateTime.MinValue)
                    {
                        e.CreateDate = DateTime.UtcNow;
                    }
                }                
            }

            try
            {
                this.DbContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult dbEntityValidationResult in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError dbValidationError in dbEntityValidationResult.ValidationErrors)
                    {
                        Console.WriteLine(dbValidationError.PropertyName + ", " + dbValidationError.ErrorMessage);
                        Logger.Current.Error(
                            string.Format(
                                "ValidationError: {0} : {1}",
                                dbValidationError.PropertyName,
                                dbValidationError.ErrorMessage));
                    }
                }
                throw new Exception("Database validation error!");
            }
            catch (Exception e)
            {
                //Logger.Current.Error("DB SAVE: ", e);
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException != null ? e.InnerException.Message : "");
                throw;
            }
        }


        public T Insert(T o)
        {
            var t = this.DbContext.Set<T>().Create();
            t.InjectFrom(o);
            t.CreateDate = DateTime.UtcNow;
            this.DbContext.Set<T>().Add(t);
            return t;
        }

        public virtual void Delete(T o, bool hard)
        {
            if (hard)
            {
                var a = this.Get(o.ID);
                this.DbContext.Set<T>().Remove(a);
                this.DbContext.SaveChanges();
                return;
            }
            var j = this.Get(o.ID);
            j.IsDeleted = true;
            j.ModifyDate = DateTime.UtcNow;        
            this.Save();
        }

        public T Get(int id)
        {
            return this.DbContext.Set<T>().Find(id);
        }

        public void SetDetectChanges(bool value)
        {
            DbContext.Configuration.AutoDetectChangesEnabled = value;
        }

        public void DetectChanges()
        {
            DbContext.ChangeTracker.DetectChanges();
        }

        public void Restore(T o)
        {
            var j = this.Get(o.ID);
            j.IsDeleted = false;
            j.ModifyDate = DateTime.UtcNow;
            this.Save();
        }
        public void Reload(T o)
        {
            DbContext.Entry(o).Reload();
        }

        public void Attach(BaseEntity b, bool add = false)
        {   
            var q = b.GetType();
            var set = this.DbContext.Set(q);
            if (add)
            {
                set.Attach(b);
                return;
            }
            set.Attach(b);
        }

        public virtual IQueryable<T> Where(Expression<Func<T, bool>> predicate, bool showDeleted = false)
        {
            return showDeleted ? this.DbContext.Set<T>().Where(predicate) : this.DbContext.Set<T>().Where(x => !x.IsDeleted).Where(predicate);
        }

        public virtual IQueryable<T> GetAll(bool showDeleted = false)
        {
            return showDeleted ? this.DbContext.Set<T>() : this.DbContext.Set<T>().Where(x => !x.IsDeleted);
        }
    }
}
