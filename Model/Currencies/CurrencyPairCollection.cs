using System.Collections.ObjectModel;
using System.Linq;

namespace EFx.Model
{
    public class CurrencyPairCollection : Collection<CurrencyPair>
    {
        public CurrencyPair this[CurrencyPairs currencyPair]
        {
            get { return Items.First(x => x.CurrencyPairSymbol == currencyPair); }
        }
    }
}