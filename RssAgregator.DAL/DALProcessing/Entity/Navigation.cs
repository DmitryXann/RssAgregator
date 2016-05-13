using RssAgregator.Models.Services;

namespace RssAgregator.DAL
{
    public partial class Navigation
    {
        public NavigationModel GetModel()
        {
            return new NavigationModel
            {
                Title = Title,
                RedirectTo = RedirectTo,
                OrderNo = OrderNo
            };
        }
    }
}
