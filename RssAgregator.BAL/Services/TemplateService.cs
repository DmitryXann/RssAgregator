using RssAgregator.BAL.Interfaces.Services;
using RssAgregator.DAL;
using RssAgregator.Models.Services;
using System.Collections.Generic;
using System.Linq;
using System;

namespace RssAgregator.BAL.Services
{
    public class TemplateService : ITemplateService
    {
        public IEnumerable<TemplateModel> GetAllUserTemplates()
        { 
            using(var db = new RssAggregatorModelContainer())
            {
                return db.GetDBSet<Template>(el => el.Type == TemplateTypeEnum.Header || el.Type == TemplateTypeEnum.Post)
                        .ToList()
                        .Select(el => el.GetModel());
            }
        }

        public TemplateModel GetUserTemplate(string templateType, int userId)
        {
            TemplateModel result;

            TemplateTypeEnum expectedTemplate;
            if (Enum.TryParse(templateType, out expectedTemplate) || expectedTemplate == TemplateTypeEnum.System)
            {
                using (var db = new RssAggregatorModelContainer())
                {
                    var selectedEntity = db.GetEntity<Template>(el => el.Type == expectedTemplate);
                    result = selectedEntity == null ? null : selectedEntity.GetModel();
                }
            }
            else
            {
                result = null;
            }

            return result; 
        }
    }
}
