using RssAgregator.CORE.Interfaces.Models.DOMObjectModel;
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
        protected const string SEARCH_CRITEREA_NODE_NAME                = "SearchCriterea";
        protected const string SUB_SEARCH_CRITEREA_NODE_NAME            = "SubSearchCriterea";

        protected const string GET_CRITEREA_NODE_NAME                   = "GetCriterea";
        protected const string TRIM_CRITEREA_NODE_NAME                  = "TrimCriterea";
        protected const string ADDITIONAL_PREFIX                        = "Additional";
        protected const string USE_STRICT_EQUAL_CHECK_ATTRIBUTE_NAME    = "strictEqual";
        protected const string STRING_TRIM_NAME                         = "stringTrim";
        protected const string REG_EXP_TRIM_NAME                        = "regExpTrim";

        protected const string EXPECTED_AUTHOR_ID_MACRO_NAME            = "{AuthorId}";
        protected const string EXPECTED_POST_ID_MACRO_NAME              = "{PostId}";

        protected virtual IDOMElement SearchForNode(IEnumerable<XElement> searchCriterea, IDOMElement domElement, PostModel postModel)
        {
            return domElement.GetFirstChildInSubThree(el => searchCriterea.All(elem =>
                                                                                el.ElementMatch(elem.Name.ToString(), elem.Value.Replace(EXPECTED_AUTHOR_ID_MACRO_NAME, postModel.AuthorId)
                                                                                                                                .Replace(EXPECTED_POST_ID_MACRO_NAME, postModel.PostId), 
                                                                                elem.Attributes().Any(attr => attr.Name.ToString().ToLower() == USE_STRICT_EQUAL_CHECK_ATTRIBUTE_NAME.ToLower() && bool.Parse(attr.Value)))));
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

            string getResult = string.Empty;
            switch (getCriterea.Name.ToString().ToLower())
            {
                case "values":
                case "childs":
                    getResult = expectedNode.GetContent(getCriterea.Name.ToString());
                    break;
                default:
                    var expectedNodeContent = expectedNode.GetContent(getCriterea.Name.ToString());
                    getResult = expectedNodeContent != null ? Regex.Match(expectedNodeContent, getCriterea.Value, RegexOptions.IgnoreCase).Value : null;
                    break;
            }

            var trimCritereaNode = xmlParceRule.Elements().FirstOrDefault(el => el.Name.ToString().ToLower() == (string.IsNullOrEmpty(postfix) ? TRIM_CRITEREA_NODE_NAME : TRIM_CRITEREA_NODE_NAME + postfix).ToLower());
            if (trimCritereaNode != null)
            {
                if (trimCritereaNode.Attributes().Any(attr => attr.Name.ToString().ToLower() == STRING_TRIM_NAME.ToLower() && bool.Parse(attr.Value)))
                {
                    result = getResult.Split(trimCritereaNode.Value.Split(new[] { ' ' }), StringSplitOptions.RemoveEmptyEntries)[0];
                }
                else if (trimCritereaNode.Attributes().Any(attr => attr.Name.ToString().ToLower() == REG_EXP_TRIM_NAME.ToLower() && bool.Parse(attr.Value)))
                {
                    result = Regex.Match(getResult, trimCritereaNode.Value, RegexOptions.IgnoreCase).Value;
                }
                else
                {
                    result = getResult.Trim(trimCritereaNode.Value.ToArray());
                }

                var additionalTrimCritereaNode = xmlParceRule.Elements().FirstOrDefault(el => el.Name.ToString().ToLower() == (ADDITIONAL_PREFIX + (string.IsNullOrEmpty(postfix) ? TRIM_CRITEREA_NODE_NAME : TRIM_CRITEREA_NODE_NAME + postfix)).ToLower());
                if (additionalTrimCritereaNode != null)
                {
                    if (additionalTrimCritereaNode.Attributes().Any(attr => attr.Name.ToString().ToLower() == STRING_TRIM_NAME.ToLower() && bool.Parse(attr.Value)))
                    {
                        result = result.Split(additionalTrimCritereaNode.Value.Split(new[] { ' ' }), StringSplitOptions.RemoveEmptyEntries)[0];
                    }
                    else if (additionalTrimCritereaNode.Attributes().Any(attr => attr.Name.ToString().ToLower() == REG_EXP_TRIM_NAME.ToLower() && bool.Parse(attr.Value)))
                    {
                        result = Regex.Match(result, additionalTrimCritereaNode.Value, RegexOptions.IgnoreCase).Value;
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

        protected async virtual Task<IEnumerable<IDOMElement>> GetAdditionalResourceData(Uri resourceUrl, HttpMethodEnum requestType = HttpMethodEnum.GET, Dictionary<string, string> requestContent = null)
        {
            IEnumerable<IDOMElement> result = null;

            using (var resourceSerializer = new ResourceSerializer())
            {
                result = await resourceSerializer.GetSerializedResourceData(resourceUrl, requestType, requestContent);
            }

            return result;
        }
    }
}
