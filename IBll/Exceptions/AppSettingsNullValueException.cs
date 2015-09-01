using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFx.IBll.Exceptions
{
    public class AppSettingsNullValueException : Exception
    {
        public AppSettingsNullValueException(string appKey) : base(appKey)
        {
        }
    }
}
