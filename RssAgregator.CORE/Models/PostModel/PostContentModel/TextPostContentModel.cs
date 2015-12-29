using RssAgregator.CORE.Interfaces.Models.PostModel.PostContentModel;
using RssAgregator.CORE.Models.Enums;
using System.Collections.Generic;

namespace RssAgregator.CORE.Models.PostModel.PostContentModel
{
    public class TextPostContentModel : BasePostContentModel<string>, IPostContentModel
    {
        public override PostContentTypeEnum PostContentType { get { return PostContentTypeEnum.Text; } }

        public override IEnumerable<string> PostContent
        {
            get { return PostSpecificContent; }
        }

        public TextPostContentModel(int order, params string[] content) :
            base(order, content)
        {
        }
    }
}
