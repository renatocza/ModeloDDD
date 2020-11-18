using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Insert(T obj);

        void Update(T obj);

        void Delete(Guid id);

        T Select(Guid id);

        T Single(Func<T, bool> p);

        IEnumerable<T> Select();

        IEnumerable<T> Select(Func<T, bool> p);
    }
}

