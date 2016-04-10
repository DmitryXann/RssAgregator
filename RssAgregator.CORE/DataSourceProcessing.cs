using RssAgregator.CORE.Models.Enums;
using RssAgregator.CORE.Models.PostModel.PostContentModel;
using RssAgregator.CORE.Models.PostModel.PostContentModel.PostContentContainerModel;
using RssAgregator.CORE.Parcers;
using RssAgregator.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RssAgregator.CORE
{
    public class DataSourceProcessing
    {
        private const string CONTENT_VALUE_PLACEHOLDER = "{ContainerValue}";
        private const string CONTENT_MEDIA_AUTHOR_PLACEHOLDER = "{MediaAuthor}";
        private const string CONTENT_MEDIA_NAME_PLACEHOLDER = "{MediaName}";
        private const string CONTENT_MEDIA_PREVIEW_PLACEHOLDER = "{MediaPreview}";

        private const string IMG_TEMPLATE_NAME = "ImgContainer";
        private const string AUDIO_TEMPLATE_NAME = "AudioContainer";
        private const string VIDEO_TEMPLATE_NAME = "VideoContainer";
        private const string TEXT_TEMPLATE_NAME = "TextContainer";
        private const string POST_TEMPLATE_NAME = "PostContainer";

        private IEnumerable<Template> _systemTemplates;

        public void ProcessAllActiveDataSources()
        {
            using (var db = new RssAggregatorModelContainer(true))
            {
                if (_systemTemplates == null)
                {
                    _systemTemplates = db.GetDBSet<Template>(el => el.Type == TemplateTypeEnum.System && el.Version == null).ToList();
                }

                foreach (var dataSource in db.GetDBSet<DataSources>(el => el.IsActive && el.IsNewsSource))
                {
                    var expectedFactory = ParcerProviderFactory.GetFactory(dataSource.Type);
                    var contetn = expectedFactory.GetContent(new Uri(dataSource.Uri));
                    contetn.Wait();

                    foreach (var post in contetn.Result)
                    {
                        if (post.PostContent.Any())
                        {
                            var postStringContetnt = new List<string>();

                            var newPost = new News()
                            {
                                PostId = post.PostId,
                                AuthorName = post.AuthorName,
                                AuthorId = post.AuthorId,
                                AuthorLink = post.AuthorLink,
                                PostLikes = post.PostLikes,
                                PostName = post.PostName,
                                PostLink = post.PostName,
                                PostTags = post.PostTags.Aggregate(string.Empty, (agg, el) => agg + el + ", "),
                                External = true,
                                IsActive = true,
                                AdultContent = false,
                                DataSource = dataSource,
                                User = db.GetSystemUser
                            };

                            foreach (var postContent in post.PostContent.OrderBy(el => el.PostOrder))
                            {
                                switch (postContent.PostContentType)
                                {
                                    case PostContentTypeEnum.Text:
                                        var textPostContainer = _systemTemplates.First(el => el.Name.ToLower() == TEXT_TEMPLATE_NAME.ToLower());
                                        foreach (var el in postContent.PostContent)
                                        {
                                            postStringContetnt.Add(textPostContainer.View.Replace(CONTENT_VALUE_PLACEHOLDER, el));
                                        }
                                        break;
                                    case PostContentTypeEnum.Img:
                                        var imgPostContainer = _systemTemplates.First(el => el.Name.ToLower() == IMG_TEMPLATE_NAME.ToLower());
                                        foreach (var el in postContent.PostContent)
                                        {
                                            postStringContetnt.Add(imgPostContainer.View.Replace(CONTENT_VALUE_PLACEHOLDER, el));
                                        }
                                        break;
                                    case PostContentTypeEnum.Audio:
                                        var audioPostContainer = _systemTemplates.First(el => el.Name.ToLower() == AUDIO_TEMPLATE_NAME.ToLower());
                                        foreach (var el in ((BasePostContentModel<AudioPostContentContainerModel>)postContent).PostSpecificContent)
                                        {
                                            postStringContetnt.Add(audioPostContainer.View.Replace(CONTENT_VALUE_PLACEHOLDER, el.Link)
                                                                                           .Replace(CONTENT_MEDIA_AUTHOR_PLACEHOLDER, el.Artist)
                                                                                           .Replace(CONTENT_MEDIA_NAME_PLACEHOLDER, el.Name));
                                        }
                                        break;
                                    case PostContentTypeEnum.Video:
                                        var videoPostContainer = _systemTemplates.First(el => el.Name.ToLower() == VIDEO_TEMPLATE_NAME.ToLower());
                                        foreach (var el in ((BasePostContentModel<VideoPostContentContainerModel>)postContent).PostSpecificContent)
                                        {
                                            postStringContetnt.Add(videoPostContainer.View.Replace(CONTENT_VALUE_PLACEHOLDER, el.VideoLink)
                                                                                           .Replace(CONTENT_MEDIA_AUTHOR_PLACEHOLDER, el.Name)
                                                                                           .Replace(CONTENT_MEDIA_PREVIEW_PLACEHOLDER, el.ImagePreviewLink));
                                        }
                                        break;
                                }
                            }

                            var postContainer = _systemTemplates.First(el => el.Name.ToLower() == POST_TEMPLATE_NAME.ToLower());

                            newPost.PostContent = postContainer.View.Replace(CONTENT_VALUE_PLACEHOLDER, string.Format("{0}\n", postStringContetnt.Aggregate(string.Empty, (agg, el) => agg + '\n' + el)));

                            db.AddEntity(newPost);
                        }
                    }
                }
            }
        }
    }
}
