using RssAgregator.CORE.Models.PostModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RssAgregator.CORE.Interfaces.Parcers
{
    public interface IResourceParcer
    {
        Task<IEnumerable<PostModel>> GetContent(Uri expectedUri);
        void ResetPageCounter();
        int DefaultPageCount { get; }
    }
}
