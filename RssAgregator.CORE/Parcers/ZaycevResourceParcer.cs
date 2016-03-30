﻿using RssAgregator.CORE.Interfaces.Parcers;
using RssAgregator.CORE.Models.Enums;
using RssAgregator.CORE.Models.PostModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RssAgregator.CORE.Parcers
{
    public class ZaycevResourceParcer : AbstractResourceSerializer, IResourceParcer
    {
        private const int DEFAULT_PAGE_COUNT = 0;
        private const string PAGE_COUNT_KEY = "page";
        private const string SEARCH_CRITEREA_KEY = "query_search";

        private Dictionary<string, string> _vzaycevPostData;

        private int _pageOffsetCount;
        private int _pageOffsetMultiplier;
        private bool _pageAlredySetted;

        public int DefaultPageCount
        {
            get { return DEFAULT_PAGE_COUNT; }
        }

        private ZaycevResourceParcer(XDocument xmlGuide) 
            : base(false, xmlGuide)
        {
            _pageOffsetCount = 1;
            _pageOffsetMultiplier = 0;

            _vzaycevPostData = new Dictionary<string, string>();
        }

        public async Task<IEnumerable<PostModel>> GetContent(Uri expectedUri)
        {
            if (!_pageAlredySetted)
            {
                _vzaycevPostData[PAGE_COUNT_KEY] = (DefaultPageCount + (_pageOffsetCount * _pageOffsetMultiplier++)).ToString();
            }
            
            IEnumerable<PostModel> result = null;

            var denormalizedData = await GetResourceData(expectedUri, HttpMethodEnum.GET, _vzaycevPostData);

            var serializedData = Serialize(denormalizedData);
            if (serializedData.Any())
            {
                result = XMLGuidePostModeCreator.CreatePostModel(serializedData);
            }

#if DEBUG
            //File.WriteAllText("C:\\zaicev.html", denormalizedData.ToString());
#endif

            return result;
        }

        public void AddSearchCriteria(string queston)
        {
            _vzaycevPostData[SEARCH_CRITEREA_KEY] = queston;
        }

        public void SetPageNumber(int currentPage)
        {
            _pageOffsetMultiplier = currentPage;
            _vzaycevPostData[PAGE_COUNT_KEY] = (DefaultPageCount + (_pageOffsetCount * _pageOffsetMultiplier)).ToString();
            _pageAlredySetted = true;
        }

        public void ResetPageCounter()
        {
            _pageOffsetMultiplier = 0;
        }
    }
}
