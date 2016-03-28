using RssAgregator.Models.Results;
using RssAgregator.Models.Services;
using System.Collections.Generic;

namespace RssAgregator.BAL.Interfaces.Services
{
    public interface ITemplateService
    {
        GenericResult<IEnumerable<TemplateModel>> GetAllUserTemplates();
        GenericResult<TemplateModel> GetUserTemplate(string templateType, int userId);
    }
}
