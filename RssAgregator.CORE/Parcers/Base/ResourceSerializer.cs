using RssAgregator.CORE.Helpers;
using RssAgregator.CORE.Interfaces.Models.DOMObjectModel;
using RssAgregator.CORE.Models.DOMObjectModel;
using RssAgregator.CORE.Models.Enums;
using RssAgregator.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RssAgregator.CORE.Parcers.Base
{
    public class ResourceSerializer : IDisposable
    {
        protected CookieContainer CookieSharedContainer { get; private set; }
        protected HttpClientHandler HttpClientHandler { get; private set; }

        protected HttpClient WebClient { get; private set; }

        public ResourceSerializer(bool useCookie = false)
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
        }
        
        public virtual async Task<IEnumerable<IDOMElement>> GetSerializedResourceData(Uri resourceUrl, HttpMethodEnum requestType, Dictionary<string, string> requestContent = null)
        {
            return Serialize(await GetResourceData(resourceUrl, requestType, requestContent));
        }

        public virtual async Task<StringBuilder> GetResourceData(Uri resourceUrl, HttpMethodEnum requestType, Dictionary<string, string> requestContent = null)
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
#if DEBUG
                    File.WriteAllText("C:\\webResource.html", serverDataResult);
#endif
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

        public virtual IEnumerable<IDOMElement> Serialize(StringBuilder denormalizedDom)
        {
            var serializedList = new List<IDOMElement>();

            try
            {
                var splitCharacters = new[] { '>', '<' };
                var normalizedDom = denormalizedDom.Split(el => splitCharacters.Any(elem => elem == el), el => el == '>');

                IDOMElement parentNode = null;
                var complexElementProcessing = false;

                foreach (var el in normalizedDom)
                {
                    if (el.Length > 0)
                    {
                        var tagName = string.Empty;
                        var tagNameIndexEnd = el.FirstIndexOfAny(false, " ", "\t", "\n");

                        if (el.Length >= 2 && el[0] == '<' && (el[1] == '/' || (el[1] == '!' && complexElementProcessing)))
                        {
                            tagName = el.GetRange(2, tagNameIndexEnd > 0 ? tagNameIndexEnd - 1 : el.Length - 3).ToString().ToLower();

                            if (el[1] == '!' && complexElementProcessing && (tagName.Length > 2 && tagName[tagName.Length - 1] == '-' && tagName[tagName.Length - 2] == '-'))
                            {
                                tagName = "--";
                            }

                            var curretnNode = parentNode;

                            if (complexElementProcessing)
                            {
                                if (curretnNode.TagName == tagName)
                                {
                                    complexElementProcessing = false;

                                    curretnNode.IsDomElementComplete = true;

                                    while (curretnNode != null && curretnNode.IsDomElementComplete)
                                    {
                                        curretnNode = curretnNode.Parent;
                                    }

                                    parentNode = curretnNode == null ? null : curretnNode;
                                }
                                else
                                {
                                    parentNode.AddComplexElementContent(el.ToString());
                                }
                            }
                            else
                            {
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
                        }
                        else
                        {
                            if (complexElementProcessing)
                            {
                                parentNode.AddComplexElementContent(el.ToString());
                            }
                            else
                            {
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
                                    complexElementProcessing = parentNode.ElementType == DOMEleementTypeEnum.Script || 
                                                               parentNode.ElementType == DOMEleementTypeEnum.Comment ||
                                                               parentNode.ElementType == DOMEleementTypeEnum.NoScript;
                                }
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
