using RssAgregator.Models.Results;
using RssAgregator.Models.Services.OnlineRadio;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RssAgregator.BAL.Interfaces.Services
{
    public interface IOnlineRadioService
    {
        Task<GenericResult<IEnumerable<OnlineRadioServiceSongModel>>> Search(OnlineRadioServiceSearchModel searchModel);

        Task<GenericResult<IEnumerable<string>>> TypeAheadSearch(string question);
    }
}
