using RssAgregator.CORE.Interfaces.Parcers.XMLGuidePostModelParcer.XMLGuidePostModelParcers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RssAgregator.CORE.Interfaces.Models.DOMObjectModel;
using RssAgregator.CORE.Models.PostModel;
using System.Xml.Linq;
using RssAgregator.DAL;
using RssAgregator.CORE.Interfaces.Models.PostModel.PostContentModel;
using RssAgregator.CORE.Models.PostModel.PostContentModel;
using RssAgregator.CORE.Models.Enums;
using RssAgregator.CORE.Models.PostModel.PostContentModel.PostContentContainerModel;

namespace RssAgregator.CORE.Parcers.XMLGuidePostModelParcer.XMLGuidePostModelParcers
{
    public class VideoContentXMLParcer : BaseXMLGuideParcer, IXMLGuidePostModelParcer
    {
        private const string VIDEO_NAME_POSTFIX_NAME = "VideoName";
        private const string VIDEO_LINK_POSTFIX_NAME = "VideoLink";

        private VideoContentXMLParcer()
        {

        }

        public override dynamic ProcessDOMNode(XElement xmlParceRule, IDOMElement expectedDOMElement, PostModel postModel)
        {
            IPostContentModel result = null;

            var searchCritereaNode = xmlParceRule.Elements().FirstOrDefault(el => el.Name.ToString().ToLower() == SEARCH_CRITEREA_NODE_NAME.ToLower());

            var expectedNode = SearchForNode(searchCritereaNode.Elements(), expectedDOMElement, postModel);
            if (expectedNode != null)
            {
                string videoName = string.Empty;
                string videoData = string.Empty;

                var searchCritereaVideoNameNode = xmlParceRule.Elements().FirstOrDefault(el => el.Name.ToString().ToLower() == (SUB_SEARCH_CRITEREA_NODE_NAME + VIDEO_NAME_POSTFIX_NAME).ToLower());
                var videoNameNode = SearchForNode(searchCritereaVideoNameNode.Elements(), expectedNode, postModel);
                if (videoNameNode != null)
                {
                    videoName = ProcessGetCriterea(xmlParceRule, videoNameNode, postModel, VIDEO_NAME_POSTFIX_NAME);
                }

                var searchCritereaVideoLinkNode = xmlParceRule.Elements().FirstOrDefault(el => el.Name.ToString().ToLower() == (SUB_SEARCH_CRITEREA_NODE_NAME + VIDEO_LINK_POSTFIX_NAME).ToLower());
                if (searchCritereaVideoLinkNode != null)
                {
                    var videoLinkNode = SearchForNode(searchCritereaVideoLinkNode.Elements(), expectedNode, postModel);
                    if (videoNameNode != null)
                    {
                        videoData = ProcessGetCriterea(xmlParceRule, videoLinkNode, postModel, VIDEO_LINK_POSTFIX_NAME);
                    }
                }
                else
                {
                    var externalGetNode = xmlParceRule.Elements().FirstOrDefault(el => el.Name.ToString().ToLower() == EXTERNAL_GET_NODE_NAME.ToLower());
                    if (externalGetNode != null)
                    {
                        var videoDataTask = ProcessExternlaGetCriterea(externalGetNode.Elements(), expectedNode, postModel);
                        videoDataTask.Wait();

                        videoData = videoDataTask.Result;
                    }
                }

                if (!string.IsNullOrEmpty(videoData))
                {
                    if (!string.IsNullOrEmpty(videoName))
                    {
                        result = PostContentModelfactory.GetInitializedFactory(PostContentTypeEnum.Video, postModel.PostContent.Count, new VideoPostContentContainerModel(videoName, videoData));
                    }
                    else
                    {
                        Logger.LogException("VideoContentXMLParcer factory returned empty video name", LogTypeEnum.CORE);
                    }
                }
                else
                {
                    Logger.LogException("VideoContentXMLParcer factory returned empty video data", LogTypeEnum.CORE);
                }
            }

            
            return result;
        }
    }
}
