using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EFx.IDal;
using EFx.Dal.Helpers;
using EFx.Model;
using NHibernate;
using NHibernate.Criterion;
using AutoMapper;
using System.Configuration;

namespace EFx.Dal
{
    public class QuoteDao : NHibernateDao<Quote>, IQuoteDao
    {
        private readonly CurrencyPairCollection _currencyPairs;

        public QuoteDao(ISession session, ICurrencyPairsDao currencyPairsDao) : base(session)
        {
            _currencyPairs = currencyPairsDao.GetCurrencyPairs();
        }
    }
}
