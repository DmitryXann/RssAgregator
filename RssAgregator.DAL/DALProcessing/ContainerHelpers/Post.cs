using System;
using System.Data.Entity;

namespace RssAgregator.DAL
{
    public partial class RssAggregatorModelContainer : DbContext
    {
        public string GetPostTransliteratedName(string postName, string userName)
        {
            var dateTimeNow = DateTime.UtcNow;
            return string.Format("{0}_{1}_{2}",
                                            postName.Replace(' ', '_'),
                                            userName,
                                            string.Format("{0}-{1}-{2}_{3}-{4}-{5}",
                                                            dateTimeNow.Day < 10 ? "0" + dateTimeNow.Day.ToString() : dateTimeNow.Day.ToString(),
                                                            dateTimeNow.Month < 10 ? "0" + dateTimeNow.Month.ToString() : dateTimeNow.Month.ToString(),
                                                            dateTimeNow.Year,
                                                            dateTimeNow.Hour < 10 ? "0" + dateTimeNow.Hour.ToString() : dateTimeNow.Hour.ToString(),
                                                            dateTimeNow.Minute < 10 ? "0" + dateTimeNow.Minute.ToString() : dateTimeNow.Minute.ToString(),
                                                            dateTimeNow.Second < 10 ? "0" + dateTimeNow.Second.ToString() : dateTimeNow.Second.ToString()));
        }
    }
}
