using RssAgregator.CORE.Interfaces.DOMObjectModel;
using RssAgregator.CORE.Interfaces.Parcers.XMLGuidePostModelParcer.XMLGuidePostModelParcers;
using RssAgregator.CORE.Models.PostModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace RssAgregator.CORE.Parcers.XMLGuidePostModelParcer.XMLGuidePostModelParcers
{
    public abstract class BaseXMLGuideParcer : IXMLGuidePostModelParcer
    {
        protected const string SEARCH_CRITEREA_NODE_NAME = "SearchCriterea";
        protected const string SUB_SEARCH_CRITEREA_NODE_NAME = "SubSearchCriterea";

        protected const string GET_CRITEREA_NODE_NAME = "GetCriterea";
        protected const string TRIM_CRITEREA_NODE_NAME = "TrimCriterea";
        protected const string USE_STRICT_EQUAL_CHECK_ATTRIBUTE_NAME = "strictEqual";
        protected const string STRING_TRIM_NAME = "stringTrim";

        protected const string EXPECTED_AUTHOR_ID_MACRO_NAME = "{AuthorId}";
        protected const string EXPECTED_POST_ID_MACRO_NAME = "{PostId}";

        protected virtual IDOMElement SearchForNode(IEnumerable<XElement> searchCriterea, IDOMElement domElement, PostModel postModel)
        {
            return domElement.GetFirstChildInSubThree(el => searchCriterea.All(elem =>
                                                                                el.ElementMatch(elem.Name.ToString(), elem.Value.Replace(EXPECTED_AUTHOR_ID_MACRO_NAME, postModel.AuthorId)
                                                                                                                                .Replace(EXPECTED_POST_ID_MACRO_NAME, postModel.PostId), 
                                                                                elem.Attributes().Any(attr => attr.Name.ToString().ToLower() == USE_STRICT_EQUAL_CHECK_ATTRIBUTE_NAME.ToLower() && bool.Parse(attr.Value)))));
        }

        public virtual object ProcessDOMNode(XElement xmlParceRule, IDOMElement expectedDOMElement, PostModel postModel)
        {
            object result = null;

            var searchCritereaNode = xmlParceRule.Elements().FirstOrDefault(el => el.Name.ToString().ToLower() == SEARCH_CRITEREA_NODE_NAME.ToLower());

            var expectedNode = SearchForNode(searchCritereaNode.Elements(), expectedDOMElement, postModel);
            if (expectedNode != null)
            {
                result = ProcessGetCriterea(xmlParceRule, expectedNode, postModel);
            }
            

            return result;
        }

        protected virtual object ProcessGetCriterea(XElement xmlParceRule, IDOMElement expectedNode, PostModel postModel, string postfix = null)
        {
            object result = null;

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
                result = trimCritereaNode.Attributes().Any(attr => attr.Name.ToString().ToLower() == STRING_TRIM_NAME.ToLower() && bool.Parse(attr.Value))
                                ? getResult.Split(trimCritereaNode.Value.Split(new[] { ' ' }), StringSplitOptions.RemoveEmptyEntries)[0]
                                : getResult.Trim(trimCritereaNode.Value.ToArray());
            }
            else
            {
                result = getResult;
            }

            return result;
        }
    }
}
