using RssAgregator.CORE.Interfaces.DOMObjectModel;
using RssAgregator.CORE.Interfaces.Parcers.XMLGuidePostModelParcer.XMLGuidePostModelParcers;
using RssAgregator.CORE.Models.Enums;
using RssAgregator.CORE.Models.PostModel;
using RssAgregator.CORE.Models.PostModel.PostContentModel;
using RssAgregator.CORE.Models.PostModel.PostContentModel.PostContentContainerModel;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace RssAgregator.CORE.Parcers.XMLGuidePostModelParcer.XMLGuidePostModelParcers
{
    public class AudioContentXMLParcer : BaseXMLGuideParcer, IXMLGuidePostModelParcer
    {
        protected const string SONG_LINK_POSTFIX_NAME = "SongLink";
        protected const string SONG_AUTHOR_POSTFIX_NAME = "SongAuthor";
        protected const string SONG_NAME_POSTFIX_NAME = "SongName";

        private AudioContentXMLParcer()
        {
        }

        public override object ProcessDOMNode(XElement xmlParceRule, IDOMElement expectedDOMElement, PostModel postModel)
        {
            object result = null;

            var searchCritereaNode = xmlParceRule.Elements().FirstOrDefault(el => el.Name.ToString().ToLower() == SEARCH_CRITEREA_NODE_NAME.ToLower());

            var expectedNode = SearchForNode(searchCritereaNode.Elements(), expectedDOMElement, postModel);
            if (expectedNode != null)
            {
                var songsData = new List<AudioPostContentContainerModel>();

                foreach (var child in expectedNode.Childs)
                {
                    var songAuthor = string.Empty;
                    var songName = string.Empty;

                    var searchCritereaSongLinkNode = xmlParceRule.Elements().FirstOrDefault(el => el.Name.ToString().ToLower() == (SUB_SEARCH_CRITEREA_NODE_NAME + SONG_LINK_POSTFIX_NAME).ToLower());
                    var songLinkNode = SearchForNode(searchCritereaSongLinkNode.Elements(), child, postModel);
                    if (songLinkNode != null)
                    {
                        var songLink = (string)ProcessGetCriterea(xmlParceRule, songLinkNode, postModel, SONG_LINK_POSTFIX_NAME);
                        if (!string.IsNullOrEmpty(songLink))
                        {
                            var searchCritereaSongAuthorNode = xmlParceRule.Elements().FirstOrDefault(el => el.Name.ToString().ToLower() == (SUB_SEARCH_CRITEREA_NODE_NAME + SONG_AUTHOR_POSTFIX_NAME).ToLower());
                            if (searchCritereaSongAuthorNode != null)
                            {
                                var songAuthorNode = SearchForNode(searchCritereaSongAuthorNode.Elements(), child, postModel);
                                songAuthor = (string)ProcessGetCriterea(xmlParceRule, songAuthorNode, postModel, SONG_AUTHOR_POSTFIX_NAME);
                            }


                            var searchCritereaSongNameNode = xmlParceRule.Elements().FirstOrDefault(el => el.Name.ToString().ToLower() == (SUB_SEARCH_CRITEREA_NODE_NAME + SONG_NAME_POSTFIX_NAME).ToLower());
                            if (searchCritereaSongNameNode != null)
                            {
                                var songNameNode = SearchForNode(searchCritereaSongNameNode.Elements(), child, postModel);
                                songName = (string)ProcessGetCriterea(xmlParceRule, songNameNode, postModel, SONG_NAME_POSTFIX_NAME);
                            }

                            songsData.Add(new AudioPostContentContainerModel(songAuthor, songName, songLink));
                        }
                    }
                    
                }


                result = songsData.Any() ? PostContentModelfactory.GetInitializedFactory(PostContentTypeEnum.Audio, postModel.PostContent.Count, songsData.ToArray()) : null;
            }

            return result;
        }
    }
}
