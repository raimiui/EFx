using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace EFx.Dal.Helpers
{
    public static class MarketInfoServiceHelper
    {
        static MarketInfoService.EasyForexMarketInfoServiceClient marketInfoService;

        public static MarketInfoService.EasyForexMarketInfoServiceClient MarketInfoService
        {
            get { return marketInfoService ?? (marketInfoService = new MarketInfoService.EasyForexMarketInfoServiceClient("MarketInfoAPI")); }
        }

        public static MarketInfoService.Session Login()
        {
            return MarketInfoService.Login(ConfigurationManager.AppSettings["ApplicationId"]);
        }

        public static bool Logout(MarketInfoService.Session session)
        {
            var result = MarketInfoService.Logout(session.SessionId);
            return result.ErrorTicket == null;
        }
    }
}