using App.Domain.Entities;
using App.Domain.Interfaces;
using App.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Infra.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private MsSqlContext context;

        public BaseRepository(MsSqlContext context)
        {
            this.context = context;
        }

        public void Insert(T obj)
        {
            context.Set<T>().Add(obj);
            context.SaveChanges();
        }

        public void Update(T obj)
        {
            context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            context.Set<T>().Remove(Select(id));
            context.SaveChanges();
        }

        public IEnumerable<T> Select()
        {
            return context.Set<T>().ToList();
        }

        public T Select(Guid id)
        {
            return context.Set<T>().FirstOrDefault(x=>x.Id == id);
        }

        public T Single(Func<T, bool> p)
        {
            return context.Set<T>().FirstOrDefault(p);
        }
        public IEnumerable<T> Select(Func<T, bool> p)
        {
            return context.Set<T>().Where(p);
        }

        
    }
}
