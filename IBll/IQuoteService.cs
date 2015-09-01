using System.Collections.Generic;
using System.Linq;
using EFx.Model;
using System;

namespace EFx.IBll
{
    public interface IQuoteService
    {
        IEnumerable<IEnumerable<Quote>> GetFullWeeks(string fileDir);
        Quote GetById(int id);
        void Save(Quote quote);
        void Delete(Quote quote);
        IList<Quote> GetAll();
        IList<Quote> GetLastSavedUntradedQuotes(Trade lastTrade);
        void CopyRecentHistoryQuotesToDb();        
        IEnumerable<HistoricalQuote> GetHistoricalQuotes(DateTime from);
    }
}