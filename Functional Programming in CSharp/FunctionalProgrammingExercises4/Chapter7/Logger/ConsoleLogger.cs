using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter7.Logger
{
    public static class ConsoleLogger
    {
        enum Level { Debug, Info, Error }

        delegate void Log(Level level, string message);

        static Log consoleLogger = (Level level, string message)
           => Console.WriteLine($"[{level}]: {message}");

        static void Debug(this Log log, string message)
           => log(Level.Debug, message);

        static void Info(this Log log, string message)
           => log(Level.Info, message);

        static void Error(this Log log, string message)
           => log(Level.Error, message);

        public static void RunLogger()
           => ConsumeLog(consoleLogger);

        static void ConsumeLog(Log log)
           => log.Info("this is an info message");
    }
}
