using EFx.Bll;
using EFx.Dal;
using EFx.IBll;
using EFx.IDal;
using Microsoft.Practices.Unity;
using NHibernate;
using NHibernate.Cfg;

namespace EFx.Configurations
{
    public static class UnityContainerFactory
    {
        private static UnityContainer _unityContainer;

        public static UnityContainer UnityContainer
        {
            get { return _unityContainer ?? (_unityContainer = new UnityContainer()); }
        }

        internal static void ConfigureUnityContainer()
        {
            var cfg = new Configuration().Configure();
            var sessionFactory = cfg.BuildSessionFactory();
            RegisterTypes(sessionFactory);
        }

        private static void RegisterTypes(ISessionFactory sessionFactory)
        {
            UnityContainer
                // daos
                .RegisterType<ICurrencyPairsDao, CurrencyPairsDao>()
                .RegisterType<IHistoricalQuoteDao, HistoricalQuoteDao>()
                .RegisterType<IQuoteDao, QuoteDao>()
                .RegisterType<ITradeDao, TradeDao>()

                // services
                .RegisterType<IWorkerService, WorkerService>()
                .RegisterType<IQuoteService, QuoteService>()
                .RegisterType<ITradeService, TradeService>()
                .RegisterType<ITradingDao, TradingDao>()
                .RegisterType<IAnalysisService, AnalysisService>()
                .RegisterType<IAppSettingsService, AppSettingsService>()
                .RegisterType<ILogService, EventLogLoggerService>()

                // instances
                //TODO: manage session instances
                .RegisterInstance(sessionFactory.OpenSession())
                ;
        }
    }
}