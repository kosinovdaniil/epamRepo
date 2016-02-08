using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DTO
{
    public class DalComment : IEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DalUser User { get; set; }
        public DateTime Date { get; set; }
        public int ContentId { get; set; }
    }
}
