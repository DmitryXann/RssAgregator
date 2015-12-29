
namespace RssAgregator.WEB.Models
{
    public class NewsSearchModel
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public bool HideAdult { get; set; }
    }
}