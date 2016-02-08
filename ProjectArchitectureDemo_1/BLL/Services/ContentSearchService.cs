using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Services;

namespace BLL.Services
{
    public class ContentSearchService : IContentSearchService
    {
        public IEnumerable<int> Search(string input)
        {
            return Lucene.LuceneSearch.Search(input);
        }
    }
}
