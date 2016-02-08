using System.Collections.Generic;

namespace BLL.Interface.Entities
{
    public class UserEntity
    {
        public UserEntity()
        {
            Roles = new List<RoleEntity>();
            Publications = new List<ContentEntity>();
            VotedPublicationsId = new HashSet<int>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public List<RoleEntity> Roles { get; set; }
        public List<ContentEntity> Publications { get; set; }
        public HashSet<int> VotedPublicationsId { get; set; }
    }
}