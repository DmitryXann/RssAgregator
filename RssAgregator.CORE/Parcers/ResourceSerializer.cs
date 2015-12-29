﻿using RssAgregator.CORE.Helpers;
using RssAgregator.CORE.Interfaces.DOMObjectModel;
using RssAgregator.CORE.Models.Enums;
using RssAgregator.CORE.Parcers.DOMObjectModel;
using RssAgregator.CORE.Parcers.XMLGuidePostModelParcer;
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
    public abstract class ResourceSerializer : IDisposable
    {
        protected CookieContainer CookieSharedContainer { get; set; }
        protected HttpClientHandler HttpClientHandler { get; set; }

        protected XMLGuidePostModelCreator XMLGuidePostModeCreator { get; set; }

        protected ResourceSerializer(bool useCookie, XDocument xmlParceRules)
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

            XMLGuidePostModeCreator = new XMLGuidePostModelCreator(xmlParceRules);
        }

        protected virtual async Task<StringBuilder> GetResourceData(Uri resourceUrl, HttpMethodEnum requestType, HttpContent requestContent = null)
        {
            StringBuilder result = null;

            using (var webClient = new HttpClient(HttpClientHandler))
            {
                Task<HttpResponseMessage> serverResponceTask = null;

                switch (requestType)
                { 
                    case HttpMethodEnum.GET:
                        serverResponceTask = webClient.GetAsync(resourceUrl);
                        break;
                    case HttpMethodEnum.PUT:
                        serverResponceTask = webClient.PutAsync(resourceUrl, requestContent);
                        break;
                    case HttpMethodEnum.POST:
                        serverResponceTask = webClient.PostAsync(resourceUrl, requestContent);
                        break;
                }

                if (serverResponceTask != null)
                {
                    var serverDataResult = await (await serverResponceTask).Content.ReadAsStringAsync();
                    result = new StringBuilder(serverDataResult);
                }

            }

            return result;
        }

        protected virtual IEnumerable<IDOMElement> Serialize(StringBuilder denormalizedDom)
        {
            var splitCharacters = new[] { '>', '<' };
            var normalizedDom = denormalizedDom.Split(el => splitCharacters.Any(elem => elem == el), el => el == '>');

            var serializedList = new List<IDOMElement>();
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

            return serializedList;
        }

        public void Dispose()
        {
            if (HttpClientHandler != null)
            {
                HttpClientHandler.Dispose();
            }
        }
    }
}
