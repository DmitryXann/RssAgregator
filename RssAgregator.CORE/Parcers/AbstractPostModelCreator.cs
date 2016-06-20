using RssAgregator.CORE.Interfaces.DOMObjectModel;
using RssAgregator.CORE.Models.Enums;
using RssAgregator.CORE.Models.PostModel;
using RssAgregator.CORE.Parcers.XMLGuidePostModelParcer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RssAgregator.CORE.Parcers
{
    public abstract class AbstractPostModelCreator : AbstractResourceSerializer
    {
        protected XMLGuidePostModelCreator XMLGuidePostModeCreator { get; set; }

        protected AbstractPostModelCreator(bool useCookie, XDocument xmlParceRules) 
            : base(useCookie)
        {
            XMLGuidePostModeCreator = new XMLGuidePostModelCreator(xmlParceRules);
        }

        protected virtual async Task<IEnumerable<PostModel>> GetPostModelFromResourceData(Uri resourceUrl, HttpMethodEnum requestType, Dictionary<string, string> requestContent = null)
        {
            return CreatePostModel(Serialize(await GetResourceData(resourceUrl, requestType, requestContent)));
        }

        protected virtual IEnumerable<PostModel> CreatePostModel(IEnumerable<IDOMElement> serializedData)
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
