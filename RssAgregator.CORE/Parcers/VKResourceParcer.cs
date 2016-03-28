using RssAgregator.CORE.Interfaces.Parcers;
using RssAgregator.CORE.Models.Enums;
using RssAgregator.CORE.Models.PostModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RssAgregator.CORE.Parcers
{
    public class VKResourceParcer : ResourceSerializer, IResourceParcer
    {
        private const int DEFAULT_PAGE_COUNT = 8;
        private const string PAGE_COUNT_KEY = "offset";

        private Dictionary<string, string> _vkPostData;

        private int _pageOffsetCount;
        private int _pageOffsetMultiplier;

        public int DefaultPageCount
        {
            get { return DEFAULT_PAGE_COUNT; }
        }

        private VKResourceParcer(XDocument xmlGuide) 
            : base(false, xmlGuide)
        {
            _pageOffsetCount = 10;
            _pageOffsetMultiplier = 0;

            _vkPostData = new Dictionary<string, string>
                    {
                        {"act", "get_wall"},
                        {"al", "1"},
                        {"type", "own"}
                    };
        }

        public async Task<IEnumerable<PostModel>> GetContent(Uri expectedUri)
        {
            _vkPostData[PAGE_COUNT_KEY] = (DefaultPageCount + (_pageOffsetCount * _pageOffsetMultiplier++)).ToString();

            IEnumerable<PostModel> result = null;

            var denormalizedData = await GetResourceData(expectedUri, HttpMethodEnum.POST, _vkPostData);

            var serializedData = Serialize(denormalizedData);
            if (serializedData.Any())
            {
                result = XMLGuidePostModeCreator.CreatePostModel(serializedData);
            }
            
#if DEBUG
            File.WriteAllText("C:\\vk.html", denormalizedData.ToString());
#endif

            return result;
        }

        public void AddSearchCriteria(string queston)
        {
        }

        public void SetPageNumber(int currentPage)
        {
            _pageOffsetMultiplier = currentPage;
            _vkPostData[PAGE_COUNT_KEY] = (DefaultPageCount + (_pageOffsetCount * _pageOffsetMultiplier)).ToString();
        }

        public void ResetPageCounter()
        {
            _pageOffsetMultiplier = 0;
        }
    }
}
