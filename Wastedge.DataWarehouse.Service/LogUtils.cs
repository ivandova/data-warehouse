using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace Wastedge.DataWarehouse.Service
{
    public static class LogUtils
    {
        public static void SetupEventLogging(string source, string log, int instanceId)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (log == null)
                throw new ArgumentNullException(nameof(log));

            var eventLog = new EventLog
            {
                Source = source,
                Log = log
            };

            if (!EventLog.SourceExists(eventLog.Source))
                EventLog.CreateEventSource(eventLog.Source, eventLog.Log);

            SetAppender(new EventLogAppender(eventLog, instanceId), Level.Info);
        }

        public static void SetupConsoleLogging()
        {
            var patternLayout = new PatternLayout { ConversionPattern = "%-4timestamp [%thread] %-5level %logger - %message%newline" };
            patternLayout.ActivateOptions();

            var appender = new ConsoleAppender { Layout = patternLayout };
            appender.ActivateOptions();

            SetAppender(appender, Level.Debug);
        }

        private static void SetAppender(IAppender appender, Level level)
        {
            var hierarchy = (Hierarchy)LogManager.GetRepository();
            
            hierarchy.Root.AddAppender(appender);
            hierarchy.Root.Level = level;
            hierarchy.Configured = true;
        }
    }
}
