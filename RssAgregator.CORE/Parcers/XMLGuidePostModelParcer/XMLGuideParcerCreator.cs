﻿using RssAgregator.CORE.Interfaces.DOMObjectModel;
using RssAgregator.CORE.Interfaces.Models.PostModel.PostContentModel;
using RssAgregator.CORE.Interfaces.Parcers.XMLGuidePostModelParcer.XMLGuidePostModelParcers;
using RssAgregator.CORE.Models.Enums;
using RssAgregator.CORE.Models.PostModel;
using RssAgregator.CORE.Models.PostModel.PostContentModel;
using RssAgregator.CORE.Parcers.XMLGuidePostModelParcer.XMLGuidePostModelParcers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var result = new List<PostModel>();

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
                    foreach (var domElement in searchedNodes)
                    {
                        if (!domElement.BadTag && domElement.ElementType == DOMEleementTypeEnum.Tag)
                        {
                            var post = new PostModel();

                            IEnumerable<XElement> elementsInRootSorted;
                            var elementsInRoot = parcerRuleRootNode.Elements();

                            var expectedMacro = Regex.Matches(parcerRuleRootNode.Value, "{([\\w]*)}", RegexOptions.IgnoreCase);
                            if (expectedMacro.Count > 0)
                            {
                                var sortedNodes = new List<XElement>();
                                foreach (Match macro in expectedMacro)
                                {
                                    var expectedNode = elementsInRoot.FirstOrDefault(el => el.Name.ToString().ToLower() == macro.Value.ToLower().Trim(new[] { '{', '}' }));
                                    if (expectedNode != null)
                                    {
                                        sortedNodes.Add(expectedNode);
                                    }

                                    var uniqueNodes = elementsInRoot.Except(sortedNodes);
                                    sortedNodes.AddRange(uniqueNodes);

                                    elementsInRootSorted = sortedNodes;
                                }
                            }
                            else
                            {
                                elementsInRootSorted = elementsInRoot;
                            }

                            foreach (var ruleNode in parcerRuleRootNode.Elements())
                            {
                                XMLGuidePostModelParcersEnum expectedparcerType;
                                if (Enum.TryParse(ruleNode.Name.ToString(), true, out expectedparcerType))
                                {
                                    var expectedFactory = XMLPostModelParcerFactory.GetFactory(expectedparcerType);
                                    switch (expectedparcerType)
                                    {
                                        case XMLGuidePostModelParcersEnum.AuthorId:
                                            post.AuthorId = (string)expectedFactory.ProcessDOMNode(ruleNode, domElement, post);
                                            break;
                                        case XMLGuidePostModelParcersEnum.PostId:
                                            post.PostId = (string)expectedFactory.ProcessDOMNode(ruleNode, domElement, post);
                                            break;
                                        case XMLGuidePostModelParcersEnum.PostLikes:
                                            int postLikes;
                                            if (int.TryParse((string)expectedFactory.ProcessDOMNode(ruleNode, domElement, post), out postLikes))
                                            {
                                                post.PostLikes = postLikes;
                                            }
                                            break;
                                        case XMLGuidePostModelParcersEnum.AuthorName:
                                            post.AuthorName = (string)expectedFactory.ProcessDOMNode(ruleNode, domElement, post);
                                            break;
                                        case XMLGuidePostModelParcersEnum.AuthorLink:
                                            post.AuthorLink = (string)expectedFactory.ProcessDOMNode(ruleNode, domElement, post);
                                            break;
                                        case XMLGuidePostModelParcersEnum.PostName:
                                            post.PostName = (string)expectedFactory.ProcessDOMNode(ruleNode, domElement, post);
                                            break;
                                        case XMLGuidePostModelParcersEnum.PostLink:
                                            post.PostLink = (string)expectedFactory.ProcessDOMNode(ruleNode, domElement, post);
                                            break;
                                        case XMLGuidePostModelParcersEnum.TextContent:
                                        case XMLGuidePostModelParcersEnum.ImgContent:
                                        case XMLGuidePostModelParcersEnum.AudioContent:
                                            var content = (IPostContentModel)expectedFactory.ProcessDOMNode(ruleNode, domElement, post);
                                            if (content != null)
                                            {
                                                post.PostContent.Add(content);
                                            }
                                            break;
                                    }
                                }
                            }

                            result.Add(post);
                        }
                    }
                }
            }

            return result;
        }

        private IEnumerable<IDOMElement> SearchForNodes(IEnumerable<IDOMElement> serializedDomModel, Func<IDOMElement, bool> searchCriterea)
        {
            var result = new List<IDOMElement>();

            foreach (var el in serializedDomModel)
            {
                var firstChildInSubThree = el.GetFirstChildInSubThree(searchCriterea);
                if (firstChildInSubThree != null)
                {
                    result.Add(firstChildInSubThree);
                }
            }

            return result;
        }

    }
}
