using System.Data.Entity;
using System.Linq;

namespace RssAgregator.DAL
{
    public partial class RssAggregatorModelContainer : DbContext
    {
        private User _systemUser;
        public User GetSystemUser
        {
            get
            {
                if (_systemUser == null)
                {
                    _systemUser = GetDBSet<User>().First(el => el.Type == UserTypeEnum.System);
                }
                return _systemUser;
            }
        }
    }
}
