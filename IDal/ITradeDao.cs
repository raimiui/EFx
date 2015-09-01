using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFx.Model;

namespace EFx.IDal
{
    public interface ITradeDao : IDao<Trade>
    {
        Trade GetLastTrade();
    }
}
