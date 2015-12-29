using System.Collections.Generic;

namespace RssAgregator.Models.Services
{
    public class NewsModel
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string AuthorId { get; set; }
        public string AuthorLink { get; set; }
        public int PostLikes { get; set; }
        public string PostName { get; set; }
        public string PostLink { get; set; }
        public string PostContent { get; set; }
        public IEnumerable<string> PostTags { get; set; }
        public bool External { get; set; }
        public bool AdultContent { get; set; }
        public string DataSource { get; set; }
    }
}
