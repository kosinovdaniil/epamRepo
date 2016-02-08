using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.ViewModels
{
    public class UserLoginModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}