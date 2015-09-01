using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFx.Model
{
    public class Quote : Entity
    {
        public virtual double Open { get; set; }
        public virtual double Close { get; set; }
        public virtual double High { get; set; }
        public virtual double Low { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public virtual Trade Trade { get; set; }

        public virtual double Ask { get; set; } // I buy base currency
        public virtual double Bid { get; set; } // I sell base currency
    }
}
