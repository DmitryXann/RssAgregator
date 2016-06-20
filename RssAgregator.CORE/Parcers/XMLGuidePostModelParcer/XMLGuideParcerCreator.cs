using RssAgregator.CORE.Interfaces.DOMObjectModel;
using RssAgregator.CORE.Models.Enums;
using RssAgregator.CORE.Models.PostModel;
using RssAgregator.CORE.Parcers.XMLGuidePostModelParcer.XMLGuidePostModelParcers;
using RssAgregator.DAL;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RssAgregator.CORE.Parcers.XMLGuidePostModelParcer
{
    public class XMLGuidePostModelCreator
    {
        private const string POST_ROOT_NODE_NAME = "PostRootNode";
        private const string USE_STRICT_EQUAL_CHECK_ATTRIBUTE_NAME = "strictEqual";

        private XDocument _xmlParceRules;

        public XMLGuidePostModelCreator(XDocument xmlParceRules)
        {
            _xmlParceRules = xmlParceRules;
        }

        public IEnumerable<PostModel> CreatePostModel(IEnumerable<IDOMElement> serializedDomModel)
        {
            var result = new ConcurrentBag<PostModel>();

            var parcerRuleRootNode = _xmlParceRules.Root;

            var parcerRulepostRoot = parcerRuleRootNode.Elements().FirstOrDefault(el => el.Name.ToString().ToLower() == POST_ROOT_NODE_NAME.ToLower());
            if (parcerRulepostRoot != null)
            {
                var searchCriterea = parcerRulepostRoot.Elements();
                var searchedNodes = SearchForNodes(serializedDomModel, el => searchCriterea.All(elem =>
                                                                            el.ElementMatch(elem.Name.ToString(), elem.Value, elem.Attributes().Any(attr =>
                                                                                                                                                attr.Name.ToString().ToLower() == USE_STRICT_EQUAL_CHECK_ATTRIBUTE_NAME.ToLower() && bool.Parse(attr.Value)))));
                if (searchedNodes.Any())
                {
                    //Parallel.ForEach(searchedNodes, domElement => 
                    foreach(var domElement in searchedNodes)//TODO: just for debug, remove
                    {
                        if (!domElement.BadTag && domElement.ElementType == DOMEleementTypeEnum.Tag)
                        {
                            var post = new PostModel();

                            IEnumerable<XElement> sortedRuleNodes;
                            var elementsInRoot = parcerRuleRootNode.Elements();

                            var expectedMacro = Regex.Matches(parcerRuleRootNode.Value, "{([\\w]*)}", RegexOptions.IgnoreCase);
                            if (expectedMacro.Count > 0)
                            {
                                var sortedNodes = new List<XElement>();
                                var nodeMacroCollection = new List<string>();

                                foreach (Match macro in expectedMacro)
                                {
                                    var expectedMacroValue = macro.Value.ToLower().Trim(new[] { '{', '}', ' ', '\n', '\t' });
                                    if (!nodeMacroCollection.Contains(expectedMacroValue))
                                    {
                                        nodeMacroCollection.Add(expectedMacroValue);
                                    }
                                }

                                foreach (var macro in nodeMacroCollection)
                                {
                                    var expectedNode = elementsInRoot.FirstOrDefault(el => el.Name.LocalName.ToLower() == macro);
                                    if (expectedNode != null)
                                    {
                                        sortedNodes.Add(expectedNode);
                                    }
                                }

                                sortedNodes.AddRange(elementsInRoot.Except(sortedNodes));
                                sortedRuleNodes = sortedNodes;
                            }
                            else
                            {
                                sortedRuleNodes = elementsInRoot;
                            }

                            foreach (var ruleNode in sortedRuleNodes)
                            {
                                XMLGuidePostModelParcersEnum expectedparcerType;
                                if (Enum.TryParse(ruleNode.Name.ToString(), true, out expectedparcerType))
                                {
                                    var expectedFactory = XMLPostModelParcerFactory.GetFactory(expectedparcerType);

                                    try
                                    {
                                        switch (expectedparcerType)
                                        {
                                            case XMLGuidePostModelParcersEnum.AuthorId:
                                                post.AuthorId = expectedFactory.ProcessDOMNode(ruleNode, domElement, post);
                                                break;
                                            case XMLGuidePostModelParcersEnum.PostId:
                                                post.PostId = expectedFactory.ProcessDOMNode(ruleNode, domElement, post);
                                                break;
                                            case XMLGuidePostModelParcersEnum.PostLikes:
                                                int postLikes;
                                                if (int.TryParse(expectedFactory.ProcessDOMNode(ruleNode, domElement, post), out postLikes))
                                                {
                                                    post.PostLikes = postLikes;
                                                }
                                                break;
                                            case XMLGuidePostModelParcersEnum.AuthorName:
                                                post.AuthorName = expectedFactory.ProcessDOMNode(ruleNode, domElement, post);
                                                break;
                                            case XMLGuidePostModelParcersEnum.AuthorLink:
                                                post.AuthorLink = expectedFactory.ProcessDOMNode(ruleNode, domElement, post);
                                                break;
                                            case XMLGuidePostModelParcersEnum.PostName:
                                                post.PostName = expectedFactory.ProcessDOMNode(ruleNode, domElement, post);
                                                break;
                                            case XMLGuidePostModelParcersEnum.PostLink:
                                                post.PostLink = expectedFactory.ProcessDOMNode(ruleNode, domElement, post);
                                                break;
                                            case XMLGuidePostModelParcersEnum.TextContent:
                                            case XMLGuidePostModelParcersEnum.ImgContent:
                                            case XMLGuidePostModelParcersEnum.AudioContent:
                                                var content = expectedFactory.ProcessDOMNode(ruleNode, domElement, post);
                                                if (content != null)
                                                {
                                                    post.PostContent.Add(content);
                                                }
                                                break;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Logger.LogException(ex, LogTypeEnum.CORE, string.Format("ProcessDOMNode {0} factory failed", expectedparcerType));
                                    }
                                }
                            }

                            result.Add(post);
                        }
                    }//);
                }
            }

            return result;
        }

        private IEnumerable<IDOMElement> SearchForNodes(IEnumerable<IDOMElement> serializedDomModel, Func<IDOMElement, bool> searchCriterea)
        {
            var result = new ConcurrentBag<IDOMElement>();

            Parallel.ForEach(serializedDomModel, el =>
            {
                var firstChildInSubThree = el.GetFirstChildInSubThree(searchCriterea);
                if (firstChildInSubThree != null)
                {
                    result.Add(firstChildInSubThree);
                }
            });

            return result;
        }

    }
}
