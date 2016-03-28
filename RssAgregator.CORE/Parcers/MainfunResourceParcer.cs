using RssAgregator.CORE.Interfaces.Parcers;
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
    public class MainfunResourceParcer : ResourceSerializer, IResourceParcer
    {
        private const int DEFAULT_PAGE_COUNT = 1;

        private Dictionary<string, string> _mainFunGetData;

        private int _pageOffsetCount;
        private int _pageOffsetMultiplier;

        public int DefaultPageCount
        {
            get { return DEFAULT_PAGE_COUNT; }
        }

        private MainfunResourceParcer(XDocument xmlGuide) 
            : base(false, xmlGuide)
        {
            _pageOffsetCount = 1;
            _pageOffsetMultiplier = 0;

            _mainFunGetData = new Dictionary<string, string>
                    {
                        {"Accept-Encoding", "gzip, deflate, sdch"}
                    };
        }

        public async Task<IEnumerable<PostModel>> GetContent(Uri expectedUri)
        {
            var expectedPage = DefaultPageCount + (_pageOffsetCount * _pageOffsetMultiplier++);

            IEnumerable<PostModel> result = null;

            var denormalizedData = await GetResourceData(new Uri(expectedUri.AbsoluteUri.TrimEnd(new [] {'/'}) + (expectedPage > 1 ? string.Format("index/page{0}", expectedPage) : "/")), HttpMethodEnum.GET, _mainFunGetData);

            var serializedData = Serialize(denormalizedData);
            if (serializedData.Any())
            {
                result = XMLGuidePostModeCreator.CreatePostModel(serializedData);
            }

#if DEBUG
            File.WriteAllText("C:\\mainfun.html", denormalizedData.ToString());
#endif

            return result;
        }

        public void AddSearchCriteria(string queston)
        {
        }

        public void SetPageNumber(int currentPage)
        {
            //_pageOffsetMultiplier = currentPage;
            //_pikabuGetData[PAGE_COUNT_KEY] = (DefaultPageCount + (_pageOffsetCount * _pageOffsetMultiplier)).ToString();
        }
        public void ResetPageCounter()
        {
            _pageOffsetMultiplier = 0;
        }
    }
}
