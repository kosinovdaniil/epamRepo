using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM
{
    public class Content
    {
        public Content()
        {
            Rating = 0;
        }
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public string UrlToContent { get; set; }
        public string Description { get; set; }       
        public double Rating { get; set; }
        public virtual User User{ get; set; }
        public virtual ICollection<User> VotedUsers { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public DateTime Date { get; set; }
    }
}
