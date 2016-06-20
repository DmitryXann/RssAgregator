using RssAgregator.CORE.Interfaces.Models.DOMObjectModel;
using RssAgregator.CORE.Models.PostModel;
using System.Xml.Linq;

namespace RssAgregator.CORE.Interfaces.Parcers.XMLGuidePostModelParcer.XMLGuidePostModelParcers
{
    public interface IXMLGuidePostModelParcer
    {
        dynamic ProcessDOMNode(XElement xmlParceRule, IDOMElement expectedDOMElement, PostModel postModel);
    }
}
