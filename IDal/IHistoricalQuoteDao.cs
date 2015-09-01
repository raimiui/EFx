using System;
using System.Collections.Generic;
using EFx.Model;

namespace EFx.IDal
{
    public interface IHistoricalQuoteDao
    {
        HistoricalQuote LastSavedHistoricalQuote();
        IEnumerable<HistoricalQuote> GetHistoricalQuotes(RateType rateType, DateTime? from);
        void SaveAll(IEnumerable<HistoricalQuote> historicalQuotes);
    }
}