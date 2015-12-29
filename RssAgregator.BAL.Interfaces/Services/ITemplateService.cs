using RssAgregator.Models.Services;
using System.Collections.Generic;

namespace RssAgregator.BAL.Interfaces.Services
{
    public interface ITemplateService
    {
        IEnumerable<TemplateModel> GetAllUserTemplates();
        TemplateModel GetUserTemplate(string templateType, int userId);
    }
}
