using RssAgregator.Models.Results;
using RssAgregator.Models.Services;
using System.Collections.Generic;

namespace RssAgregator.BAL.Interfaces.Services
{
    public interface INavigationService
    {
        GenericResult<IEnumerable<NavigationModel>> GetNavigationData();
    }
}
