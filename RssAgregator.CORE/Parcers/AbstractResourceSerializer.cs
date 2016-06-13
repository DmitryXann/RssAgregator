using RssAgregator.CORE.Helpers;
using RssAgregator.CORE.Interfaces.DOMObjectModel;
using RssAgregator.CORE.Models.Enums;
using RssAgregator.CORE.Parcers.DOMObjectModel;
using RssAgregator.CORE.Parcers.XMLGuidePostModelParcer;
using RssAgregator.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RssAgregator.CORE.Parcers
{
    public abstract class AbstractResourceSerializer : IDisposable
    {
        protected CookieContainer CookieSharedContainer { get; set; }
        protected HttpClientHandler HttpClientHandler { get; set; }

        protected HttpClient WebClient { get; set; }

        protected XMLGuidePostModelCreator XMLGuidePostModeCreator { get; set; }

        protected AbstractResourceSerializer(bool useCookie, XDocument xmlParceRules)
        {
            if (useCookie)
            {
                CookieSharedContainer = new CookieContainer();
                HttpClientHandler = new HttpClientHandler { CookieContainer = CookieSharedContainer };
            }
            else
            {
                HttpClientHandler = new HttpClientHandler();
            }

            WebClient = new HttpClient(HttpClientHandler);
            XMLGuidePostModeCreator = new XMLGuidePostModelCreator(xmlParceRules);
        }

        protected virtual async Task<StringBuilder> GetResourceData(Uri resourceUrl, HttpMethodEnum requestType, Dictionary<string, string> requestContent = null)
        {
            StringBuilder result = null;

            try
            {
                Task<HttpResponseMessage> serverResponceTask = null;

                switch (requestType)
                {
                    case HttpMethodEnum.GET:
                        var getParams = string.Empty;

                        if (requestContent != null && requestContent.Any())
                        {
                            var firstElement = requestContent.First();
                            getParams += string.Format("?{0}={1}", firstElement.Key, firstElement.Value);

                            foreach (var el in requestContent.Skip(1))
                            {
                                getParams += string.Format("&{0}={1}", el.Key, el.Value);
                            }
                        }

                        serverResponceTask = WebClient.GetAsync(resourceUrl + getParams);
                        break;
                    case HttpMethodEnum.PUT:
                        serverResponceTask = WebClient.PutAsync(resourceUrl, new FormUrlEncodedContent(requestContent));
                        break;
                    case HttpMethodEnum.POST:
                        serverResponceTask = WebClient.PostAsync(resourceUrl, new FormUrlEncodedContent(requestContent));
                        break;
                }

                if (serverResponceTask != null)
                {
                    var serverDataResult = await (await serverResponceTask).Content.ReadAsStringAsync();
                    result = new StringBuilder(serverDataResult);
                }
            }
            catch(Exception ex)
            {
                Logger.LogException(ex, LogTypeEnum.CORE,
                                    string.Format("GetResourceData for {0} parser failed, URL: {1}, RequestType: {2}, RequestContent: {3}",
                                                    GetType().Name,
                                                    resourceUrl.AbsoluteUri,
                                                    requestType,
                                                    requestContent == null
                                                        ? string.Empty
                                                        : requestContent.Aggregate(string.Empty, (agg, el) => agg + string.Format("Key: {0}, Value: {1}", el.Key, el.Value))));
            }

            return result;
        }

        protected virtual IEnumerable<IDOMElement> Serialize(StringBuilder denormalizedDom)
        {
            var serializedList = new List<IDOMElement>();

            try
            {
                var splitCharacters = new[] { '>', '<' };
                var normalizedDom = denormalizedDom.Split(el => splitCharacters.Any(elem => elem == el), el => el == '>');

                IDOMElement parentNode = null;

                foreach (var el in normalizedDom)
                {
                    if (el.Length > 0)
                    {
                        var tagName = string.Empty;
                        var tagNameIndexEnd = el.FirstIndexOfAny(false, " ", "\t", "\n");

                        if (el.Length >= 2 && el[0] == '<' && el[1] == '/')
                        {
                            tagName = el.GetRange(2, tagNameIndexEnd > 0 ? tagNameIndexEnd - 1 : el.Length - 3).ToString().ToLower();

                            var curretnNode = parentNode;

                            while (curretnNode != null && (curretnNode.TagName != tagName || curretnNode.IsDomElementComplete))
                            {
                                curretnNode = curretnNode.Parent;
                            }

                            if (curretnNode != null && curretnNode.TagName == tagName && !curretnNode.IsDomElementComplete)
                            {
                                curretnNode.IsDomElementComplete = true;

                                while (curretnNode != null && curretnNode.IsDomElementComplete)
                                {
                                    curretnNode = curretnNode.Parent;
                                }

                                parentNode = curretnNode == null ? null : curretnNode;
                            }
                        }
                        else
                        {
                            //if (tagNameIndexEnd > 0)
                            //{
                            //    tagName = el.GetRange(1, tagNameIndexEnd - 1).ToString().ToLower();
                            //}

                            var newElement = new DOMElement(parentNode, el);

                            if (parentNode == null)
                            {
                                serializedList.Add(newElement);
                            }
                            else
                            {
                                parentNode.AddChild(newElement);
                            }

                            if (!newElement.IsDomElementComplete && !newElement.BadTag)
                            {
                                parentNode = newElement;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, LogTypeEnum.CORE, string.Format("Serialize for {0} parser failed", GetType().Name));
            }

            return serializedList;
        }

        public void Dispose()
        {
            if (WebClient != null)
            {
                WebClient.Dispose();
            }

            if (HttpClientHandler != null)
            {
                HttpClientHandler.Dispose();
            }
        }
    }
}
