using System.Data.Entity;

namespace RssAgregator.DAL
{
    public partial class RssAggregatorModelContainer : DbContext
    {
        private bool _saveChanges;

        public RssAggregatorModelContainer(bool saveChanges)
            : this()
        {
            _saveChanges = saveChanges;
        }

        protected override void Dispose(bool disposing)
        {
            if (_saveChanges)
            {
                base.SaveChanges();
            }

            base.Dispose(disposing);
        }
    }
}
