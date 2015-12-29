using RssAgregator.CORE.Helpers;
using RssAgregator.CORE.Interfaces.Parcers;
using RssAgregator.CORE.Models.Enums;
using RssAgregator.CORE.Models.PostModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using RssAgregator.CORE.Models.PostModel.PostContentModel;
using System.Xml.Linq;

namespace RssAgregator.CORE.Parcers
{
    public class PikabuResourceParcer : ResourceSerializer, IResourceParcer
    {
        private const int DEFAULT_PAGE_COUNT = 2;
        private const string PAGE_COUNT_KEY = "page";
        private const string ACCEPT_ENCODING_HEADER_NAME = "Accept-Encoding";
        private const string ACCEPT_ENCODING_HEADER_PARAMS = "gzip, deflate, sdch";
        private const string SESSION_ID_TOKEN = "X-Csrf-Token";

        private Dictionary<string, string> _pikabuGetData;

        private int _pageOffsetCount;
        private int _pageOffsetMultiplier;

        private bool _sessionIsActive;

        public int DefaultPageCount
        {
            get { return DEFAULT_PAGE_COUNT; }
        }

        private PikabuResourceParcer(XDocument xmlGuide) 
            : base(true, xmlGuide)
        {
            _pageOffsetCount = 10;
            _pageOffsetMultiplier = 0;

            _pikabuGetData = new Dictionary<string, string>
                    {
                        {"twitmode", "1"},
                        {ACCEPT_ENCODING_HEADER_NAME, ACCEPT_ENCODING_HEADER_PARAMS}
                    };
        }

        public async Task<IEnumerable<PostModel>> GetContent(Uri expectedUri)
        {
            var result = new List<PostModel>();

            if (!_sessionIsActive)
            {
                _pageOffsetMultiplier = 0;

                var firstTimeEnterParams = new Dictionary<string, string> { { ACCEPT_ENCODING_HEADER_NAME, ACCEPT_ENCODING_HEADER_PARAMS } };
                var denormalizedFirstEnterData = await GetResourceData(expectedUri, HttpMethodEnum.GET, new FormUrlEncodedContent(firstTimeEnterParams));

                var sessionIdStartIndex = denormalizedFirstEnterData.FirstIndexOf("sessionID");
                if (sessionIdStartIndex > 0)
                {
                    var sessionIdData = denormalizedFirstEnterData.TakeWhile(sessionIdStartIndex, el => el != ',');
                    if (sessionIdData != null)
                    {
                        _pikabuGetData[SESSION_ID_TOKEN] = sessionIdData.ToString().Split(new[] { ':' })[1].Trim(new[] { '\"' });
                        _sessionIsActive = true;

                        result.AddRange(SerializeContent(denormalizedFirstEnterData, true));
                    }
                }
            }
            else
            {
                _pikabuGetData[PAGE_COUNT_KEY] = (DefaultPageCount + (_pageOffsetCount * _pageOffsetMultiplier++)).ToString();

                var denormalizedData = await GetResourceData(expectedUri, HttpMethodEnum.GET, new FormUrlEncodedContent(_pikabuGetData));

                result.AddRange(SerializeContent(denormalizedData, false));
            }
            
            return result;
        }

        public void ResetPageCounter()
        {
            _pageOffsetMultiplier = 0;
            _sessionIsActive = false;
        }

