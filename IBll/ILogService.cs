using System;

namespace EFx.IBll
{
    public interface ILogService
    {
        void Log(string message);
        void Log(Exception ex);
    }
}
