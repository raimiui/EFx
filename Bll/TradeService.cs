using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFx.IBll;
using EFx.IDal;
using EFx.Model;

namespace EFx.Bll
{
    public class TradeService : ITradeService
    {
        private readonly ITradeDao _tradeDao;

        public TradeService(ITradeDao tradeDao)
        {
            _tradeDao = tradeDao;
        }

        public Trade GetLastTrade()
        {
            return _tradeDao.GetLastTrade();// ?? new Trade{StartDate = DateTime.Now, Quotes = new List<Quote>()};
        }

        public Trade GetById(int id)
        {
            return _tradeDao.GetById(id);
        }

        public void Save(Trade trade)
        {
            _tradeDao.Save(trade);
        }

        public void Close(Trade trade)
        {
            trade.EndDate = DateTime.Now;
            _tradeDao.Save(trade);
        }
    }
}
