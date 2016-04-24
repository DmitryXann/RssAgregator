using Microsoft.Practices.Unity;
using RssAgregator.BAL.Interfaces.Services;
using RssAgregator.DAL;
using RssAgregator.Models.Results;
using RssAgregator.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RssAgregator.BAL.Services
{
    public class TemplateService : ITemplateService
    {
        [Dependency]
        public ISettingService SettingService { get; set; }

        public GenericResult<IEnumerable<TemplateModel>> GetAllUserTemplates()
        {
            var result = new GenericResult<IEnumerable<TemplateModel>>();

            try
            {
                using (var db = new RssAggregatorModelContainer())
                {
                    result.SetDataResult(db.GetDBSet<Template>(el => el.Type == TemplateTypeEnum.Header || el.Type == TemplateTypeEnum.Post)
                                            .ToList()
                                            .Select(el => el.GetModel()));
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, LogTypeEnum.BAL);
                result.SetErrorResultCode(SettingService.GetUserFriendlyExceptionMessage());
            }

            return result;
        }

        public GenericResult<TemplateModel> GetUserTemplate(string templateOrTemplateType, int userId)
        {
            var result = new GenericResult<TemplateModel>();

            try
            {
                using (var db = new RssAggregatorModelContainer())
                {
                    Expression<Func<Template, bool>> predicate;

                    TemplateTypeEnum expectedTemplate;
                    if (Enum.TryParse(templateOrTemplateType, out expectedTemplate))
                    {
                        predicate = el => el.Type == expectedTemplate;
                    }
                    else
                    {
                        predicate = el => el.Name.ToLower() == templateOrTemplateType.ToLower();
                    }

                    var selectedEntity = db.GetEntity<Template>(predicate);

                    result.SetDataResult(selectedEntity == null ? null : selectedEntity.GetModel());
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, LogTypeEnum.BAL);
                result.SetErrorResultCode(SettingService.GetUserFriendlyExceptionMessage());
            }

            return result; 
        }
    }
}
