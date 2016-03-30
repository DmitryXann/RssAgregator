using RssAgregator.CORE.Models.PostModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RssAgregator.CORE.Interfaces.Parcers
{
    public interface IResourceParcer : IDisposable
    {
        int DefaultPageCount { get; }

        Task<IEnumerable<PostModel>> GetContent(Uri expectedUri);

        void AddSearchCriteria(string queston);

        void SetPageNumber(int currentPage);

        void ResetPageCounter();
    }
}
