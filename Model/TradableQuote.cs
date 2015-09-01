using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFx.Model
{
    public class TradableQuote
    {
        public string Id { get; set; }
        public double Ask { get; set; }
        public double Bid { get; set; }
        public string PairSymbol { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}