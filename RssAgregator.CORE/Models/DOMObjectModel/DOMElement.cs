using RssAgregator.CORE.Helpers;
using RssAgregator.CORE.Interfaces.Models.DOMObjectModel;
using RssAgregator.CORE.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RssAgregator.CORE.Models.DOMObjectModel
{
    public class DOMElement : IDOMElement
    {
        private readonly IEnumerable<string> tagsWithoutClosingTag = new[] { "area", "base", "br", "col", "command", "embed", "hr", "img", "input", "input", "meta", "param", "source", "html", "link", "!doctype", "!" };

        public IDOMElement Parent { get; private set; }
        public List<IDOMElement> Childs { get; private set; }

        public StringBuilder Element { get; private set; }

        public bool BadTag { get; private set; }
        public bool IsDomElementComplete { get; set; }
        public DOMEleementTypeEnum ElementType { get; private set; }
        public string TagName { get; private set; }

        private string _tagId;
        public string TagId
        {
            get
            {
                if (!BadTag && ElementType == DOMEleementTypeEnum.Tag && string.IsNullOrEmpty(_tagId))
                {
                    _tagId = GetTagPropertyContent("id");
                }

                return _tagId;
            }
        }

        private string _tagClass;
        public string TagClass
        {
            get
            {
                if (!BadTag && ElementType == DOMEleementTypeEnum.Tag && string.IsNullOrEmpty(_tagClass))
                {
                    _tagClass = GetTagPropertyContent("class");
                }

                return _tagClass;
            }
        }

        private string _content;
        public string Content
        {
            get
            {
                if (!BadTag && ElementType == DOMEleementTypeEnum.Text && string.IsNullOrEmpty(_content))
                {
                    _content = Element.ToString();
                }
                else if (!BadTag && ElementType == DOMEleementTypeEnum.Tag && !string.IsNullOrEmpty(TagName) && TagName == "img" && string.IsNullOrEmpty(_content)) 
                {
                    _content = GetTagPropertyContent("src");
                }

                return _content;
            }
        }

        public bool IsContentElement
        {
            get 
            {
                return ElementType == DOMEleementTypeEnum.Text || ElementType == DOMEleementTypeEnum.Script;
            }        
        }

        public DOMElement(IDOMElement parent, StringBuilder domElement)
        {
            Parent = parent;
            Element = domElement;

            Childs = new List<IDOMElement>();

            if (Element.Length > 0)
            {
                if (Element[0] == '<')
                {
                    var tagNameIndexEnd = Element.FirstIndexOfAny(false, " ", "\t", "\n");
                    if (tagNameIndexEnd > 0)
                    {
                        TagName = Element.GetRange(1, tagNameIndexEnd - 1).ToString().ToLower();
                    }
                    else if (Element[Element.Length - 1] == '>')
                    {
                        TagName = Element.GetRange(1, Element.Length - 2).ToString().ToLower();
                    }
                    else
                    {
                        BadTag = true;
                        IsDomElementComplete = true;
                    }

                    switch (TagName)
                    {
                        case "script":
                            ElementType = DOMEleementTypeEnum.Script;
                        break;
                        case "noscript":
                            ElementType = DOMEleementTypeEnum.NoScript;
                        break;
                        default:
                            if (Element[1] == '!')
                            {
                                ElementType = DOMEleementTypeEnum.Comment;

                                if (Element.Length >= 4 && (Element[2] == '-' && Element[3] == '-'))
                                {
                                    TagName = "--";

                                    if (Element.Length >= 7 && (Element[Element.Length - 2] == '-' && Element[Element.Length - 3] == '-'))
                                    {
                                        IsDomElementComplete = true;
                                    }
                                }
                            }
                            else if (Element.Length >= 4 && (Element[Element.Length - 2] == '-' && Element[Element.Length - 2] == '-'))
                            {
                                ElementType = DOMEleementTypeEnum.Comment;
                                TagName = "--";
                            }
                            else
                            {
                                ElementType = DOMEleementTypeEnum.Tag;
                            }
                        break;
                    }

                    if (!IsDomElementComplete)
                    {
                        IsDomElementComplete = (Element[Element.Length - 1] == '>' && Element[Element.Length - 2] == '/') ||
                                                (!string.IsNullOrEmpty(TagName) && tagsWithoutClosingTag.Contains(TagName));
                    }
                }
                else
                {
                    IsDomElementComplete = true;
                }
            }
            else
            {
                BadTag = true;
                IsDomElementComplete = true;
            }
            
        }

        public void AddChild(IDOMElement child)
        {
            Childs.Add(child);
        }

        public void AddChildRange(IEnumerable<IDOMElement> childs)
        {
            Childs.AddRange(childs);
        }

        public void AddComplexElementContent(string content)
        {
            if (ElementType == DOMEleementTypeEnum.Script || ElementType == DOMEleementTypeEnum.Comment || ElementType == DOMEleementTypeEnum.NoScript)
            {
                _content += content;
            }
            else
            {
                throw new InvalidOperationException("AddContent is only valid for the DOMEleementTypeEnum.Script ElementType");
            }
        }

        public string GetTagPropertyContent(string propertyName)
        {
            string result = null;

            var tagPropertyIndex = Element.FirstIndexOfAny(true, string.Format(" {0}=\"", propertyName), string.Format("\t{0}=\"", propertyName), string.Format("\n{0}=\"", propertyName), string.Format("\"{0}=\"", propertyName));
            if (tagPropertyIndex > 0)
            {
                result = string.Empty; 

                var tagPropertyContentStart = tagPropertyIndex + propertyName.Length + 3;
                var index = tagPropertyContentStart;

                while ((index < Element.Length && index + 1 < Element.Length) && 
                    (Element[index] != '\"' || (Element[index + 1] != ' ' && Element[index + 1] != '\t' && Element[index + 1] != '\n' && Element[index + 1] != '>')))
                {
                    index++;
                }

                result = Element.GetRange(tagPropertyContentStart, index - tagPropertyContentStart).ToString();
            }

            return result;
        }

        public bool ElementMatch(string key, string value, bool strictEqual = false)
        {
            var result = false;

            switch(key.ToLower())
            {
                case "name":
                    result = TagName == value.ToLower();
                break;
                default:
                    var tagPropertyContent = GetTagPropertyContent(key);
                    result = tagPropertyContent != null ? (strictEqual ? tagPropertyContent == value : tagPropertyContent.Contains(value)) : false; 
                break;
            }        

            return  result;
        }

        public string GetContent(string contentSource)
        {
            var result = string.Empty;

            switch (contentSource.ToLower())
            {
                case "values":
                    var contentNode = GetFirstChild(el => el.IsContentElement);
                    result = contentNode != null ? contentNode.Content : string.Empty;
                break;
                case "childs":
                    result = DeserializeChilds(this, true);
                break;
                case "element":
                    result = DesirializeNode();
                break;
                case "content":
                    result = Content;
                break;
                default:
                    result = GetTagPropertyContent(contentSource);
                break;
            }

            return result;
        }

        public IDOMElement GetFirstChild(Func<IDOMElement, bool> filter)
        {
            return Childs.FirstOrDefault(filter);
        }

        public IDOMElement GetFirstChildInSubThree(Func<IDOMElement, bool> filter)
        {
            IDOMElement firstChildInSubThree = null;

            GetFirstChildInSubThree(this, filter, ref firstChildInSubThree);

            return firstChildInSubThree;
        }

        public IDOMElement GetLastChild(Func<IDOMElement, bool> filter)
        {
            return Childs.LastOrDefault(filter);
        }

        public IDOMElement GetLastChildInSubThree(Func<IDOMElement, bool> filter)
        {
            IDOMElement lastChildInSubThree = null;

            GetLastChildInSubThree(this, filter, ref lastChildInSubThree);

            return lastChildInSubThree;
        }

        public IDOMElement GetLastChildFromChilds(Func<IDOMElement, string, bool> filter, params string[] childs)
        {
            IDOMElement currentNode = this;
            var index = 0;

            while (currentNode != null && index < childs.Length)
            {
                var curretnIndex = index++;
                currentNode = currentNode.GetFirstChild(el => filter(el, childs[curretnIndex]));
            }

            return currentNode;
        }

        public string DesirializeNode()
        {
            return Element.ToString() + (string.IsNullOrEmpty(TagName) || (!string.IsNullOrEmpty(TagName) && tagsWithoutClosingTag.Contains(TagName))
                                            ? string.Empty
                                            : string.Format("</{0}>", TagName));
        }

        public IDOMElement this[int index]
        {
            get
            {
                return Childs[index];
            }

            set 
            {
                Childs[index] = value;
            }
        }

        public override string ToString()
        {
            return string.Format("Element type: {0}, TagName: {1}, TagId: {2}, TagClass: {3}, Content: {4}", ElementType, TagName, TagId, TagClass, Content);
        }


        private void GetFirstChildInSubThree(IDOMElement currentElement, Func<IDOMElement, bool> filter, ref IDOMElement foundElement)
        {
            if (filter(currentElement))
            {
                foundElement = currentElement;
            }
            else
            {
                var childs = currentElement.Childs;
                var index = 0;

                while (foundElement == null && index < childs.Count)
                {
                    GetFirstChildInSubThree(childs[index++], filter, ref foundElement);
                }
            }
        }

        private void GetLastChildInSubThree(IDOMElement currentElement, Func<IDOMElement, bool> filter, ref IDOMElement foundElement)
        {
            if (filter(currentElement))
            {
                foundElement = currentElement;
            }
            else
            {
                var childs = currentElement.Childs;
                var index = childs.Count - 1;

                while(foundElement == null && index >= 0)
                {
                    GetLastChildInSubThree(childs[index--], filter, ref foundElement);
                }
            }
        }

        private string DeserializeChilds(IDOMElement currentElement, bool doNotAddRootElement)
        {
            return (doNotAddRootElement && currentElement == this ? string.Empty : currentElement.Element.ToString()) +
                    currentElement.Childs.Select(el => DeserializeChilds(el, doNotAddRootElement)).Aggregate(string.Empty, (agg, el) => agg + el) +
                    (string.IsNullOrEmpty(currentElement.TagName) ||
                    (doNotAddRootElement && currentElement == this) || 
                    (!string.IsNullOrEmpty(currentElement.TagName) && tagsWithoutClosingTag.Contains(currentElement.TagName)) ? string.Empty : string.Format("</{0}>", currentElement.TagName));
        }
    }
}
