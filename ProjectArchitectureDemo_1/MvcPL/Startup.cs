using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Owin.Security.Providers.GitHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;

namespace MvcPL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/LogOn")
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // App.Secrets is application specific and holds values in CodePasteKeys.json
            // Values are NOT included in repro – auto-created on first load
            /*if (!string.IsNullOrEmpty(App.Secrets.GoogleClientId))
            {
                app.UseTwoFactorSignInCookie.UseGoogleAuthentication(
                    clientId: App.Secrets.GoogleClientId,
                    clientSecret: App.Secrets.GoogleClientSecret);
            }

            if (!string.IsNullOrEmpty(App.Secrets.TwitterConsumerKey))
            {
                app.UseTwitterAuthentication(
                    consumerKey: App.Secrets.TwitterConsumerKey,
                    consumerSecret: App.Secrets.TwitterConsumerSecret);
            }
            
            if (!string.IsNullOrEmpty(App.Secrets.GitHubClientId))
            {
                app.UseGitHubAuthentication(
                    clientId: App.Secrets.GitHubClientId,
                    clientSecret: App.Secrets.GitHubClientSecret);
            }*/

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }
    }
}