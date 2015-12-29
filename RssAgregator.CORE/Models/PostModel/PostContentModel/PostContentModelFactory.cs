using RssAgregator.CORE.Interfaces.Models.PostModel.PostContentModel;
using RssAgregator.CORE.Models.Enums;
using RssAgregator.CORE.Models.PostModel.PostContentModel.PostContentContainerModel;
using System.Linq;

namespace RssAgregator.CORE.Models.PostModel.PostContentModel
{
    public static class PostContentModelfactory
    {
        public static IPostContentModel GetInitializedFactory<T>(PostContentTypeEnum contentType, int order, params T[] content)
        {
            IPostContentModel resultFactory = null;

            switch (contentType)
            {
                case PostContentTypeEnum.Audio:
                    resultFactory = new AudioPostContentModel(order, content.Cast<AudioPostContentContainerModel>().ToArray());
                    break;
                case PostContentTypeEnum.Img:
                    resultFactory = new ImgPostContentModel(order, content.Cast<string>().ToArray());
                    break;
                case PostContentTypeEnum.Text:
                    resultFactory = new TextPostContentModel(order, content.Cast<string>().ToArray());
                    break;
                case PostContentTypeEnum.Video:
                    resultFactory = new VideoPostContentModel(order, content.Cast<VideoPostContentContainerModel>().ToArray());
                    break;

            }

            return resultFactory;
        }
    }
}
