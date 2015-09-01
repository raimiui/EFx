using System;
using System.Collections.Generic;
using System.Text;

namespace EFx.Model
{
    public enum CurrencyPairs
    {
        EUR_USD
    }

    public struct CurrencyPair
    {
        private readonly CurrencyPairs _currencyPair;
        private string _baseCurrency;
        private string _quoteCurrency;

        public CurrencyPair(CurrencyPairs currencyPair)
        {
            _currencyPair = currencyPair;
            _baseCurrency = currencyPair.ToString().Split('_')[0];
            _quoteCurrency = currencyPair.ToString().Split('_')[1];
        }

        public CurrencyPairs CurrencyPairSymbol { get { return _currencyPair; } }
        public string BaseCurrency { get { return _baseCurrency; } }
        public string QuoteCurrency { get { return _quoteCurrency; } }

        public override string ToString()
        {
            return _baseCurrency + _quoteCurrency;
        }
    }
}
