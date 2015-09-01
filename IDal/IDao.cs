using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace EFx.IDal
{
    public interface IDao
    {
        IDisposable BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

        void Commit();
    }

    public interface IDao<T> : IDao where T : class
    {
        T GetById(object id);

        void Save(T entity);

        void Delete(T entity);

        IQueryable<T> All();

        IList<T> GetAll();
    }
}