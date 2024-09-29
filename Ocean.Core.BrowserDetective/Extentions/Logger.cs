using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Ocean.Core.BrowserDetective.Extentions
{
    public static class LoggerExtention
    {
        public static void Loging(this ILogger logger, LogLevel level, string message)
        {
            if (logger == null)
                return;
            if (string.IsNullOrEmpty(message) == false)
                logger.Log(level, message);
        }
    }
}
