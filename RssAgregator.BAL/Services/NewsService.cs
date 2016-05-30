using Microsoft.Practices.Unity;
using RssAgregator.BAL.Interfaces.Services;
using RssAgregator.DAL;
using RssAgregator.Models.Results;
using RssAgregator.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RssAgregator.BAL.Services
{
    public class NewsService : INewsService
    {
        [Dependency]
        public ISettingService SettingService { get; set; }

        [Dependency]
        public ITranslateService TranslateService { get; set; }

        public GenericResult<IEnumerable<NewsModel>> GetNews(int pageSize, int pageNumber, bool hideAdult)
        {
            var result = new GenericResult<IEnumerable<NewsModel>>();

            try
            {
                if (pageSize > 0 && pageNumber > 0)
                {
                    using (var db = new RssAggregatorModelContainer())
                    {
                        result.SetDataResult(db.GetDBSet<News>(el => el.IsActive)
                                                .OrderBy(el => el.Id)
                                                .Skip(pageSize * (pageNumber - 1))
                                                .Take(pageSize)
                                                .ToList()
                                                .AsParallel()
                                                .Select(el => el.GetModel())
                                                .ToList());

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, LogTypeEnum.BAL);
                result.SetErrorResultCode(SettingService.GetUserFriendlyExceptionMessage());
            }

            return result;
        }

        public GenericResult<NewsModel> GetNewsItem(string newsItemId)
        {
            var result = new GenericResult<NewsModel>();

            try
            {
                if (!string.IsNullOrEmpty(newsItemId))
                {
                    using (var db = new RssAggregatorModelContainer())
                    {
                        result.SetDataResult(db.GetEntity<News>(el => el.IsActive && el.PostId == newsItemId).GetModel());

                    }
                }
                
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, LogTypeEnum.BAL);
                result.SetErrorResultCode(SettingService.GetUserFriendlyExceptionMessage());
            }

            return result;
        }


        public GenericResult<bool> AddEditNewsItem(NewsItemModel inputParams)
        {
            var result = new GenericResult<bool>();

            try
            {
                using (var db = new RssAggregatorModelContainer(true))
                {
                    var user = db.GetEntity<User>(el => el.Id == inputParams.UserId && el.IsActive);
                    var userName = user.Name.ToLower();

                    var translatePost = TranslateService.Translate(inputParams.PostName);
                    var postId = db.GetPostTransliteratedName(translatePost.InfoResult.ResultCode == Models.Enums.ResultCodeEnum.Success 
                                                                    ? translatePost.DataResult 
                                                                    : Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}{1}", userName, DateTime.UtcNow.Ticks))), 
                                                                userName);

                    var systemDataSource = db.GetEntity<DataSources>(el => el.Type == DataSourceEnum.System);


                    if (inputParams.IsNewOne && !string.IsNullOrEmpty(inputParams.PostId))
                    {
                        var newsEntity = db.GetEntity<News>(el => el.IsActive && el.User.Id == user.Id && el.PostId == inputParams.PostId);
                        if (newsEntity != null)
                        {
                            newsEntity.PostContent = inputParams.PostContent;
                            newsEntity.PostTags = inputParams.PostTags;
                            newsEntity.AdultContent = inputParams.AdultContent;
                            newsEntity.ModificationDateTime = DateTime.UtcNow;

                            result.SetDataResult(true);
                        }
                        else
                        {
                            Logger.LogException(string.Format("News item not found, expected user Id:{0}, expected news item id:{1}", user.Id, inputParams.PostId), LogTypeEnum.BAL);

                            result.SetErrorResultCode(SettingService.GetUserFriendlyExceptionMessage());
                        }
                    }
                    else
                    {
                        db.AddEntity(new News
                        {
                            PostId = postId,
                            AuthorId = userName,
                            AuthorName = userName,
                            AuthorLink = string.Format("{0}/{1}", systemDataSource.Uri.TrimEnd(new[] { '/' }), userName),
                            PostName = inputParams.PostName,
                            PostLink = postId,
                            PostContent = inputParams.PostContent,
                            PostTags = inputParams.PostTags,
                            IsActive = true,
                            AdultContent = inputParams.AdultContent,
                            CreationDateTime = DateTime.UtcNow,
                            ModificationDateTime = DateTime.UtcNow,

                            DataSource = systemDataSource,
                            User = user
                        });
                    }
                    
                    result.SetDataResult(true);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, LogTypeEnum.BAL);
                result.SetErrorResultCode(SettingService.GetUserFriendlyExceptionMessage());
            }

            return result;
        }

        public GenericResult<IEnumerable<KeyValuePair<string, int>>> GetAllNewsTags()
        {
            var result = new GenericResult<IEnumerable<KeyValuePair<string, int>>>();

            try
            {
                using (var db = new RssAggregatorModelContainer())
                {
                    result.SetDataResult(db.GetDBSet<News>(el => el.IsActive)
                                           .Select(el => el.PostTags)
                                           .ToList()
                                           .AsParallel()
                                           .SelectMany(el => el.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                                           .GroupBy(el => el)
                                           .ToDictionary(el => el.Key, el => el.Count()));
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, LogTypeEnum.BAL);
                result.SetErrorResultCode(SettingService.GetUserFriendlyExceptionMessage());
            }

            return result;
        }
    }
}
