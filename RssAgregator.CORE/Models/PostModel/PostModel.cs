using RssAgregator.CORE.Interfaces.Models.PostModel.PostContentModel;
using System.Collections.Generic;

namespace RssAgregator.CORE.Models.PostModel
{
    public class PostModel
    {
        public string BaseURL { get; private set; }

        public string PostId { get; set; }

        private string _authorName;
        public string AuthorName 
        {
            get 
            {
                return _authorName;
            }
            set
            {

                _authorName = value;
                PostTags.Add(value);
            }
        }

        public string AuthorId { get; set; }

        public string AuthorLink { get; set; }

        public int PostLikes { get; set; }

        public string PostName { get; set; }

        public string PostLink { get; set; }

        public List<IPostContentModel> PostContent { get; private set; }

        public List<string> PostTags { get; private set; }


        public PostModel(string baseURL) 
        {
            PostContent = new List<IPostContentModel>();
            PostTags = new List<string>();
            BaseURL = baseURL;
        }
    }
}
