using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFx.Model;

namespace EFx.IBll
{
    public interface IHistoryService
    {
        IEnumerable<Quote> GetHistory(Quote quote);
    }
}
