using System;
using EFx.Model;
using EFx.IBll;
using EFx.IDal;

namespace EFx.Bll
{
    public class WorkerService : IWorkerService
    {
        private readonly ITradingDao _tradingDao;
        private readonly ITradeDao _tradeDao;
        private readonly ILogService _logService;
        private readonly IQuoteService _quoteService;

        public WorkerService(ITradingDao tradingDao, ITradeDao tradeDao, ILogService logService, IQuoteService quoteService)
        {
            _tradingDao = tradingDao;
            _tradeDao = tradeDao;
            _logService = logService;
            _quoteService = quoteService;
        }

        public void Work()
        {
            _quoteService.CopyRecentHistoryQuotesToDb();
            //_tradeDao.Save(new Trade{StartDate = DateTime.Now, EndDate = DateTime.Now});
            
        }
    }
}