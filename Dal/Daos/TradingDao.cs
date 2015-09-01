using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Configuration;
using EFx.Dal.TradingService;
using EFx.IDal;
using AutoMapper;
using EFx.Model;
using Quote = EFx.Model.Quote;
using TradableQuote = EFx.Model.TradableQuote;
using CurrencyPair = EFx.Model.CurrencyPair;

namespace EFx.Dal
{
    public class TradingDao : ITradingDao
    {
        private CurrencyPairCollection _currencyPair;
        public TradingDao(ICurrencyPairsDao currencyPairsDao)
        {
            _currencyPair = currencyPairsDao.GetCurrencyPairs();
        }

        private TradableQuote GetLastTradingQuote()
        {
            var lastTradableQuote = GetTradingQuote().First();
            return lastTradableQuote;
        }

        private IEnumerable<TradableQuote> GetTradingQuote()
        {
            var tradingService = new TradingService.EasyForexTradingServiceClient("TradingServiceHttp");
            var session = tradingService.Login(
                ConfigurationManager.AppSettings["ApplicationId"],
                ConfigurationManager.AppSettings["UserName"],
                ConfigurationManager.AppSettings["Password"]);

            try
            {
                var tradableQuotes = tradingService.GetTradableQuotes(
                    session.SessionId,
                    new[] { new TradableQuoteRequestParams { PairSymbol = _currencyPair[CurrencyPairs.EUR_USD].ToString() } }
                    );
                //TODO: Handle tradableQuotes.ErrorTicket

                return Mapper.Map<IEnumerable<TradableQuote>>(tradableQuotes.QuotesList.OrderByDescending(x => x.TimeStamp));
            }
            catch (Exception e) { throw e; }
            finally { tradingService.Logout(session.SessionId); }
        }
    }
}
