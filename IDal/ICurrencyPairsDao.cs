using EFx.Model;

namespace EFx.IDal
{
    public interface ICurrencyPairsDao
    {
        CurrencyPairCollection GetCurrencyPairs();
    }
}