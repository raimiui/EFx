using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFx.IBll;
using EFx.Model;

namespace EFx.Bll
{
    public class HistoryService : IHistoryService
    {
        public IEnumerable<Quote> GetHistory(Quote quote)
        {
            throw new NotImplementedException();
        }
    }
}
