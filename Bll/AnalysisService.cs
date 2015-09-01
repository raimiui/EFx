using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFx.IBll;
using EFx.IBll.Extensions;
using EFx.Model;

namespace EFx.Bll
{
    public class AnalysisService : IAnalysisService
    {
        private readonly IQuoteService _quoteService;
        private readonly ITradeService _tradeService;
        private readonly IAppSettingsService _configs;

        public AnalysisService(
            IQuoteService quoteService,
            ITradeService tradeService,
            IAppSettingsService configs
            )
        {
            _quoteService = quoteService;
            _tradeService = tradeService;
            _configs = configs;
        }

        //public TradeAction AnalizeWithNewQuote(Quote quote)
        //{
        //    //_quoteService.Save(quote);
        //    //var lastTrade = _tradeService.GetLastTrade();
        //    //if (lastTrade != null && lastTrade.EndDate == null)
        //    //{
        //    //    lastTrade.Quotes.Add(quote);
        //    //    _tradeService.Save(lastTrade);
        //    //}
        //    return ProvideDecision(quote);
        //}

        public TradeAction ProvideDecision(Trade lastTrade, Quote newQuote)
        {
            //TODO: PAY ATTENTION TO LEVERAGE!!!

            var action = TradeAction.None;
            
            if (!lastTrade.IsNew && lastTrade.IsOpen) // position is still open
            {
                //TODO: ble sutvarkyk!!!!!
                var historicalQuotes = new List<Quote>();// _quoteService.GetHistoricalQuotes(lastTrade.StartDate).ToList();
                var maxCloseRateOfAllTradeQuotes = historicalQuotes.Max(x => x.Close); //lastTrade.Quotes.Max(x => x.Close);
                var orderedTradeQuotes = historicalQuotes.OrderBy(x => x.CreatedOn); //lastTrade.Quotes.OrderBy(x => x.CreatedOn);
                var openQuote = orderedTradeQuotes.First();

                // last quote must be higher than close kpi
                var newOffset = newQuote.Close - openQuote.Open;
                var maxOffset = maxCloseRateOfAllTradeQuotes - openQuote.Open; // Historical, Of all current trade quotes

                var currentKpi = maxOffset != 0 ?
                    Convert.ToSingle(1 - newOffset / maxOffset) * 100 :
                    100; // should not face this situation

                if (_configs.Kpi_LossLimitToClose > currentKpi)
                    action = TradeAction.Close;
            }
            else // no open positions. Analyze last _configs.HistoryQuotesCountToAnalize
            {
                var untradedQuotes = _quoteService.GetLastSavedUntradedQuotes(lastTrade);
                untradedQuotes.Add(newQuote);
                untradedQuotes = untradedQuotes
                    .Skip(untradedQuotes.Count < _configs.HistoryQuotesCountToAnalize ? 0 : untradedQuotes.Count - _configs.HistoryQuotesCountToAnalize)
                    .ToList();

                if (untradedQuotes.AllInOneTrend() &&
                    untradedQuotes.TimeAmongQuotesIsAboutTheSame() &&
                    untradedQuotes.CanOpenPosition(_configs.Kpi_TakeOffTangentToOpen)
                    )
                    action = TradeAction.Open;
            }

            return action;
        }
    }
}
