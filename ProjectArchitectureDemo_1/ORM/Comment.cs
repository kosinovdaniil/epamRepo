using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Text { get; set; }
        public virtual User User { get; set; }
        public virtual Content Content { get; set; }

        public DateTime Date { get; set; }
    }
}
