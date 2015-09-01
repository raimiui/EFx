using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace EFx.Dal.Helpers
{
    public static class TraddingServiceHelper
    {
        static TradingService.EasyForexTradingServiceClient tradingService;

        public static TradingService.EasyForexTradingServiceClient TradingService
        {
            get { return tradingService ?? (tradingService = new TradingService.EasyForexTradingServiceClient("TradingServiceHttp")); }
        }

        public static TradingService.Session Login()
        {
            return TradingService.Login(ConfigurationManager.AppSettings["ApplicationId"], ConfigurationManager.AppSettings["UserName"], ConfigurationManager.AppSettings["Password"]);
        }

        public static bool Logout(TradingService.Session session)
        {
            var result = TradingService.Logout(session.SessionId);
            return result.ErrorTicket == null;
        }
    }
}
