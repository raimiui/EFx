using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EFx.IDal;
using EFx.Model;
using NHibernate;
using NHibernate.Linq;

namespace EFx.Dal
{
    public class NHibernateDao : IDao, IDisposable
    {
        public NHibernateDao(ISession session)
        {
            Session = session;
        }

        protected ISession Session { get; private set; }

        public IDisposable BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return Session.BeginTransaction(isolationLevel);
        }

        public void Commit()
        {
            Session.Transaction.Commit();
        }

        public void Dispose()
        {
            Session.Close();
            Session.Dispose();
        }
    }

    public class NHibernateDao<T> : NHibernateDao, IDao<T> where T : Entity
    {
        public NHibernateDao(ISession session)
            : base(session)
        {
        }

        public T GetById(object id)
        {
            return Session.Get<T>(id);
        }

        public void Save(T entity)
        {
            using ( var transaction = Session.BeginTransaction())
            {
                Session.SaveOrUpdate(entity);
                transaction.Commit();
                //Session.Flush();
                //Session.Close()
            }
        }

        public IQueryable<T> All()
        {
            return Session.Query<T>();
        }

        public IList<T> GetAll()
        {
            return Session.Query<T>().ToList();
        }

        public void Delete(T entity)
        {
            Session.Delete(entity);
        }
    }
}
