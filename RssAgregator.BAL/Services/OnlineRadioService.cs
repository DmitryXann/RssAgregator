using Newtonsoft.Json;
using RssAgregator.BAL.Interfaces.Services;
using RssAgregator.CORE.Models.PostModel.PostContentModel;
using RssAgregator.CORE.Models.PostModel.PostContentModel.PostContentContainerModel;
using RssAgregator.CORE.Parcers;
using RssAgregator.DAL;
using RssAgregator.Models.Results;
using RssAgregator.Models.Services.OnlineRadio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RssAgregator.BAL.Services
{
    public class OnlineRadioService : IOnlineRadioService
    {
        private string _onlineRadionBaseURL;
        private string _onlineRadioSearchURl;
        private string _onlineRadioTypeAheadPostfix;

        public ISettingService SettingService { get; set; }

        public OnlineRadioService(ISettingService settingsService)
        {
            SettingService = settingsService;

            using (var db = new RssAggregatorModelContainer())
            {
                var onlineRadioEntity = db.GetEntity<DataSources>(el => el.Type == DataSourceEnum.OnlineRadio);

                _onlineRadionBaseURL = onlineRadioEntity.BaseUri;
                _onlineRadioSearchURl = onlineRadioEntity.Uri;

                var onlineRadioTypeAheadPostfix = settingsService.GetSetting("PLAYER_OnlineRadioTypeAheadPostfix");
                if (onlineRadioTypeAheadPostfix.InfoResult.ResultCode == Models.Enums.ResultCodeEnum.Success)
                {
                    _onlineRadioTypeAheadPostfix = string.Format("{0}/{1}", _onlineRadionBaseURL.TrimEnd(new[] { '/' }), onlineRadioTypeAheadPostfix.DataResult);
                }
            }
        }

        public async Task<GenericResult<IEnumerable<OnlineRadioServiceSongModel>>> Search(OnlineRadioServiceSearchModel searchModel)
        {
            var result = new GenericResult<IEnumerable<OnlineRadioServiceSongModel>>();

            try
            {
                var serverFactory = ParcerProviderFactory.GetFactory(DataSourceEnum.OnlineRadio);
                serverFactory.AddSearchCriteria(searchModel.Question);
                serverFactory.SetPageNumber(searchModel.PageNumber);

                var serverFactoryResult = await serverFactory.GetContent(new Uri(_onlineRadioSearchURl));

                if (serverFactoryResult != null && serverFactoryResult.Any())
                {
                    var allSongTasks = new List<Task<OnlineRadioServiceSongModel>>();
                    IEnumerable<SongsBlackList> songsBlackList;

                    using (var db = new RssAggregatorModelContainer())
                    {
                        songsBlackList = db.GetDBSet<SongsBlackList>(el => string.Compare(el.City, searchModel.City, true) == 0 && string.Compare(el.Country, searchModel.Country, true) == 0).ToList();
                    }

                    foreach (var postItem in serverFactoryResult)
                    {
                        foreach (var content in postItem.PostContent)
                        {
                            if (content.PostContentType == CORE.Models.Enums.PostContentTypeEnum.Audio)
                            {
                                foreach (var audioContent in ((BasePostContentModel<AudioPostContentContainerModel>)content).PostSpecificContent)
                                {
                                    if (!string.IsNullOrEmpty(audioContent.Link))
                                    {
                                        allSongTasks.Add(Task.Run<OnlineRadioServiceSongModel>(async () =>
                                        {
                                            using (var webClient = new HttpClient())
                                            {
                                                var serverResponceTask = webClient.GetAsync(string.Format("{0}/{1}", _onlineRadionBaseURL.TrimEnd(new[] { '/' }), audioContent.Link.TrimStart(new[] { '/' })));

                                                var serverResult = await (await serverResponceTask).Content.ReadAsStringAsync();
                                                var serializedServerResult = JsonConvert.DeserializeObject<OnlineRadioServiceSongUrlJsonModel>(serverResult);
                                                if (serializedServerResult != null && !string.IsNullOrEmpty(serializedServerResult.url) && !songsBlackList.Any(el => el.SongURL == serializedServerResult.url))
                                                {
                                                    return new OnlineRadioServiceSongModel
                                                    {
                                                        Artist = audioContent.Artist,
                                                        Name = audioContent.Name,
                                                        Link = serializedServerResult.url
                                                    };
                                                }

                                                return null;
                                            }
                                        }));
                                    }
                                }
                            }
                        }
                    }

                    result.SetDataResult(await Task.Run<IEnumerable<OnlineRadioServiceSongModel>>(() =>
                    {
                        Task.WaitAll(allSongTasks.ToArray());

                        return allSongTasks.AsParallel().Select(el => el.Result).Where(el => el != null);
                    }));
                }
                else
                {
                    result.SetDataResult(Enumerable.Empty<OnlineRadioServiceSongModel>());
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, LogTypeEnum.BAL);
                result.SetErrorResultCode(SettingService.GetUserFriendlyExceptionMessage());
            }

            return result;
        }

        public async Task<GenericResult<IEnumerable<string>>> TypeAheadSearch(string question)
        {
            var result = new GenericResult<IEnumerable<string>>();

            try
            {
                using (var webClient = new HttpClient())
                {
                    var serverResponceTask = webClient.GetAsync(string.Format("{0}/?terms.prefix={1}", _onlineRadioTypeAheadPostfix.TrimEnd(new[] { '/' }), question));

                    var serverResult = await (await serverResponceTask).Content.ReadAsStringAsync();
                    var serializedServerResult = JsonConvert.DeserializeObject<OnlineRadioTypeAhedJsonModel>(serverResult);

                    result.SetDataResult(serializedServerResult == null ? Enumerable.Empty<string>() : serializedServerResult.terms.GetAllKeys());
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, LogTypeEnum.BAL);
                result.SetErrorResultCode(SettingService.GetUserFriendlyExceptionMessage());
            }

            return result;
        }

        public GenericResult<bool> AddNotPlayebleSong(OnlineRadioNotPlayebleSongModel inputParams)
        {
            var result = new GenericResult<bool>();

            try
            {
                using (var db = new RssAggregatorModelContainer(true))
                {
                    db.AddEntity(new SongsBlackList
                    {
                        SongURL = inputParams.SongURL,
                        City = inputParams.City,
                        Country = inputParams.Country
                    });

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
    }
}
