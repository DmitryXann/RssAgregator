using RssAgregator.CORE.Interfaces.Models.PostModel.PostContentModel;
using RssAgregator.CORE.Models.Enums;
using RssAgregator.CORE.Models.PostModel.PostContentModel.PostContentContainerModel;
using System.Collections.Generic;
using System.Linq;

namespace RssAgregator.CORE.Models.PostModel.PostContentModel
{
    public class VideoPostContentModel : BasePostContentModel<VideoPostContentContainerModel>, IPostContentModel
    {
        public override PostContentTypeEnum PostContentType { get { return PostContentTypeEnum.Video; } }

        public override IEnumerable<string> PostContent
        {
            get { return PostSpecificContent.Select(el => string.Format("{0} : {1} : {2}", el.Name, el.ImagePreviewLink, el.Data)); }
        }

        public VideoPostContentModel(int order, params VideoPostContentContainerModel[] content) :
            base(order, content)
        {
        }
    }
}
