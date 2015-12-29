using RssAgregator.CORE.Interfaces.Models.PostModel.PostContentModel;
using RssAgregator.CORE.Models.Enums;
using System.Collections.Generic;

namespace RssAgregator.CORE.Models.PostModel.PostContentModel
{
    public class ImgPostContentModel : BasePostContentModel<string>, IPostContentModel
    {
        public override PostContentTypeEnum PostContentType { get { return PostContentTypeEnum.Img; } }

        public override IEnumerable<string> PostContent
        {
            get { return PostSpecificContent; }
        }

        public ImgPostContentModel(int order, params string[] content) :
            base(order, content)
        {
        }
    }
}
