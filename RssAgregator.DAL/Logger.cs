using System;

namespace RssAgregator.DAL
{
    public static class Logger
    {
        public static void LogException(Exception ex, LogTypeEnum type)
        {
            try
            {
                using (var db = new RssAggregatorModelContainer(true))
                {
                    var logEntity = new Log
                    {
                        Message = ex.Message,
                        StackTrace = ex.StackTrace,
                        Source = ex.Source,
                        DateTime = DateTime.UtcNow,
                        Type = type
                    };

                    db.AddEntity(logEntity);
                }
            }
            catch
            {
                //no actions can be done
            }
        }

        public static void LogException(Exception ex, LogTypeEnum type, string additionalData)
        {
            try
            {
                using (var db = new RssAggregatorModelContainer(true))
                {
                    var logEntity = new Log
                    {
                        Message = string.Format("{0}, {1}", additionalData, ex.Message),
                        StackTrace = ex.StackTrace,
                        Source = ex.Source,
                        DateTime = DateTime.UtcNow,
                        Type = type
                    };

                    db.AddEntity(logEntity);
                }
            }
            catch
            {
                //no actions can be done
            }
        }

        public static void LogException(string message, LogTypeEnum type, string stackTrace = null, string source = null)
        {
            try
            {
                using (var db = new RssAggregatorModelContainer(true))
                {
                    var logEntity = new Log
                    {
                        Message = message,
                        StackTrace = stackTrace,
                        Source = source,
                        DateTime = DateTime.UtcNow,
                        Type = type
                    };

                    db.AddEntity(logEntity);
                }
            }
            catch
            {
                //no actions can be done
            }
        }
    }
}