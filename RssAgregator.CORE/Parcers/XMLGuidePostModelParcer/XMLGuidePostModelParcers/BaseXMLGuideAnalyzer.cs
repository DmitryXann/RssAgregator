﻿using RssAgregator.CORE.Interfaces.Models.DOMObjectModel;
using RssAgregator.CORE.Interfaces.Parcers.XMLGuidePostModelParcer.XMLGuidePostModelParcers;
using RssAgregator.CORE.Models.Enums;
using RssAgregator.CORE.Models.PostModel;
using RssAgregator.CORE.Parcers.Base;
using RssAgregator.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RssAgregator.CORE.Parcers.XMLGuidePostModelParcer.XMLGuidePostModelParcers
{
    public abstract class BaseXMLGuideParcer : IXMLGuidePostModelParcer
    {
        protected const string EXTERNAL_GET_NODE_NAME                   = "ExternalGet";
        protected const string EXTERNAL_GET_LINK_NODE_NAME              = "Link";
        protected const string EXTERNAL_GET_CONTENT_NODE_NAME           = "ExternalGetContent";

        protected const string SEARCH_CRITEREA_NODE_NAME                = "SearchCriterea";
        protected const string SUB_SEARCH_CRITEREA_NODE_NAME            = "SubSearchCriterea";

        protected const string GET_CRITEREA_NODE_NAME                   = "GetCriterea";
        protected const string TRIM_CRITEREA_NODE_NAME                  = "TrimCriterea";
        protected const string ADDITIONAL_PREFIX                        = "Additional";

        protected const string USE_STRICT_EQUAL_CHECK_ATTRIBUTE_NAME    = "strictEqual";

        protected const string STRING_TRIM_ATTRIBUTE_NAME               = "stringTrim";
        protected const string REG_EXP_TRIM_ATTRIBUTE_NAME              = "regExpTrim";
        protected const string GET_LAST_OCCURENCE_ATTRIBUTE_NAME        = "getLastOccurence";

        protected const string REMOVE_TRIM_ATTRIBUTE_NAME               = "removeTrim";

        protected const string USE_LAST_CHILD_SEARCH_ATTRIBUTE_NAME     = "useLastChildSearch";

        protected const string EXPECTED_AUTHOR_ID_MACRO_NAME            = "{AuthorId}";
        protected const string EXPECTED_POST_ID_MACRO_NAME              = "{PostId}";

        protected virtual IDOMElement SearchForNode(IEnumerable<XElement> searchCriterea, IDOMElement domElement, PostModel postModel, bool useFirstElSearch = true)
        {
            Func<IDOMElement, bool> searchCritereaFilter = el => 
                searchCriterea.All(elem => el.ElementMatch(elem.Name.ToString(), 
                                                            elem.Value.Replace(EXPECTED_AUTHOR_ID_MACRO_NAME, postModel.AuthorId).Replace(EXPECTED_POST_ID_MACRO_NAME, postModel.PostId),
                                                            elem.Attributes().Any(attr => attr.Name.ToString().ToLower() == USE_STRICT_EQUAL_CHECK_ATTRIBUTE_NAME.ToLower() && bool.Parse(attr.Value))));

            return useFirstElSearch ? domElement.GetFirstChildInSubThree(searchCritereaFilter) : domElement.GetLastChildInSubThree(searchCritereaFilter);
        }

        protected virtual IDOMElement SearchForNode(IEnumerable<XElement> searchCriterea, IEnumerable<IDOMElement> domElements, PostModel postModel, bool useFirstElSearch = true)
        {
            IDOMElement result = null;

            var domElementsCount = domElements.Count();
            var domElementsIndex = useFirstElSearch ? 0 : domElementsCount - 1;

            if (useFirstElSearch)
            {
                while (result == null && domElementsCount > domElementsIndex)
                {
                    result = SearchForNode(searchCriterea, domElements.ElementAt(domElementsIndex++), postModel, useFirstElSearch);
                }
            }
            else
            {
                while (result == null && domElementsIndex >= 0)
                {
                    result = SearchForNode(searchCriterea, domElements.ElementAt(domElementsIndex--), postModel, useFirstElSearch);
                }
            }

            return result;
        }

        public virtual dynamic ProcessDOMNode(XElement xmlParceRule, IDOMElement expectedDOMElement, PostModel postModel)
        {
            string result = null;

            var searchCritereaNode = xmlParceRule.Elements().FirstOrDefault(el => el.Name.ToString().ToLower() == SEARCH_CRITEREA_NODE_NAME.ToLower());

            var expectedNode = SearchForNode(searchCritereaNode.Elements(), expectedDOMElement, postModel);
            if (expectedNode != null)
            {
                result = ProcessGetCriterea(xmlParceRule, expectedNode, postModel);
            }

            if (string.IsNullOrEmpty(result))
            {
                Logger.LogException(string.Format("BaseXMLGuideParcer returned no result for {0} factory", GetType().Name), LogTypeEnum.CORE);
            }

            return result;
        }

        protected virtual string ProcessGetCriterea(XElement xmlParceRule, IDOMElement expectedNode, PostModel postModel, string postfix = null)
        {
            string result = null;

            var getCritereaNode = xmlParceRule.Elements().FirstOrDefault(el => el.Name.ToString().ToLower() == (string.IsNullOrEmpty(postfix) ? GET_CRITEREA_NODE_NAME : GET_CRITEREA_NODE_NAME + postfix).ToLower());
            var getCriterea = getCritereaNode.Elements().First();

            var getResult = string.Empty;
            switch (getCriterea.Name.ToString().ToLower())
            {
                case "values":
                case "childs":
                case "element":
                case "content":
                    getResult = expectedNode.GetContent(getCriterea.Name.ToString());
                break;
                default:
                    var expectedNodeContent = expectedNode.GetContent(getCriterea.Name.ToString());
                    if (!string.IsNullOrEmpty(expectedNodeContent))
                    {
                        getResult = string.IsNullOrEmpty(getCriterea.Value) ? expectedNodeContent : Regex.Match(expectedNodeContent, getCriterea.Value, RegexOptions.IgnoreCase).Value;
                    }
                break;
            }

            var trimCritereaNode = xmlParceRule.Elements().FirstOrDefault(el => el.Name.ToString().ToLower() == (string.IsNullOrEmpty(postfix) ? TRIM_CRITEREA_NODE_NAME : TRIM_CRITEREA_NODE_NAME + postfix).ToLower());
            if (trimCritereaNode != null)
            {
                if (trimCritereaNode.Attributes().Any(attr => attr.Name.ToString().ToLower() == STRING_TRIM_ATTRIBUTE_NAME.ToLower() && bool.Parse(attr.Value)))
                {
                    var splitResult = getResult.Split(trimCritereaNode.Value.Split(new[] { ' ' }), StringSplitOptions.RemoveEmptyEntries);
                    result = trimCritereaNode.Attributes().Any(attr => attr.Name.ToString().ToLower() == GET_LAST_OCCURENCE_ATTRIBUTE_NAME.ToLower() && bool.Parse(attr.Value))
                                ? splitResult[splitResult.Length - 1]
                                : splitResult[0];
                }
                else if (trimCritereaNode.Attributes().Any(attr => attr.Name.ToString().ToLower() == REG_EXP_TRIM_ATTRIBUTE_NAME.ToLower() && bool.Parse(attr.Value)))
                {
                    if (trimCritereaNode.Attributes().Any(attr => attr.Name.ToString().ToLower() == GET_LAST_OCCURENCE_ATTRIBUTE_NAME.ToLower() && bool.Parse(attr.Value)))
                    {
                        var regExpMatch = Regex.Matches(getResult, trimCritereaNode.Value, RegexOptions.IgnoreCase);
                        result = regExpMatch[regExpMatch.Count - 1].Value;
                    }
                    else
                    {
                        result = Regex.Match(getResult, trimCritereaNode.Value, RegexOptions.IgnoreCase).Value;
                    }
                }
                else if (trimCritereaNode.Attributes().Any(attr => attr.Name.ToString().ToLower() == REMOVE_TRIM_ATTRIBUTE_NAME.ToLower() && bool.Parse(attr.Value)))
                {
                    result = result.Where(el => !trimCritereaNode.Value.Any(elem => elem == el)).Aggregate(string.Empty, (agg, el) => agg + el);
                }
                else
                {
                    result = getResult.Trim(trimCritereaNode.Value.ToArray());
                }

                var additionalTrimCritereaNode = xmlParceRule.Elements().FirstOrDefault(el => el.Name.ToString().ToLower() == (ADDITIONAL_PREFIX + (string.IsNullOrEmpty(postfix) ? TRIM_CRITEREA_NODE_NAME : TRIM_CRITEREA_NODE_NAME + postfix)).ToLower());
                if (additionalTrimCritereaNode != null)
                {
                    if (additionalTrimCritereaNode.Attributes().Any(attr => attr.Name.ToString().ToLower() == STRING_TRIM_ATTRIBUTE_NAME.ToLower() && bool.Parse(attr.Value)))
                    {
                        var splitResult = getResult.Split(trimCritereaNode.Value.Split(new[] { ' ' }), StringSplitOptions.RemoveEmptyEntries);
                        result = trimCritereaNode.Attributes().Any(attr => attr.Name.ToString().ToLower() == GET_LAST_OCCURENCE_ATTRIBUTE_NAME.ToLower() && bool.Parse(attr.Value))
                                    ? splitResult[splitResult.Length - 1]
                                    : splitResult[0];
                    }
                    else if (additionalTrimCritereaNode.Attributes().Any(attr => attr.Name.ToString().ToLower() == REG_EXP_TRIM_ATTRIBUTE_NAME.ToLower() && bool.Parse(attr.Value)))
                    {
                        if (trimCritereaNode.Attributes().Any(attr => attr.Name.ToString().ToLower() == GET_LAST_OCCURENCE_ATTRIBUTE_NAME.ToLower() && bool.Parse(attr.Value)))
                        {
                            var regExpMatch = Regex.Matches(getResult, trimCritereaNode.Value, RegexOptions.IgnoreCase);
                            result = regExpMatch[regExpMatch.Count - 1].Value;
                        }
                        else
                        {
                            result = Regex.Match(getResult, trimCritereaNode.Value, RegexOptions.IgnoreCase).Value;
                        }
                    }
                    else if (additionalTrimCritereaNode.Attributes().Any(attr => attr.Name.ToString().ToLower() == REMOVE_TRIM_ATTRIBUTE_NAME.ToLower() && bool.Parse(attr.Value)))
                    {
                        result = result.Where(el => !additionalTrimCritereaNode.Value.Any(elem => elem == el)).Aggregate(string.Empty, (agg, el) => agg + el);
                    }
                    else
                    {
                        result = result.Trim(additionalTrimCritereaNode.Value.ToArray());
                    }
                }
            }
            else
            {
                result = getResult;
            }

            return result;
        }

        protected async virtual Task<IEnumerable<IDOMElement>> GetAdditionalResourceData(string resourceUrl, HttpMethodEnum requestType = HttpMethodEnum.GET, Dictionary<string, string> requestContent = null)
        {
            IEnumerable<IDOMElement> result = null;

            using (var resourceSerializer = new ResourceSerializer())
            {
                result = await resourceSerializer.GetSerializedResourceData(new Uri(resourceUrl), requestType, requestContent);
            }

            return result;
        }

        protected virtual async Task<dynamic> ProcessExternlaGetCriterea(IEnumerable<XElement> xmlParceRules, IDOMElement expectedNode, PostModel postModel)
        {
            var result = string.Empty;

            var externalLinkNode = xmlParceRules.FirstOrDefault(el => el.Name.ToString().ToLower() == EXTERNAL_GET_LINK_NODE_NAME.ToLower());
            if (externalLinkNode != null)
            {
                var searchCritereaLinkNode = externalLinkNode.Elements().FirstOrDefault(el => el.Name.ToString().ToLower() == (SUB_SEARCH_CRITEREA_NODE_NAME + EXTERNAL_GET_LINK_NODE_NAME).ToLower());
                var linkNode = SearchForNode(searchCritereaLinkNode.Elements(), expectedNode, postModel);
                if (linkNode != null)
                {
                    var link = ProcessGetCriterea(externalLinkNode, linkNode, postModel, EXTERNAL_GET_LINK_NODE_NAME);
                    if (!string.IsNullOrEmpty(link))
                    {
                        var serializedExternalData = await GetAdditionalResourceData(string.Format("{0}\\{1}", postModel.BaseURL.TrimEnd(new[] { '\\', ' ' }), link));
                        if (serializedExternalData != null)
                        {
                            var externalGetContentCriteriaNode = xmlParceRules.FirstOrDefault(el => el.Name.ToString().ToLower() == EXTERNAL_GET_CONTENT_NODE_NAME.ToLower());
                            if (externalGetContentCriteriaNode != null)
                            {
                                var searchCriterea = externalGetContentCriteriaNode.Elements().FirstOrDefault(el => el.Name.ToString().ToLower() == SEARCH_CRITEREA_NODE_NAME.ToLower());
                                var useLastElSearch = searchCriterea.Attributes().Any(attr => attr.Name.ToString().ToLower() == USE_LAST_CHILD_SEARCH_ATTRIBUTE_NAME.ToLower() && bool.Parse(attr.Value));

                                var expectedDataNode = SearchForNode(searchCriterea.Elements(), serializedExternalData, postModel, !useLastElSearch);
                                if (expectedDataNode != null)
                                {
                                    result = ProcessGetCriterea(externalGetContentCriteriaNode, expectedDataNode, postModel);
                                }
                                else
                                {
                                    Logger.LogException("ProcessExternlaGetCriterea from BaseXMLGuideParcer factory no node found from external SearchForNode process", LogTypeEnum.CORE);
                                }
                            }
                            else
                            {
                                Logger.LogException("ProcessExternlaGetCriterea from BaseXMLGuideParcer factory no get criteria node", LogTypeEnum.CORE);
                            }
                        }
                        else
                        {
                            Logger.LogException("ProcessExternlaGetCriterea from BaseXMLGuideParcer factory serialization result is null", LogTypeEnum.CORE);
                        }
                    }
                    else
                    {
                        Logger.LogException("ProcessExternlaGetCriterea from BaseXMLGuideParcer factory has an empty link", LogTypeEnum.CORE);
                    }
                }
                else
                {
                    Logger.LogException("ProcessExternlaGetCriterea from BaseXMLGuideParcer factory can`t find a link node", LogTypeEnum.CORE);
                }
            }
            else
            {
                Logger.LogException("ProcessExternlaGetCriterea from BaseXMLGuideParcer factory link node in XML Guide is not found", LogTypeEnum.CORE);
            }    
                    
            return result;
        }
    }
}
