using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public UserViewModel User { get; set; }
        public int ContentId { get; set; }
        public DateTime Date { get; set; }
    }
}