using DAL.Interface.DTO;
using System.Collections.Generic;

namespace DAL.Interface.Repository
{
    public interface IUserRepository : IRepository<DalUser>
    {
        DalUser GetByName(string name);
        IEnumerable<DalRole> GetAllRoles();
        DalRole GetRole(string name);
    }
}