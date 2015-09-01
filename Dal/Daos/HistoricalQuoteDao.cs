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
    public class HistoricalQuoteDao : NHibernateDao<HistoricalQuote>, IHistoricalQuoteDao
    {
        private readonly CurrencyPairCollection _currencyPairs;

        public HistoricalQuoteDao(ISession session, ICurrencyPairsDao currencyPairsDao)
            : base(session)
        {
            _currencyPairs = currencyPairsDao.GetCurrencyPairs();
        }

        public HistoricalQuote LastSavedHistoricalQuote()
        {
            return Session.CreateCriteria<HistoricalQuote>()
                .AddOrder(Order.Desc("CreatedOn"))
                .SetMaxResults(1)
                .UniqueResult<HistoricalQuote>();
        }

        public IEnumerable<HistoricalQuote> GetHistoricalQuotes(RateType rateType, DateTime? from)
        {
            //var tsession = TraddingServiceHelper.Login();
            //try
            //{
            //    var result = TraddingServiceHelper.TradingService.GetProductSet(tsession.SessionId, TradingService.Products.DayTrading);
            //    ITradableQuote oneTime = TraddingServiceHelper.TradingService.sub api.SubscribeToTradableQuotes(curr1, curr2, null);
            //}
            //catch (Exception e) { throw e; }
            //finally { TraddingServiceHelper.Logout(tsession); }

            //if (!from.HasValue)
            //    from = DateTime.Now.AddDays(-4);

            //TODO: implement IDisposable to smth and use using(...)
            var session = MarketInfoServiceHelper.Login();

            var currencyPairEurUsd = _currencyPairs[CurrencyPairs.EUR_USD];

            try
            {
                var historicalQuotes = MarketInfoServiceHelper.MarketInfoService.GetHistoricalQuotes(
                            session.SessionId,
                            Mapper.Map<MarketInfoService.RateTypeIds>(rateType),
                            currencyPairEurUsd.BaseCurrency,
                            currencyPairEurUsd.QuoteCurrency,
                            1,
                            null,
                            from,
                            DateTime.Now,
                            null);

                var mappedHistoricalQuotes = Mapper.Map<MarketInfoService.HistoricalQuote[], HistoricalQuote[]>(historicalQuotes.HistoricalQuotesList)
                    .Select(hq => { hq.RateType = rateType; return hq; });

                return mappedHistoricalQuotes;
            }
            catch (Exception e) { throw e; }
            finally { MarketInfoServiceHelper.Logout(session); }
        }

        public void SaveAll(IEnumerable<HistoricalQuote> historicalQuotes)
        {
            foreach (var historicalQuote in historicalQuotes)
                Session.Save(historicalQuote);
        }
    }
}
