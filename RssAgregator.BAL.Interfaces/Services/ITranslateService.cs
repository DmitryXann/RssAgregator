using RssAgregator.Models.Results;

namespace RssAgregator.BAL.Interfaces.Services
{
    public interface ITranslateService
    {
        GenericResult<string> Translate(string text);
    }
}
