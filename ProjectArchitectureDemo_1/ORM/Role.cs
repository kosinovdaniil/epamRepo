using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ORM
{
    public partial class Role
    {
       /* public Role()
        {
            Users = new HashSet<User>();
        }*/


        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }

    }
}
