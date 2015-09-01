using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFx.IDal;
using EFx.Model;
using NHibernate;
using NHibernate.Criterion;

namespace EFx.Dal
{
    public class TradeDao : NHibernateDao<Trade>, ITradeDao
    {
        public TradeDao(ISession session) : base(session)
        {
        }

        public Trade GetLastTrade()
        {
            return Session.CreateCriteria<Trade>()
                .AddOrder(Order.Desc("StartDate"))
                .SetMaxResults(1)
                .UniqueResult<Trade>();
        }
    }
}
