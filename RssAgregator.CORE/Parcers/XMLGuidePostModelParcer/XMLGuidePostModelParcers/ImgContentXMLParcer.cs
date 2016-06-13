using RssAgregator.CORE.Interfaces.DOMObjectModel;
using RssAgregator.CORE.Interfaces.Models.PostModel.PostContentModel;
using RssAgregator.CORE.Interfaces.Parcers.XMLGuidePostModelParcer.XMLGuidePostModelParcers;
using RssAgregator.CORE.Models.Enums;
using RssAgregator.CORE.Models.PostModel;
using RssAgregator.CORE.Models.PostModel.PostContentModel;
using RssAgregator.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RssAgregator.CORE.Parcers.XMLGuidePostModelParcer.XMLGuidePostModelParcers
{
    public class ImgContentXMLParcer : BaseXMLGuideParcer, IXMLGuidePostModelParcer
    {
        protected const string IMG_EXT_NAME = "ImgExt";

        private ImgContentXMLParcer()
        { 
        }

        public override dynamic ProcessDOMNode(XElement xmlParceRule, IDOMElement expectedDOMElement, PostModel postModel)
        {
            IPostContentModel result = null;

            var searchCritereaNode = xmlParceRule.Elements().FirstOrDefault(el => el.Name.ToString().ToLower() == SEARCH_CRITEREA_NODE_NAME.ToLower());

            var expectedNode = SearchForNode(searchCritereaNode.Elements(), expectedDOMElement, postModel);
            if (expectedNode != null)
            {
                var imgUrls = new List<string>();

                Parallel.ForEach(expectedNode.Childs, child =>
                {
                    if (!child.BadTag)
                    {
                        var subSearchCritereaNode = xmlParceRule.Elements().FirstOrDefault(el => el.Name.ToString().ToLower() == SUB_SEARCH_CRITEREA_NODE_NAME.ToLower());

                        var expectedSubNode = SearchForNode(subSearchCritereaNode.Elements(), child, postModel);
                        if (expectedSubNode != null)
                        {
                            var imgUrl = string.Empty;

                            var getCritereaNode = xmlParceRule.Elements().FirstOrDefault(el => el.Name.ToString().ToLower() == GET_CRITEREA_NODE_NAME.ToLower());
                            var getCriterea = getCritereaNode.Elements().First();

                            var getResult = Regex.Matches(expectedSubNode.GetContent(getCriterea.Name.ToString()), getCriterea.Value, RegexOptions.IgnoreCase);

                            var trimCritereaNode = xmlParceRule.Elements().FirstOrDefault(el => el.Name.ToString().ToLower() == TRIM_CRITEREA_NODE_NAME.ToLower());

                            foreach (Match resultPart in getResult)
                            {
                                if (trimCritereaNode != null)
                                {
                                    imgUrl += (trimCritereaNode.Attributes().Any(attr => attr.Name.ToString().ToLower() == STRING_TRIM_NAME.ToLower() && bool.Parse(attr.Value))
                                            ? resultPart.Value.Split(trimCritereaNode.Value.Split(new[] { ' ' }), StringSplitOptions.RemoveEmptyEntries)[0]
                                            : resultPart.Value.Trim(trimCritereaNode.Value.ToArray())).Trim();
                                }
                                else
                                {
                                    imgUrl += resultPart.Value.Trim();
                                }

                            }

                            var extNode = xmlParceRule.Elements().FirstOrDefault(el => el.Name.ToString().ToLower() == IMG_EXT_NAME.ToLower());
                            if (extNode != null && !string.IsNullOrEmpty(imgUrl))
                            {
                                imgUrl += string.Format(".{0}", extNode.Value.Trim());
                            }

                            if (string.IsNullOrEmpty(imgUrl))
                            {
                                Logger.LogException("ImgContentXMLParcer factory returned an empty link", LogTypeEnum.CORE);
                            }
                            else
                            {
                                var filename = Path.GetFileNameWithoutExtension(imgUrl);
                                if (string.IsNullOrEmpty(filename))
                                {
                                    Logger.LogException(string.Format("ImgContentXMLParcer factory returned a link without name: {0}", imgUrl), LogTypeEnum.CORE);
                                }
                                else
                                {
                                    var extension = Path.GetExtension(imgUrl);
                                    if (string.IsNullOrEmpty(extension))
                                    {
                                        Logger.LogException(string.Format("ImgContentXMLParcer factory returned a link without extension: {0}", imgUrl), LogTypeEnum.CORE);
                                    }
                                    else
                                    {
                                        imgUrls.Add(imgUrl);
                                    }
                                }
                            }
                        }
                    }
                });

                if (imgUrls.Any())
                {
                    result = PostContentModelfactory.GetInitializedFactory(PostContentTypeEnum.Img, postModel.PostContent.Count, imgUrls.ToArray());
                }
                else
                {
                    Logger.LogException("ImgContentXMLParcer factory returned no records", LogTypeEnum.CORE);
                }
            }

            return result;
        }
    }
}
