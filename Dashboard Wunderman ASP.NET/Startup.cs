using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using Umbraco.Web;

namespace Dashboard_Wunderman_ASP.NET
{
    public class Startup : UmbracoDefaultOwinStartup
    {
        public override void Configuration(IAppBuilder app)
        {
            base.Configuration(app);
            ConfigureAuth(app);
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType("ExternalCookie");
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ExternalCookie",
                AuthenticationMode = AuthenticationMode.Passive,
                CookieName = ".AspNet.ExternalCookie",
                ExpireTimeSpan = TimeSpan.FromMinutes(5),
            });

            var options = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "873219085269-4qc60qnh7cv4rujj0ac97ht0ls6k3593.apps.googleusercontent.com",
                ClientSecret = "HlCO9ioHhFhovb81tUT4eUq0",
                Provider = new GoogleOAuth2AuthenticationProvider()
                {
                    OnAuthenticated = context =>
                    {
                        context.Identity.AddClaim(new Claim("urn:token:google", context.AccessToken));

                        return Task.FromResult(true);
                    }
                }
            };

            options.Scope.Add("https://www.googleapis.com/auth/analytics");

            app.UseGoogleAuthentication(options);
        }
    }
}