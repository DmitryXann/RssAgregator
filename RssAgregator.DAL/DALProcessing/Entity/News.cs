using RssAgregator.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssAgregator.DAL
{
    public partial class News
    {
        public NewsModel GetModel()
        {
            return new NewsModel 
            {
                Id = Id,
                AuthorName = AuthorName,
                AuthorId = AuthorId,
                AuthorLink = AuthorLink,
                PostLikes = PostLikes,
                PostName = PostName,
                PostLink = PostLink,
                PostContent = PostContent,
                PostTags = PostTags.Split(new [] { ',' }, StringSplitOptions.RemoveEmptyEntries),
                External = External,
                AdultContent = AdultContent,
                //DataSource = DataSource.BaseUri
            };
        }
    }
}
