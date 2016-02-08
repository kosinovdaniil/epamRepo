using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.ViewModels
{
    public class ContentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlToContent { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public UserViewModel User { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public DateTime Date { get; set; }
    }
}