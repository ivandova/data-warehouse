using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wastedge.DataWarehouse
{
    public class LogLine
    {
        public DateTime Start { get; }
        public DateTime End { get; }
        public int Duration { get; }
        public string Path { get; }
        public string Error { get; }
        public int? Records { get; }

        public LogLine(DateTime start, DateTime end, int duration, string path, string error, int? records)
        {
            Start = start;
            End = end;
            Duration = duration;
            Path = path;
            Error = error;
            Records = records;
        }
    }
}
