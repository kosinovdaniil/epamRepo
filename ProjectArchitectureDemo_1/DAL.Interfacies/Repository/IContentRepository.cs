using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface IContentRepository : IRepository<DalContent>
    {
        void AddComment(DalComment comment);
        void DeleteComment(DalComment comment);
        void UpdateComment(DalComment comment);
        int UpVote(DalContent content,DalUser user);
        int DownVote(DalContent content,DalUser user);
        DalComment GetComment(int id);
    }
}
