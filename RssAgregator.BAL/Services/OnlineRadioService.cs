using Microsoft.Practices.Unity;
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
        private const string ONLINE_RADIO_SEARCH_URL = "http://zaycev.net/search.html";
        private const string ONLINE_RADIO_SEARCH_BASE_URL = "http://zaycev.net";
        private const string ONLINE_RADIO_TYPE_AHEAD_URL = "http://zaycev.net/suggest4/";

        [Dependency]
        public ISettingService SettingService { get; set; }

        public async Task<GenericResult<IEnumerable<OnlineRadioServiceSongModel>>> Search(OnlineRadioServiceSearchModel searchModel)
        {
            var result = new GenericResult<IEnumerable<OnlineRadioServiceSongModel>>();

            try
            {
                var serverFactory = ParcerProviderFactory.GetFactory(DataSourceEnum.Zaycev);
                serverFactory.AddSearchCriteria(searchModel.Question);
                serverFactory.SetPageNumber(searchModel.PageNumber);

                var serverFactoryResult = await serverFactory.GetContent(new Uri(ONLINE_RADIO_SEARCH_URL));

                if (serverFactoryResult != null && serverFactoryResult.Any())
                {
                    var allSongTasks = new List<Task<OnlineRadioServiceSongModel>>();

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
                                                var serverResponceTask = webClient.GetAsync(string.Format("{0}/{1}", ONLINE_RADIO_SEARCH_BASE_URL, audioContent.Link.TrimStart(new[] { '/' })));

                                                var serverResult = await (await serverResponceTask).Content.ReadAsStringAsync();
                                                var serializedServerResult = JsonConvert.DeserializeObject<OnlineRadioServiceSongUrlJsonModel>(serverResult);
                                                if (serializedServerResult != null && !string.IsNullOrEmpty(serializedServerResult.url))
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
                    var serverResponceTask = webClient.GetAsync(string.Format("{0}?terms.prefix={1}", ONLINE_RADIO_TYPE_AHEAD_URL, question));

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
    }
}
