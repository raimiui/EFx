using System;
using System.Configuration;
using EFx.IBll;
using EFx.IBll.Exceptions;

namespace EFx.Bll
{
    public class AppSettingsService : IAppSettingsService
    {
        public string ServiceName { get { return GetAppSettingValue("serviceName"); } }
        public string WorkCycleInterval { get { return GetAppSettingValue("workCycleInterval"); } }

        public string MaxLogFileSizeInMB { get { return GetAppSettingValue("maxLogFileSizeInMB"); } }
        public int HistoryQuotesCountToAnalize { get { return Convert.ToInt32(GetAppSettingValue("HistoryQuotesCountToAnalize")); } }
        public decimal Capital { get { return Convert.ToDecimal(GetAppSettingValue("Capital")); } }
        public int Leverage { get { return Convert.ToInt32(GetAppSettingValue("Leverage")); } }
        public float Kpi_TakeOffTangentToOpen { get { return Convert.ToSingle(GetAppSettingValue("KpiTakeOffTangentToOpenv")); } }
        public float Kpi_LossLimitToClose { get { return Convert.ToSingle(GetAppSettingValue("KpiLossLimitToClose")); } }

        private string GetAppSettingValue(string appKey)
        {
            var value = ConfigurationManager.AppSettings[appKey];
            if (value == null) throw new AppSettingsNullValueException(appKey);
            return value;
        }
    }
}

