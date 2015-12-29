using System.IO;

namespace RssAgregator.CORE.Models.PostModel.PostContentModel.PostContentContainerModel
{
    public class AudioPostContentContainerModel
    {
        public string Signer { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }

        public AudioPostContentContainerModel(string signer, string name, string link)
        {
            Signer = signer;
            Name = string.IsNullOrEmpty(name) ? Path.GetFileName(link) : name;
            Link = link;
        }
    }
}
