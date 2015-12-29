using RssAgregator.CORE.Interfaces.Models.PostModel.PostContentModel;
using RssAgregator.CORE.Models.Enums;
using RssAgregator.CORE.Models.PostModel.PostContentModel.PostContentContainerModel;
using System.Collections.Generic;
using System.Linq;

namespace RssAgregator.CORE.Models.PostModel.PostContentModel
{
    public class AudioPostContentModel : BasePostContentModel<AudioPostContentContainerModel>, IPostContentModel
    {
        public override PostContentTypeEnum PostContentType { get { return PostContentTypeEnum.Audio; } }

        public override IEnumerable<string> PostContent
        {
            get { return PostSpecificContent.Select(el => string.Format("{0} - {1} : {2}", el.Signer, el.Name, el.Link)); }
        }

        public AudioPostContentModel(int order, params AudioPostContentContainerModel[] content) :
            base(order, content)
        {
        }
    }
}