        private IEnumerable<PostModel> SerializeContent(StringBuilder content, bool firstEnter)
        {
#if DEBUG
            File.WriteAllText("C:\\pikabu.html", content.ToString());
#endif

            IEnumerable<PostModel> result = null;

            if (firstEnter)
            {
                var serializedData = Serialize(content);
                if (serializedData.Any())
                {
                    result = XMLGuidePostModeCreator.CreatePostModel(serializedData);
                }

                foreach (var serialized in Serialize(content))
                {
                    if (serialized.ElementType == DOMEleementTypeEnum.Tag)
                    {
                        var postsRootNode = serialized.GetFirstChildInSubThree(el => !string.IsNullOrEmpty(el.TagId) && el.TagId.ToLower() == "stories_container");
                        if (postsRootNode != null)
                        {
                            foreach (var child in postsRootNode.Childs)
                            {
                                if (child.ElementType == DOMEleementTypeEnum.Tag && !string.IsNullOrEmpty(child.TagName) && child.TagName.ToLower() == "table")
                                {
                                    var postModel = new PostModel();

                                    var postId = child.GetTagPropertyContent("data-story-id");;
                                    if (!string.IsNullOrEmpty(postId))
                                    {
                                        var postContentCounter = 0;

                                        postModel.PostId = postId;

                                        var expectedmetaDataContainerTagId = string.Format("story_table_{0}", postModel.PostId);
                                        var metaDataContainer = child.GetFirstChildInSubThree(el => !string.IsNullOrEmpty(el.TagId) && el.TagId.ToLower() == expectedmetaDataContainerTagId);

                                        var expectedPostLikesTagId = string.Format("num_digs{0}", postModel.PostId);
                                        var postLikesContainer = metaDataContainer.GetFirstChildInSubThree(el => !string.IsNullOrEmpty(el.TagId) && el.TagId.ToLower() == expectedPostLikesTagId);
                                        if (postLikesContainer != null)
                                        {
                                            int postLikes;
                                            var likesContent = postLikesContainer.GetFirstChild(el => el.IsContentElement);
                                            if (likesContent != null && !string.IsNullOrEmpty(likesContent.Content) && int.TryParse(likesContent.Content, out postLikes))
                                            {
                                                postModel.PostLikes = postLikes;
                                            }
                                        }

                                        var expectedNameConteinertagId = string.Format("num_dig3{0}", postModel.PostId);
                                        var postNameConteiner = metaDataContainer.GetFirstChildInSubThree(el => !string.IsNullOrEmpty(el.TagId) && el.TagId.ToLower() == expectedNameConteinertagId);
                                        if (postNameConteiner != null)
                                        {
                                            postModel.PostLink = postNameConteiner.GetTagPropertyContent("href");

                                            var postNameContent = postNameConteiner.GetFirstChild(el => el.IsContentElement);
                                            if (postNameContent != null)
                                            {
                                                postModel.PostName = postNameContent.Content;
                                            }
                                        }



                                        var nameTagsContainer = metaDataContainer.GetFirstChildInSubThree(el => !string.IsNullOrEmpty(el.TagClass) && el.TagClass.ToLower().Contains("b-story__header-additional"));

                                        var tagsContainer = nameTagsContainer.GetFirstChildInSubThree(el => !string.IsNullOrEmpty(el.TagClass) && el.TagClass.ToLower() == "story_tag_list");
                                        if (tagsContainer != null)
                                        {
                                            foreach (var tag in tagsContainer.Childs)
                                            {
                                                var contentTagContainer = tag.GetFirstChild(el => !string.IsNullOrEmpty(el.TagClass) && el.TagClass.ToLower().Contains("tag"));
                                                if (contentTagContainer != null)
                                                {
                                                    var contentTag = contentTagContainer.GetFirstChild(el => el.IsContentElement);
                                                    if (contentTag != null)
                                                    {
                                                        postModel.PostTags.Add(contentTag.Content);
                                                    }

                                                }
                                            }
                                        }

                                        var authorNode = nameTagsContainer.GetFirstChildInSubThree(el => !string.IsNullOrEmpty(el.TagName) && el.TagName.ToLower() == "a" && string.IsNullOrEmpty(el.TagClass) &&
                                                                                                            el.Childs.Count == 1 && el.Childs[0].IsContentElement);
                                        if (authorNode != null)
                                        {
                                            postModel.AuthorLink = authorNode.GetTagPropertyContent("href");
                                            postModel.AuthorName = authorNode.Childs[0].Content;

                                            postModel.AuthorId = postModel.AuthorName;
                                        }

                                        var expecedTextContentTagId = string.Format("tgpdiv{0}", postModel.PostId);
                                        var textContentTagContainer = child.GetFirstChildInSubThree(el => !string.IsNullOrEmpty(el.TagId) && el.TagId.ToLower() == expecedTextContentTagId);
                                        if (textContentTagContainer != null)
                                        {
                                            var textBlock = textContentTagContainer.GetFirstChildInSubThree(el => !string.IsNullOrEmpty(el.TagClass) && el.TagClass.ToLower().Contains("b-story-block_type_text"));
                                            if (textBlock != null)
                                            {
                                                var textContentBlock = textBlock.GetFirstChildInSubThree(el => !string.IsNullOrEmpty(el.TagClass) && el.TagClass.ToLower() == "b-story-block__content");
                                                foreach (var textContent in textContentBlock.Childs)
                                                {
                                                    var elementToAdd = string.Empty;

                                                    if (textContent.IsContentElement)
                                                    {
                                                        elementToAdd = textContent.Content;
                                                    }
                                                    else if (!string.IsNullOrEmpty(textContent.TagName) && textContent.TagName.ToLower() == "a")
                                                    {
                                                        elementToAdd = textContent.GetTagPropertyContent("href");
                                                    }
                                                    else
                                                    {
                                                        var contentElement = textContent.GetFirstChildInSubThree(el => el.IsContentElement);
                                                        if (contentElement != null)
                                                        {
                                                            elementToAdd = contentElement.Content;
                                                        }
                                                    }

                                                    postModel.PostContent.Add(PostContentModelfactory.GetInitializedFactory(PostContentTypeEnum.Text, postContentCounter++, elementToAdd));
                                                }
                                            }

                                            var imageBlock = textContentTagContainer.GetFirstChildInSubThree(el => !string.IsNullOrEmpty(el.TagClass) && el.TagClass.ToLower().Contains("b-story-block_type_image"));
                                            if (imageBlock != null)
                                            {
                                                foreach (var textContent in imageBlock.Childs)
                                                {
                                                    var elementToAdd = string.Empty;

                                                    if (textContent.IsContentElement)
                                                    {
                                                        elementToAdd = textContent.Content;
                                                    }
                                                    else if (!string.IsNullOrEmpty(textContent.TagName) && textContent.TagName.ToLower() == "a")
                                                    {
                                                        elementToAdd = textContent.GetTagPropertyContent("href");
                                                    }
                                                    else
                                                    {
                                                        var contentElement = textContent.GetFirstChildInSubThree(el => el.IsContentElement);
                                                        if (contentElement != null)
                                                        {
                                                            elementToAdd = contentElement.Content;
                                                        }
                                                    }

                                                    postModel.PostContent.Add(PostContentModelfactory.GetInitializedFactory(PostContentTypeEnum.Text, postContentCounter++, elementToAdd));
                                                }
                                            }
                                            
                                        }

                                        var expecedImgContentTagId = string.Format("picdiv{0}", postModel.PostId);
                                        var imgContentTagContainer = child.GetFirstChildInSubThree(el => !string.IsNullOrEmpty(el.TagId) && el.TagId.ToLower() == expecedTextContentTagId);
                                        if (imgContentTagContainer != null)
                                        {
                                            //postModel.PostContent.Add(PostContentModelfactory.GetInitializedFactory(PostContentTypeEnum.Img, postContentCounter++, resultImageLink));
                                        }

                                        var expecedVideoContentTagId = string.Format("videodiv{0}", postModel.PostId);
                                        var videoContentTagContainer = child.GetFirstChildInSubThree(el => !string.IsNullOrEmpty(el.TagId) && el.TagId.ToLower() == expecedTextContentTagId);
                                        if (videoContentTagContainer != null)
                                        {
                                            //postModel.PostContent.Add(PostContentModelfactory.GetInitializedFactory(PostContentTypeEnum.Video, postContentCounter++, resultImageLink));
                                        }

                                        
                                        //result.Add(postModel);
                                    }
                                }
                            }
                        }
                    }
                    
                }
            }
            else
            { 
            
            }

            return result;
        }

    }
}
