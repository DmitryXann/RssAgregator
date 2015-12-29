using RssAgregator.CORE.Interfaces.Models.PostModel.PostContentModel;
using RssAgregator.CORE.Models.Enums;
using RssAgregator.CORE.Models.PostModel.PostContentModel.PostContentContainerModel;
using System.Collections.Generic;

namespace RssAgregator.CORE.Models.PostModel.PostContentModel
{
    public abstract class BasePostContentModel<T> : IPostContentModel
    {
        public abstract PostContentTypeEnum PostContentType { get; }

        public IEnumerable<T> PostSpecificContent { get; private set; }

        public abstract IEnumerable<string> PostContent { get; }

        public virtual bool HasSpecificContent 
        {
            get
            {
                return PostContentType == PostContentTypeEnum.Audio || PostContentType == PostContentTypeEnum.Video;
            }
        }

        public int PostOrder { get; private set;}

        public BasePostContentModel(int order, params T[] content)
        {
            PostOrder = order;
            PostSpecificContent = content;
        }
    }
}
