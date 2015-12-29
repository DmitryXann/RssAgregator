using RssAgregator.CORE.Helpers;
using RssAgregator.CORE.Interfaces.DOMObjectModel;
using RssAgregator.CORE.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RssAgregator.CORE.Parcers.DOMObjectModel
{
    public class DOMElement : IDOMElement
    {
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
                else if (!BadTag && ElementType == DOMEleementTypeEnum.Tag && !string.IsNullOrEmpty(TagName) && TagName.ToLower() == "img" && string.IsNullOrEmpty(_content)) 
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
                return ElementType == DOMEleementTypeEnum.Text;
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
                    if (Element[1] == '!')
                    {
                        ElementType = DOMEleementTypeEnum.Comment;
                        IsDomElementComplete = true;
                    }
                    else 
                    {
                        ElementType = DOMEleementTypeEnum.Tag;

                        var tagNameIndexEnd = Element.FirstIndexOfAny(false, " ", "\t", "\n");
                        if (tagNameIndexEnd > 0)
                        {
                            TagName = Element.GetRange(1, tagNameIndexEnd - 1).ToString();
                        }
                        else if (Element[Element.Length - 1] == '>')
                        {
                            TagName = Element.GetRange(1, Element.Length - 2).ToString();
                        }
                        else
                        {
                            BadTag = true;
                            IsDomElementComplete = true;
                        }

                        IsDomElementComplete = Element[Element.Length - 1] == '>' && Element[Element.Length - 2] == '/' || !string.IsNullOrEmpty(TagName) && TagName.ToLower() == "br";
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
                    result = TagName == value;
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
                foreach (var el in currentElement.Childs)
                {
                    GetFirstChildInSubThree(el, filter, ref foundElement);
                }
            }
        }

        private string DeserializeChilds(IDOMElement currentElement, bool doNotAddRootElement)
        {
            return (doNotAddRootElement && currentElement == this ? string.Empty : currentElement.Element.ToString()) +
                    currentElement.Childs.Select(el => DeserializeChilds(el, doNotAddRootElement)).Aggregate(string.Empty, (agg, el) => agg + el) +
                    (string.IsNullOrEmpty(currentElement.TagName) ||
                    (doNotAddRootElement && currentElement == this) || 
                    (!string.IsNullOrEmpty(currentElement.TagName) && currentElement.TagName.ToLower()  == "br") ? string.Empty : string.Format("</{0}>", currentElement.TagName));
        }
    }
}
