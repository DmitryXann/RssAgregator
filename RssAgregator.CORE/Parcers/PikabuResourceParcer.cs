using RssAgregator.CORE.Helpers;
using RssAgregator.CORE.Interfaces.Parcers;
using RssAgregator.CORE.Models.Enums;
using RssAgregator.CORE.Models.PostModel;
using RssAgregator.CORE.Parcers.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RssAgregator.CORE.Parcers
{
    public class PikabuResourceParcer : AbstractPostModelCreator, IResourceParcer
    {
        private const int DEFAULT_PAGE_COUNT = 2;
        private const string PAGE_COUNT_KEY = "page";
        private const string ACCEPT_ENCODING_HEADER_NAME = "Accept-Encoding";
        private const string ACCEPT_ENCODING_HEADER_PARAMS = "gzip, deflate, sdch";
        private const string SESSION_ID_TOKEN = "X-Csrf-Token";

        private Dictionary<string, string> _pikabuGetData;

        private int _pageOffsetCount;
        private int _pageOffsetMultiplier;
        private bool _pageAlredySetted;

        private bool _sessionIsActive;

        public int DefaultPageCount
        {
            get { return DEFAULT_PAGE_COUNT; }
        }

        private PikabuResourceParcer(XDocument xmlGuide) 
            : base(true, xmlGuide)
        {
            _pageOffsetCount = 10;
            _pageOffsetMultiplier = 0;

            _pikabuGetData = new Dictionary<string, string>
                    {
                        {"twitmode", "1"},
                        {ACCEPT_ENCODING_HEADER_NAME, ACCEPT_ENCODING_HEADER_PARAMS}
                    };
        }

        public async Task<IEnumerable<PostModel>> GetContent(Uri expectedUri)
        {
            var result = new List<PostModel>();

            if (!_sessionIsActive)
            {
                _pageOffsetMultiplier = 0;

                var firstTimeEnterParams = new Dictionary<string, string> { { ACCEPT_ENCODING_HEADER_NAME, ACCEPT_ENCODING_HEADER_PARAMS } };
                var denormalizedFirstEnterData = await GetResourceData(expectedUri, HttpMethodEnum.GET, firstTimeEnterParams);

                var sessionIdStartIndex = denormalizedFirstEnterData.FirstIndexOf("sessionID");
                if (sessionIdStartIndex > 0)
                {
                    var sessionIdData = denormalizedFirstEnterData.TakeWhile(sessionIdStartIndex, el => el != ',');
                    if (sessionIdData != null)
                    {
                        _pikabuGetData[SESSION_ID_TOKEN] = sessionIdData.ToString().Split(new[] { ':' })[1].Trim(new[] { '\"' });
                        _sessionIsActive = true;

                        result.AddRange(SerializeContent(denormalizedFirstEnterData, true));
                    }
                }
            }
            else
            {
                if (!_pageAlredySetted)
                {
                    _pikabuGetData[PAGE_COUNT_KEY] = (DefaultPageCount + (_pageOffsetCount * _pageOffsetMultiplier++)).ToString();
                }
                
                var denormalizedData = await GetResourceData(expectedUri, HttpMethodEnum.GET, _pikabuGetData);

                result.AddRange(SerializeContent(denormalizedData, false));
            }
            
            return result;
        }

        public void AddSearchCriteria(string queston)
        {
        }

        public void SetPageNumber(int currentPage)
        {
            _pageOffsetMultiplier = currentPage;
            _pikabuGetData[PAGE_COUNT_KEY] = (DefaultPageCount + (_pageOffsetCount * _pageOffsetMultiplier)).ToString();
            _pageAlredySetted = true;
        }

        public void ResetPageCounter()
        {
            _pageOffsetMultiplier = 0;
            _sessionIsActive = false;
        }

        private IEnumerable<PostModel> SerializeContent(StringBuilder content, bool firstEnter)
        {
#if DEBUG
            File.WriteAllText("C:\\pikabu.html", content.ToString());
#endif

            IEnumerable<PostModel> result = null;

            if (firstEnter)
            {
                var serializedData = Serialize(content);
                if (serializedData.Any())
                {
                    result = XMLGuidePostModeCreator.CreatePostModel(serializedData);
                }
            }
            else
            { 
            
            }

            return result;
        }

    }
}
