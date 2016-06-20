using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RssAgregator.DAL
{
    public partial class RssAggregatorModelContainer : DbContext
    {
        private IEnumerable<Tags> _tags;
        public IEnumerable<Tags> Tags
        {
            get
            {
                if (_tags == null)
                {
                    _tags = GetDBSet<Tags>().ToList();
                }

                return _tags;
            }
        }

        public string GetTagName(TagTypeEnum expectedType, LocationEnum location)
        {
            return Tags.FirstOrDefault(el => el.Type == expectedType && el.Location == location).Name;
        }
    }
}
