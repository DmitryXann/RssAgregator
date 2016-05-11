﻿using System;

namespace RssAgregator.DAL
{
    public static class Logger
    {
        public static void LogException(Exception ex, LogTypeEnum type)
        {
            using (var db = new RssAggregatorModelContainer(true))
            {
                var logEntity = new Log
                {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    Source = ex.Source,
                    DateTime = DateTime.Now,
                    Type = type
                };

                db.AddEntity(logEntity);
            }
        }

        public static void LogException(string message, LogTypeEnum type)
        {
            using (var db = new RssAggregatorModelContainer(true))
            {
                var logEntity = new Log
                {
                    Message = message,
                    DateTime = DateTime.Now,
                    Type = type
                };

                db.AddEntity(logEntity);
            }
        }
    }
}
