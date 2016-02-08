
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class ContentEntity
    {
        public ContentEntity()
        {
            Rating = 0;
            Date = DateTime.Now;
            Comments = new List<CommentEntity>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlToContent { get; set; }
        public string Description { get; set; }
        public double? Rating { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public ICollection<CommentEntity> Comments { get; set; }
    }
}
