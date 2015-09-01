using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFx.Model
{
    /// <summary>
    /// Describes direction of a trend
    /// </summary>
    public enum TradeType
    {
        /// <summary> - Sell(bid) base currency, graph down. </summary>
        Sell = 1,
        /// <summary> - Buy(ask) base currency, graph up. </summary>
        Buy
        
    }

    public class Trade : Entity
    {
        public TradeType TradeType { get; private set; }

        public Trade(TradeType tradeType)
        {
            TradeType = tradeType;
        }

        public virtual Quote OpenQuote { get; set; }
        public virtual Quote MaxQuote { get; set; }
        public virtual Quote CloseQuote { get; set; }

        public virtual DateTime StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }

        public virtual bool IsOpen { get { return EndDate == null; } }
    }
}
