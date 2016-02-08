using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.ViewModels
{
    
    public class UserViewModel
    {
        public int Id { get; set; }
        [Display(Name = "User name")]
        public string Name { get; set; }
        public List<RoleViewModel> Roles { get; set; }
        public List<ContentViewModel> Publications { get; set; }
        public int AmountOfPublications { get; set; }
        public double? AverageRating { get; set; }

    }
}