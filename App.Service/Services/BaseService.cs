using FluentValidation;
using App.Domain.Entities;
using App.Domain.Interfaces;
using App.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using App.Infra.Data.Context;
using System.Linq;

namespace App.Service.Services
{
    public class BaseService<T> : IService<T> where T : BaseEntity
    {
        private BaseRepository<T> repository;

        public BaseService(MsSqlContext context)
        {
            repository = new BaseRepository<T>(context);
        }

        public T Post<V>(T obj) where V : AbstractValidator<T>
        {
            Validate(obj, Activator.CreateInstance<V>());

            repository.Insert(obj);
            return obj;
        }

        public T Put<V>(T obj) where V : AbstractValidator<T>
        {
            Validate(obj, Activator.CreateInstance<V>());

            repository.Update(obj);
            return obj;
        }

        public void Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("The id can't be empty.");

            repository.Delete(id);
        }

        public IList<T> Get()
        {
            return repository.Select().ToList();
        }

        public T Get(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("The id can't be empty.");

            return repository.Select(id);
        }

        private void Validate(T obj, AbstractValidator<T> validator)
        {
            if (obj == null)
                throw new Exception("No data found!");

            validator.ValidateAndThrow(obj);
        }

        public T Single(Func<T, bool> p)
        {
            return repository.Single(p);
        }

        public IEnumerable<T> Get(Func<T, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}
