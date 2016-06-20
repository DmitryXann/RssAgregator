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

        string GetTagPropertyContent(string propertyName);
        bool ElementMatch(string key, string value, bool useContains = false);
        string GetContent(string contentSource);

        IDOMElement GetFirstChild(Func<IDOMElement, bool> filter);
        IDOMElement GetLastChildFromChilds(Func<IDOMElement, string, bool> filter, params string[] childs);
        IDOMElement GetFirstChildInSubThree(Func<IDOMElement, bool> filter);

        IDOMElement this[int index] { get; set; }
    }
}
