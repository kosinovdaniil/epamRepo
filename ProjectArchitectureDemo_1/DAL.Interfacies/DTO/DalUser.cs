using System.Collections.Generic;

namespace DAL.Interface.DTO
{
    public class DalUser : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public ICollection<DalRole> Roles{ get; set; }
        public ICollection<DalContent> Publications { get; set; }
        public HashSet<int> VotedPublicationsId { get; set; }
    }
}