using RssAgregator.CORE.Interfaces.DOMObjectModel;
using RssAgregator.CORE.Interfaces.Models.PostModel.PostContentModel;
using RssAgregator.CORE.Interfaces.Parcers.XMLGuidePostModelParcer.XMLGuidePostModelParcers;
using RssAgregator.CORE.Models.Enums;
using RssAgregator.CORE.Models.PostModel;
using RssAgregator.CORE.Models.PostModel.PostContentModel;
using RssAgregator.CORE.Models.PostModel.PostContentModel.PostContentContainerModel;
using RssAgregator.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

        public override dynamic ProcessDOMNode(XElement xmlParceRule, IDOMElement expectedDOMElement, PostModel postModel)
        {
            IPostContentModel result = null;

            var searchCritereaNode = xmlParceRule.Elements().FirstOrDefault(el => el.Name.ToString().ToLower() == SEARCH_CRITEREA_NODE_NAME.ToLower());

            var expectedNode = SearchForNode(searchCritereaNode.Elements(), expectedDOMElement, postModel);
            if (expectedNode != null)
            {
                var songsData = new List<AudioPostContentContainerModel>();

                Parallel.ForEach(expectedNode.Childs, child => {
                    var songAuthor = string.Empty;
                    var songName = string.Empty;

                    var searchCritereaSongLinkNode = xmlParceRule.Elements().FirstOrDefault(el => el.Name.ToString().ToLower() == (SUB_SEARCH_CRITEREA_NODE_NAME + SONG_LINK_POSTFIX_NAME).ToLower());
                    var songLinkNode = SearchForNode(searchCritereaSongLinkNode.Elements(), child, postModel);
                    if (songLinkNode != null)
                    {
                        var songLink = ProcessGetCriterea(xmlParceRule, songLinkNode, postModel, SONG_LINK_POSTFIX_NAME);
                        if (!string.IsNullOrEmpty(songLink))
                        {
                            var searchCritereaSongAuthorNode = xmlParceRule.Elements().FirstOrDefault(el => el.Name.ToString().ToLower() == (SUB_SEARCH_CRITEREA_NODE_NAME + SONG_AUTHOR_POSTFIX_NAME).ToLower());
                            if (searchCritereaSongAuthorNode != null)
                            {
                                var songAuthorNode = SearchForNode(searchCritereaSongAuthorNode.Elements(), child, postModel);
                                songAuthor = ProcessGetCriterea(xmlParceRule, songAuthorNode, postModel, SONG_AUTHOR_POSTFIX_NAME);
                            }


                            var searchCritereaSongNameNode = xmlParceRule.Elements().FirstOrDefault(el => el.Name.ToString().ToLower() == (SUB_SEARCH_CRITEREA_NODE_NAME + SONG_NAME_POSTFIX_NAME).ToLower());
                            if (searchCritereaSongNameNode != null)
                            {
                                var songNameNode = SearchForNode(searchCritereaSongNameNode.Elements(), child, postModel);
                                songName = ProcessGetCriterea(xmlParceRule, songNameNode, postModel, SONG_NAME_POSTFIX_NAME);
                            }

                            var filename = Path.GetFileName(songLink);
                            if (string.IsNullOrEmpty(filename))
                            {
                                Logger.LogException(string.Format("AudioContentXMLParcer factory returned a link without name: {0}", songLink), LogTypeEnum.CORE);
                            }
                            else
                            {
                                var extension = Path.GetExtension(songLink);
                                if (string.IsNullOrEmpty(extension))
                                {
                                    Logger.LogException(string.Format("AudioContentXMLParcer factory returned a link without extension: {0}", songLink), LogTypeEnum.CORE);
                                }
                                else
                                {
                                    songsData.Add(new AudioPostContentContainerModel(songAuthor, songName, songLink));
                                }
                            }
                        }
                        else
                        {
                            Logger.LogException("AudioContentXMLParcer factory returned an empty link", LogTypeEnum.CORE);
                        }
                    }
                });

                if (songsData.Any())
                {
                    result = PostContentModelfactory.GetInitializedFactory(PostContentTypeEnum.Audio, postModel.PostContent.Count, songsData.ToArray());
                }
                else
                {
                    Logger.LogException("AudioContentXMLParcer factory returned no records", LogTypeEnum.CORE);
                }
            }

            return result;
        }
    }
}
