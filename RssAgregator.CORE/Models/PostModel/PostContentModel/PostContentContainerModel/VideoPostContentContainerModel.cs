
namespace RssAgregator.CORE.Models.PostModel.PostContentModel.PostContentContainerModel
{
    public class VideoPostContentContainerModel
    {
        public string Name { get; set; }
        public string ImagePreviewLink { get; set; }
        public string Data { get; set; }

        public VideoPostContentContainerModel(string name, string data, string imagepreviewLink = null)
        {
            Name = name;
            Data = data;
            ImagePreviewLink = imagepreviewLink;
        }
    }
}
