using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFx.IBll
{
    public interface IAppSettingsService
    {
        string ServiceName { get; }
        string WorkCycleInterval { get; }

        string MaxLogFileSizeInMB { get; }
        int HistoryQuotesCountToAnalize { get; }
        decimal Capital { get; }
        int Leverage { get; }
        float Kpi_TakeOffTangentToOpen { get; }
        float Kpi_LossLimitToClose { get; }
    }

    //public interface IResearchSettings
    //{
    //    string ServiceName { get; }
    //    string WorkCycleInterval { get; }
    //}

    //public interface ITradingServiceSettings
    //{
    //    string ServiceName { get; }
    //    string WorkCycleInterval { get; }

    //    string MaxLogFileSizeInMB { get; }
    //    int HistoryQuotesCountToAnalize { get; }
    //    decimal Capital { get; }
    //    int Leverage { get; }
    //    float Kpi_TakeOffTangentToOpen { get; }
    //    float Kpi_LossLimitToClose { get; }
    //}
}