using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dynamitey;
using ERP.Core.Models;
using ERP.Core.Services;

namespace ERP.Services.Mocks
{
    public class MockService<T> : ICrudService<T> where T : BaseEntity, new()
    {
        private IList<T> BaseList = new List<T>();

        private void Typecheck()
        {
            var listType = typeof(T);
            if (listType == typeof(Skill))
            {
                BaseList = skillList as List<T>;
            }
            // else if () add more mockmodels here:
        }
        public IEnumerable<T> GetAll()
        {
            Typecheck();
            return BaseList;
        }

        public int Update(T item)
        {
            Typecheck();

            if (item.ID == 0)
            {
                item.ID = BaseList.Last().ID + 1;

                BaseList.Add(item);
                return BaseList.Last().ID;
            }

            BaseList[BaseList.IndexOf(BaseList.Single(i => i.ID == item.ID))] = item;
            return item.ID;
        }

        public void Attach(BaseEntity t)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id, bool hard = false)
        {
            throw new NotImplementedException();
        }

        public void DeleteMany(IEnumerable<int> ids, bool hard = false)
        {
            throw new NotImplementedException();
        }

        public void DetectChanges()
        {
            throw new NotImplementedException();
        }

        public int FastUpdate(T item)
        {
            throw new NotImplementedException();
        }


        public void Reload(T o)
        {
            throw new NotImplementedException();
        }

        public void Restore(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            
        }

        public void SetDetectChanges(bool value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> func, bool showDeleted = false)
        {
            throw new NotImplementedException();
        }

        T ICrudService<T>.Get(int id)
        {
            throw new NotImplementedException();
        }

        private IList<Skill> skillList = new List<Skill>()
        {
            new Skill()
            {
                CreateDate = DateTime.Now,
                CreatedBy = "Petya",
                ID = 1,
                IsDeleted = false,
                ModifyBy = "lenya",
                ModifyDate = DateTime.MinValue,
                SkillName = "c#"
            },
            new Skill()
            {
                CreateDate = DateTime.Now,
                CreatedBy = "Petya",
                ID = 2,
                IsDeleted = false,
                ModifyBy = "lenya",
                ModifyDate = DateTime.MinValue,
                SkillName = "java"
            },
            new Skill()
            {
                CreateDate = DateTime.Now,
                CreatedBy = "Petya",
                ID = 3,
                IsDeleted = false,
                ModifyBy = "lenya",
                ModifyDate = DateTime.MinValue,
                SkillName = "lisp"
            },
            new Skill()
            {
                CreateDate = DateTime.Now,
                CreatedBy = "Petya",
                ID = 4,
                IsDeleted = false,
                ModifyBy = "lenya",
                ModifyDate = DateTime.MinValue,
                SkillName = "worldofwarcraft"
            },
            new Skill()
            {
                CreateDate = DateTime.Now,
                CreatedBy = "Petya",
                ID = 5,
                IsDeleted = false,
                ModifyBy = "lenya",
                ModifyDate = DateTime.MinValue,
                SkillName = "c++"
            }
        };
    }
    
}

