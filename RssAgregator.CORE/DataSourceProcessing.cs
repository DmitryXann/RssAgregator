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

        private const string IMG_GALLERY_TEMPLATE_NAME = "Image-Gallery-Container";
        private const string IMG_TEMPLATE_NAME = "Img-Container";

        private const string VIDEO_GALLERY_TEMPLATE_NAME = "Video-Gallery-Container";
        private const string VIDEO_TEMPLATE_NAME = "Video-Container";

        private const string AUDIO_TEMPLATE_NAME = "Audio-Container";
        private const string TEXT_TEMPLATE_NAME = "Text-Container";
       

        private IEnumerable<Template> _systemTemplates;

        public void ProcessAllActiveDataSources()
        {
            using (var db = new RssAggregatorModelContainer(true))
            {
                if (_systemTemplates == null)
                {
                    _systemTemplates = db.GetDBSet<Template>(el => el.Type == TemplateTypeEnum.System && el.Version == null).ToList();
                }

                var user = db.GetSystemUser;
                var systemDataSource = db.GetEntity<DataSources>(el => el.Type == DataSourceEnum.System);

                foreach (var dataSource in db.GetDBSet<DataSources>(el => el.IsActive && el.IsNewsSource))
                {
                    try
                    {
                        var expectedFactory = ParcerProviderFactory.GetFactory(dataSource.Type);
                        var contetn = expectedFactory.GetContent(new Uri(dataSource.Uri));
                        contetn.Wait();

                        foreach (var post in contetn.Result)
                        {
                            if (post.PostContent.Any())
                            {
                                try
                                {
                                    var postStringContetnt = new List<string>();

                                    var postId = db.GetPostTransliteratedName(db.GetTransliteration(post.PostId), user.Name);

                                    var newPost = new News()
                                    {
                                        PostId = postId,
                                        AuthorName = post.AuthorName,
                                        AuthorId = post.AuthorId,
                                        AuthorLink = string.Format("{0}/{1}", dataSource.BaseUri.TrimEnd(new[] { '/' }), post.AuthorLink.TrimStart(new[] { '/' })),
                                        PostLikes = post.PostLikes,
                                        PostName = post.PostName,
                                        PostLink = null,//postId,
                                        PostTags = post.PostTags.Aggregate(string.Empty, (agg, el) => agg + ", " + el).TrimStart(new[] { ',', ' ' }),
                                        External = true,
                                        IsActive = true,
                                        AdultContent = false,
                                        DataSource = dataSource,
                                        CreationDateTime = DateTime.UtcNow,
                                        ModificationDateTime = DateTime.UtcNow,
                                        User = user,
                                        Location = dataSource.Location
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
                                                if (postContent.PostContent.Any())
                                                {
                                                    var imgPostContainer = _systemTemplates.First(el => el.Name.ToLower() == IMG_TEMPLATE_NAME.ToLower());
                                                    var imgGalleryContainer = _systemTemplates.First(el => el.Name.ToLower() == IMG_GALLERY_TEMPLATE_NAME.ToLower());

                                                    var imgPostContainers = string.Empty;
                                                    foreach (var el in postContent.PostContent)
                                                    {
                                                        imgPostContainers += imgPostContainer.View.Replace(CONTENT_VALUE_PLACEHOLDER, el);
                                                    }

                                                    postStringContetnt.Add(imgGalleryContainer.View.Replace(CONTENT_VALUE_PLACEHOLDER, imgPostContainers));
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
                                                if (postContent.PostContent.Any())
                                                {
                                                    var videoPostContainer = _systemTemplates.First(el => el.Name.ToLower() == VIDEO_TEMPLATE_NAME.ToLower());
                                                    var videoGalleryContainer = _systemTemplates.First(el => el.Name.ToLower() == VIDEO_GALLERY_TEMPLATE_NAME.ToLower());

                                                    var videoPostContainers = string.Empty;
                                                    foreach (var el in ((BasePostContentModel<VideoPostContentContainerModel>)postContent).PostSpecificContent)
                                                    {
                                                        videoPostContainers += videoPostContainer.View.Replace(CONTENT_VALUE_PLACEHOLDER, el.Data)
                                                                                                   .Replace(CONTENT_MEDIA_AUTHOR_PLACEHOLDER, el.Name)
                                                                                                   .Replace(CONTENT_MEDIA_PREVIEW_PLACEHOLDER, el.ImagePreviewLink);
                                                    }

                                                    postStringContetnt.Add(videoGalleryContainer.View.Replace(CONTENT_VALUE_PLACEHOLDER, videoPostContainers));
                                                }
                                            break;
                                        }

                                        newPost.PostTags += string.Format(", {0}", db.GetTagName((TagTypeEnum)postContent.PostContentType, newPost.Location));
                                    }

                                    newPost.PostContent = postStringContetnt.Aggregate(string.Empty, (agg, el) => agg + '\n' + el);

                                    db.AddEntity(newPost);
                                }
                                catch (Exception ex)
                                {
                                    Logger.LogException(ex, LogTypeEnum.CORE, string.Format("DataSourceProcessing for {0} factory failed to process post: {1}", dataSource.Type, post.PostId));
                                }
                            }
                            else
                            {
                                Logger.LogException(string.Format("DataSourceProcessing for {0} factory has empty content for the post: {1}", dataSource.Type, post.PostId), LogTypeEnum.CORE);
                            }
                        }

                        if (dataSource.PostAmountPerIteration.HasValue)
                        {
                            if (contetn.Result.Count() != dataSource.PostAmountPerIteration.Value)
                            {
                                Logger.LogException(string.Format("DataSourceProcessing for {0} factory returned incorrect expected amount of post, expected {1}, got {2}", dataSource.Type, dataSource.PostAmountPerIteration.Value, contetn.Result.Count()), LogTypeEnum.CORE);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogException(ex, LogTypeEnum.CORE, string.Format("DataSourceProcessing for {0} factory failed", dataSource.Type));
                    }
                }
            }
        }
    }
}
