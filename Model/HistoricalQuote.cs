using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFx.Model
{
    public enum RateType { Bid = 1, Ask = 2, Mid = 3 }

    public class HistoricalQuote : Entity
    {
        public virtual RateType RateType { get; set; }
        public virtual double Open { get; set; }
        public virtual double Close { get; set; }
        public virtual double High { get; set; }
        public virtual double Low { get; set; }
        public virtual DateTime CreatedOn { get; set; }
    }
}
