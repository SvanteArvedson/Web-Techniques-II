using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weather.App
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Konto/Inloggning")
            });
        }
    }
}