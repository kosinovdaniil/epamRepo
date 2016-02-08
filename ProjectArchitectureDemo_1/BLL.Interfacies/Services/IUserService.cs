using System.Collections.Generic;
using BLL.Interface.Entities;
using System.Linq.Expressions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Interface.Services
{
    public interface IUserService
    {
        UserEntity GetUserEntity(int id);
        UserEntity GetUserEntity(string name);
        IEnumerable<UserEntity> GetAllUserEntities();
        UserEntity CreateUser(UserEntity user);
        void DeleteUser(UserEntity user);
        void UpdateUser(UserEntity user);
        double GetAverageRating(UserEntity user);
        IEnumerable<UserEntity> GetUsersByPredicate(Expression<Func<UserEntity, bool>> f);
        UserEntity ValidateUser(string name, string password);
        bool UserVoted(string username, ContentEntity content);
        IEnumerable<RoleEntity> GetAllRoles();
    }
}