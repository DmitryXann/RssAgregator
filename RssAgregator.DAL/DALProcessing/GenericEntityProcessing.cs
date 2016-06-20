using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace RssAgregator.DAL
{
    public partial class RssAggregatorModelContainer : DbContext
    {
        private const string DATA_SOURCES_TYPE_NAME                 = "DataSources";
        private const string NEWS_TYPE_NAME                         = "News";
        private const string USER_TYPE_NAME                         = "User";
        private const string TEMPLATE_TYPE_NAME                     = "Template";
        private const string COMMENTS_TYPE_NAME                     = "Comments";
        private const string USERS_IMAGES_TYPE_NAME                 = "UsersImage";
        private const string ICONS_TYPE_NAME                        = "Icons";
        private const string LOG_TYPE_NAME                          = "Log";
        private const string NEWS_DATA_SOURCE_REQUEST_TYPE_NAME     = "NewDataSourceRequest";
        private const string USER_FEEDBACK_TYPE_NAME                = "UserFeedback";
        private const string USER_MASSAGES_TYPE_NAME                = "UserMessages";
        private const string SETTINGS_TYPE_NAME                     = "Settings";
        private const string SONGS_BLACK_LICT                       = "SongsBlackList";
        private const string TRANSLITERATION                        = "Transliteration";
        private const string NAVIGATION                             = "Navigation";
        private const string USER_ACTIVITY_LOG                      = "UserActivityLog";
        private const string TAGS                                   = "Tags";

        public void AddEntity<T>(T entity) where T : class
        {
            GetDBSet<T>().Add(entity);
        }

        public T GetEntity<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return GetDBSet<T>().FirstOrDefault(predicate);
        }

        public IQueryable<T> GetDBSet<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return GetDBSet<T>().Where(predicate);
        }

        public DbSet<T> GetDBSet<T>() where T : class
        {
            switch (typeof(T).Name)
            {
                case DATA_SOURCES_TYPE_NAME:
                    return DataSourcesSet as DbSet<T>;
                case NEWS_TYPE_NAME:
                    return NewsSet as DbSet<T>;
                case USER_TYPE_NAME:
                    return UserSet as DbSet<T>;
                case TEMPLATE_TYPE_NAME:
                    return TemplateSet as DbSet<T>;
                case COMMENTS_TYPE_NAME:
                    return CommentsSet as DbSet<T>;
                case USERS_IMAGES_TYPE_NAME:
                    return UsersImageSet as DbSet<T>;
                case ICONS_TYPE_NAME:
                    return IconsSet as DbSet<T>;
                case LOG_TYPE_NAME:
                    return LogSet as DbSet<T>;
                case NEWS_DATA_SOURCE_REQUEST_TYPE_NAME:
                    return NewDataSourceRequestSet as DbSet<T>;
                case USER_FEEDBACK_TYPE_NAME:
                    return UserFeedbackSet as DbSet<T>;
                case USER_MASSAGES_TYPE_NAME:
                    return UserMessagesSet as DbSet<T>;
                case SETTINGS_TYPE_NAME:
                    return SettingsSet as DbSet<T>;
                case SONGS_BLACK_LICT:
                    return SongsBlackListSet as DbSet<T>;
                case TRANSLITERATION:
                    return TransliterationSet as DbSet<T>;
                case NAVIGATION:
                    return NavigationSet as DbSet<T>;
                case USER_ACTIVITY_LOG:
                    return UserActivityLogSet as DbSet<T>;
                case TAGS:
                    return TagsSet as DbSet<T>;
                default:
                    throw new NotSupportedException(string.Format("Handling for {0} type not support", typeof(T).Name));

            }
        }
    }
}
