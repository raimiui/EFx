using EFx.Model;

namespace EFx.IBll
{
    public interface IAnalysisService
    {
        TradeAction ProvideDecision(Trade lastTrade, Quote newQuote);
    }
}