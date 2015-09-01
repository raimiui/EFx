using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFx.IDal;
using EFx.Model;

namespace EFx.Dal
{
    public class CurrencyPairsDao : ICurrencyPairsDao
    {
        public CurrencyPairCollection GetCurrencyPairs()
        {
            var currencyPairsCollection = new CurrencyPairCollection();
            foreach (CurrencyPairs currencyPairValue in Enum.GetValues(typeof(CurrencyPairs)))
                currencyPairsCollection.Add(new CurrencyPair(currencyPairValue));
            return currencyPairsCollection;

        }
    }
}
