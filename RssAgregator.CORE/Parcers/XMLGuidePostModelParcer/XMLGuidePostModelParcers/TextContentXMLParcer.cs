using RssAgregator.CORE.Interfaces.DOMObjectModel;
using RssAgregator.CORE.Interfaces.Parcers.XMLGuidePostModelParcer.XMLGuidePostModelParcers;
using RssAgregator.CORE.Models.Enums;
using RssAgregator.CORE.Models.PostModel;
using RssAgregator.CORE.Models.PostModel.PostContentModel;
using System.Xml.Linq;

namespace RssAgregator.CORE.Parcers.XMLGuidePostModelParcer.XMLGuidePostModelParcers
{
    public class TextContentXMLParcer : BaseXMLGuideParcer, IXMLGuidePostModelParcer
    {
        private TextContentXMLParcer()
        { 
        }

        public override object ProcessDOMNode(XElement xmlParceRule, IDOMElement expectedDOMElement, PostModel postModel)
        {
            var textContent = (string)base.ProcessDOMNode(xmlParceRule, expectedDOMElement, postModel);
            return string.IsNullOrEmpty(textContent) ? null : PostContentModelfactory.GetInitializedFactory(PostContentTypeEnum.Text, postModel.PostContent.Count, textContent);
        }
    }
}
