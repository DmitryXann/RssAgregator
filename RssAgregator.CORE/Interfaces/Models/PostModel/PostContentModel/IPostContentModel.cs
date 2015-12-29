using RssAgregator.CORE.Models.Enums;
using System.Collections.Generic;

namespace RssAgregator.CORE.Interfaces.Models.PostModel.PostContentModel
{
    public interface IPostContentModel
    {
        PostContentTypeEnum PostContentType { get; }

        IEnumerable<string> PostContent { get; }

        bool HasSpecificContent { get; }

        int PostOrder { get; }
    }
}
