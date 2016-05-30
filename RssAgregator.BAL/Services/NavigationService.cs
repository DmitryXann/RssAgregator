using Microsoft.Practices.Unity;
using RssAgregator.BAL.Interfaces.Services;
using RssAgregator.DAL;
using RssAgregator.Models.Results;
using RssAgregator.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RssAgregator.BAL.Services
{
    public class NavigationService : INavigationService
    {
        [Dependency]
        public ISettingService SettingService { get; set; }

        public GenericResult<IEnumerable<NavigationModel>> GetNavigationData()
        {
            var result = new GenericResult<IEnumerable<NavigationModel>>();

            try
            {
                using (var db = new RssAggregatorModelContainer())
                {
                    result.SetDataResult(db.GetDBSet<Navigation>(el => el.IsActive)
                                            .OrderBy(el => el.OrderNo)
                                            .ToList()
                                            .Select(el => el.GetModel())
                                            .ToList());

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
