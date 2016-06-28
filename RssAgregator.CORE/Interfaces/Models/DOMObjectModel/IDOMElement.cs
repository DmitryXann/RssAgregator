using RssAgregator.CORE.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RssAgregator.CORE.Interfaces.Models.DOMObjectModel
{
    public interface IDOMElement
    {
        IDOMElement Parent { get; }
        List<IDOMElement> Childs { get; }

        StringBuilder Element { get; }

        bool BadTag { get; }
        bool IsDomElementComplete { get; set; }
        DOMEleementTypeEnum ElementType { get; }
        string TagName { get;  }

        string TagId {get;}

        string TagClass {get;}

        string Content { get; }

        bool IsContentElement { get; }

        void AddChild(IDOMElement child);
        void AddChildRange(IEnumerable<IDOMElement> childs);
        void AddComplexElementContent(string content);

        string GetTagPropertyContent(string propertyName);
        bool ElementMatch(string key, string value, bool useContains = false);
        string GetContent(string contentSource);
        string DesirializeNode();

        IDOMElement GetFirstChild(Func<IDOMElement, bool> filter);
        IDOMElement GetFirstChildInSubThree(Func<IDOMElement, bool> filter);

        IDOMElement GetLastChild(Func<IDOMElement, bool> filter);
        IDOMElement GetLastChildInSubThree(Func<IDOMElement, bool> filter);

        IDOMElement GetLastChildFromChilds(Func<IDOMElement, string, bool> filter, params string[] childs);

        IDOMElement this[int index] { get; set; }
    }
}
