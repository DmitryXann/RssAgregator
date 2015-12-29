using RssAgregator.BAL.Interfaces.Services;
using RssAgregator.Models.Services;
using System.Net.Http;
using System.Threading.Tasks;

namespace RssAgregator.BAL.Services
{
    public class ProstoPleerService : IProstoPleerService
    {
        private const string PROSTO_PLEER_URI = "http://pleer.com/browser-extension/search";

        public async Task<string> Search(ProstoPleerSearchModel searchModel)
        {
            using (var webClient = new HttpClient())
            {
                var serverResponceTask = webClient.GetAsync(string.Format("{0}?q={1}&page={2}&limit={3}", PROSTO_PLEER_URI, searchModel.Question, searchModel.PageNumber, searchModel.ResultLimit));

                return await (await serverResponceTask).Content.ReadAsStringAsync();
            }
        }
    }
}
