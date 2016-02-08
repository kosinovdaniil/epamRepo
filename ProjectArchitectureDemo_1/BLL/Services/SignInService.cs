using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Host.SystemWeb;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Services;
using BLL.Interface.Entities;

namespace BLL.Services
{
    public class SignInService : ISignInService
    {
        public void IdentitySignin(UserEntity user, bool isPersistent = false)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.Name));
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            AuthenticationManager.SignIn(new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = isPersistent,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            }, identity);
        }

        public void IdentitySignout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                                          DefaultAuthenticationTypes.ExternalCookie);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContextExtensions.GetOwinContext(HttpContext.Current).Authentication;
            }
        }
    }
}
