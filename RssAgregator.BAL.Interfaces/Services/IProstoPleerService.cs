using RssAgregator.Models.Services;
using System.Threading.Tasks;

namespace RssAgregator.BAL.Interfaces.Services
{
    public interface IProstoPleerService
    {
        Task<string> Search(ProstoPleerSearchModel searchModel);
    }
}
