
using NLog;

namespace Pub.API.Common {

    public static class LogHandler  {

        private static NLog.ILogger logger = LogManager.GetCurrentClassLogger();

        public static void LogDebug(string message) => logger.Debug(message);

        public static void LogError(string message) => logger.Error(message);

        public static void LogInfo(string message) => logger.Info(message);

        public static void LogWarning(string message) => logger.Warn(message);

    }
}
