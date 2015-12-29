using RssAgregator.CORE.Interfaces.DOMObjectModel;
using RssAgregator.CORE.Models.PostModel;
using System.Xml.Linq;

namespace RssAgregator.CORE.Interfaces.Parcers.XMLGuidePostModelParcer.XMLGuidePostModelParcers
{
    public interface IXMLGuidePostModelParcer
    {
        object ProcessDOMNode(XElement xmlParceRule, IDOMElement expectedDOMElement, PostModel postModel);
    }
}
