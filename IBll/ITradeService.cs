using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFx.Model;

namespace EFx.IBll
{
    public interface ITradeService
    {
        Trade GetLastTrade();
        Trade GetById(int id);
        void Save(Trade trade);
        void Close(Trade trade);
    }
}
