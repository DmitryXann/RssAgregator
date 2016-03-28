using System.IO;

namespace RssAgregator.CORE.Models.PostModel.PostContentModel.PostContentContainerModel
{
    public class AudioPostContentContainerModel
    {
        public string Artist { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }

        public AudioPostContentContainerModel(string artist, string name, string link)
        {
            Artist = artist;
            Name = string.IsNullOrEmpty(name) ? Path.GetFileName(link) : name;
            Link = link;
        }
    }
}
