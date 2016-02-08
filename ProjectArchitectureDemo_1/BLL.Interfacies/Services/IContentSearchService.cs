using System.Collections.Generic;

namespace BLL.Interface.Services
{
    public interface IContentSearchService
    {
        IEnumerable<int> Search(string input);
    }
}