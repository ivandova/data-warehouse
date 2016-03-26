using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Appender;
using log4net.Core;

namespace Wastedge.DataWarehouse.Service
{
    internal class EventLogAppender : AppenderSkeleton
    {
        private readonly EventLog _eventLog;
        private readonly int _instanceId;

        public EventLogAppender(EventLog eventLog, int instanceId)
        {
            if (eventLog == null)
                throw new ArgumentNullException(nameof(eventLog));

            _eventLog = eventLog;
            _instanceId = instanceId;
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            EventLogEntryType eventType;

            if (loggingEvent.Level <= Level.Info)
                eventType = EventLogEntryType.Information;
            else if (loggingEvent.Level >= Level.Error)
                eventType = EventLogEntryType.Error;
            else
                eventType = EventLogEntryType.Warning;

            string message = loggingEvent.RenderedMessage;

            if (loggingEvent.ExceptionObject != null)
            {
                var exception = loggingEvent.ExceptionObject;

                var sb = new StringBuilder()
                    .AppendLine(message)
                    .AppendLine();

                while (true)
                {
                    sb.Append(exception.Message).Append(" (").Append(exception.GetType().FullName).AppendLine(")");

                    if (exception.StackTrace != null)
                        sb.AppendLine().AppendLine(exception.StackTrace.TrimEnd());

                    if (exception.InnerException == null)
                        break;

                    exception = exception.InnerException;

                    sb.AppendLine("== Caused by ==").AppendLine();
                }

                message = sb.ToString();
            }

            _eventLog.WriteEntry(message, eventType, _instanceId);
        }
    }
}
