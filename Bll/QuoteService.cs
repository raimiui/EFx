using System;
using System.Collections.Generic;
using System.Linq;
using EFx.IBll;
using EFx.IDal;
using EFx.Model;

namespace EFx.Bll
{
    public class QuoteService : IService<Quote>, IQuoteService
    {
        private readonly IQuoteDao _quoteDao;
        private readonly IHistoricalQuoteDao _historicalQuoteDao;
        private readonly IAppSettingsService _configs;

        public QuoteService(
            IQuoteDao quoteDao,
            IHistoricalQuoteDao historicalQuoteDao,
            IAppSettingsService appSettingsService
            )
        {
            _quoteDao = quoteDao;
            _historicalQuoteDao = historicalQuoteDao;
            _configs = appSettingsService;
        }

        public IList<Quote> GetAll()
        {
            return _quoteDao.GetAll();
        }

        public IList<Quote> GetLastSavedUntradedQuotes(Trade lastTrade)
        {
            throw new NotImplementedException();
            ////TODO: TEST!!!
            //return _quoteDao
            //    .GetLastQuotes(_configs.HistoryQuotesCountToAnalize)
            //    .Where(
            //        x => lastTrade.IsNew
            //            ||
            //            !lastTrade
            //                .Quotes
            //            //.OrderBy(q => q.CreatedOn)
            //            //.Take(lastTrade.Quotes.Count - 1)
            //                .Contains(x))
            //    .ToList();
        }

        public IEnumerable<IEnumerable<Quote>> GetFullWeeks(string fileDir)
        {
            var quotes = new List<Quote>();

            var quoteSequences = new List<List<Quote>>();

            var min15 = new TimeSpan(0, 15, 0);

            foreach (var quote in quotes)
            {
                if (quoteSequences.Count == 0)
                {
                    quoteSequences.Add(new List<Quote> { quote });
                }
                else
                {
                    if (quote.CreatedOn.Subtract(quoteSequences.Last().Last().CreatedOn) == min15)
                    {
                        quoteSequences.Last().Add(quote);
                    }
                    else
                    {
                        quoteSequences.Add(new List<Quote> { quote });
                    }

                }
            }

            var dataNotStartingOnMondayAndEndingOnFriday = quoteSequences
                .Where(x => x.First().CreatedOn.DayOfWeek != DayOfWeek.Monday || x.Last().CreatedOn.DayOfWeek != DayOfWeek.Friday).ToList();

            return quoteSequences.Except(dataNotStartingOnMondayAndEndingOnFriday).ToList();
        }

        public Quote GetById(int id)
        {
            return _quoteDao.GetById(id);
        }

        public void Save(Quote quote)
        {
            _quoteDao.Save(quote);
        }

        public void Delete(Quote quote)
        {
            _quoteDao.Delete(quote);
        }

        public void CopyRecentHistoryQuotesToDb()
        {
            var lastSavedHistoricalQuote = _historicalQuoteDao.LastSavedHistoricalQuote() ?? new HistoricalQuote();

            var askQuotes = _historicalQuoteDao.GetHistoricalQuotes(RateType.Ask, null).Where(hq => (hq.CreatedOn.Ticks - lastSavedHistoricalQuote.CreatedOn.Ticks) / TimeSpan.TicksPerSecond > 0).ToList();
            var bidQuotes = _historicalQuoteDao.GetHistoricalQuotes(RateType.Bid, null).Where(hq => (hq.CreatedOn.Ticks - lastSavedHistoricalQuote.CreatedOn.Ticks) / TimeSpan.TicksPerSecond > 0).ToList();
            
            _historicalQuoteDao.SaveAll(askQuotes);
            _historicalQuoteDao.SaveAll(bidQuotes);
        }

        public IEnumerable<HistoricalQuote> GetHistoricalQuotes(DateTime @from)
        {
            throw new NotImplementedException();
        }
    }
}
