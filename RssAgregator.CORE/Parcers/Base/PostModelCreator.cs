using RssAgregator.CORE.Interfaces.Models.DOMObjectModel;
using RssAgregator.CORE.Models.Enums;
using RssAgregator.CORE.Models.PostModel;
using RssAgregator.CORE.Parcers.XMLGuidePostModelParcer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RssAgregator.CORE.Parcers.Base
{
    public class PostModelCreator : ResourceSerializer
    {
        protected XMLGuidePostModelCreator XMLGuidePostModeCreator { get; set; }

        public PostModelCreator(bool useCookie, XDocument xmlParceRules) 
            : base(useCookie)
        {
            XMLGuidePostModeCreator = new XMLGuidePostModelCreator(xmlParceRules);
        }

        public virtual async Task<IEnumerable<PostModel>> GetPostModelFromResourceData(Uri resourceUrl, HttpMethodEnum requestType, Dictionary<string, string> requestContent = null)
        {
            return CreatePostModel(await GetSerializedResourceData(resourceUrl, requestType, requestContent));
        }

        public virtual IEnumerable<PostModel> CreatePostModel(IEnumerable<IDOMElement> serializedData)
        {
            IEnumerable<PostModel> result = null;

            if (serializedData.Any())
            {
                result = XMLGuidePostModeCreator.CreatePostModel(serializedData);
            }

            return result;
        }
    }
}
