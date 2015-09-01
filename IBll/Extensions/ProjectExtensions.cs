using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFx.Model;

namespace EFx.IBll.Extensions
{
    public static class Extensions
    {
        public static bool TimeAmongQuotesIsAboutTheSame(this IEnumerable<Quote> list)
        {
            var deltaTimes = 
                list
                    .Skip(1)
                    .Select((q, i) => new { i, delta = q.CreatedOn.Ticks - list.ElementAt(i).CreatedOn.Ticks }) // get time deltas in ticks
                    .ToList();

            return
                deltaTimes
                    .All
                    (
                        x => deltaTimes
                              .Except(deltaTimes.Where(y => x.i == y.i)) // except current
                              .All(z => z.delta < x.delta * 1.2) // other delta times must be less than koef
                    );
        }

        public static bool CanOpenPosition(this IEnumerable<Quote> list, float kpi_TakeOffTangentToOpen)
        {
            return
                list
                    .Select((quote, idx) => new {quote, idx})
                    .Skip(1)
                    .All
                    (x =>
                         {
                             var previousQuote = list.ElementAt(x.idx - 1);
                             var currQuote = x.quote;

                             return
                                Convert.ToDouble(currQuote.Close - previousQuote.Open) * 10000
                                /
                                (currQuote.CreatedOn - previousQuote.CreatedOn).TotalMinutes
                                >
                                kpi_TakeOffTangentToOpen;
                         }
                    );
        }

        public static bool AllInOneTrend(this IEnumerable<Quote> list)
        {
            return
                list.Skip(list.Count() > 1 ? 1 : 0).Select((q, i) => new { q, i }).All(x => x.q.Close > list.ElementAt(x.i).Close)
                ||
                list.Skip(list.Count() > 1 ? 1 : 0).Select((q, i) => new { q, i }).All(x => x.q.Close < list.ElementAt(x.i).Close);
        }
    }
}
