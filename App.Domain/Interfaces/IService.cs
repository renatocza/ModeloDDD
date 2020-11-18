using FluentValidation;
using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Interfaces
{
    public interface IService<T> where T : BaseEntity
    {
        T Post<V>(T obj) where V : AbstractValidator<T>;
        T Put<V>(T obj) where V : AbstractValidator<T>;
        void Delete(Guid id);
        T Get(Guid id);
        T Single(Func<T, bool> p);
        IList<T> Get();
        IEnumerable<T> Get(Func<T, bool> p);
    }
}
