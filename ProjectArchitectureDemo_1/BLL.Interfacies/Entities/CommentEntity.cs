using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class CommentEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public UserEntity User { get; set; }
        public DateTime Date { get; set; }
        public int ContentId { get; set; }
    }
}
