using System;
using System.Web;
using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.ServiceLocation;
using EPiServer.Shell.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(AlloyTemplates.Startup))]

namespace AlloyTemplates
{
    public class CustomApplicationUISignInManager<T> : ApplicationUISignInManager<T> where T : IdentityUser, IUIUser, new()
    {
        private readonly ServiceAccessor<ApplicationSignInManager<T>> _signInManager;

        public CustomApplicationUISignInManager(ServiceAccessor<ApplicationSignInManager<T>> signInManager) : base(signInManager)
        {
            _signInManager = signInManager;
        }

        public override bool SignIn(string providerName, string userName, string password)
        {
            return _signInManager().SignIn(userName, password, string.Empty);
        }
    }

    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {

            // Add CMS integration for ASP.NET Identity
            app.AddCmsAspNetIdentity<ApplicationUser>();
            app.CreatePerOwinContext<UISignInManager>((options, context) => new CustomApplicationUISignInManager<ApplicationUser>(context.Get<ApplicationSignInManager<ApplicationUser>>));

            // Remove to block registration of administrators
            app.UseAdministratorRegistrationPage(() => HttpContext.Current.Request.IsLocal);

            // Use cookie authentication
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString(Global.LoginPath),
                Provider = new CookieAuthenticationProvider
                {
                    // If the "/util/login.aspx" has been used for login otherwise you don't need it you can remove OnApplyRedirect.
                    OnApplyRedirect = cookieApplyRedirectContext =>
                    {
                        app.CmsOnCookieApplyRedirect(cookieApplyRedirectContext, cookieApplyRedirectContext.OwinContext.Get<ApplicationSignInManager<ApplicationUser>>());
                    },

                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager<ApplicationUser>, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => manager.GenerateUserIdentityAsync(user))
                }
            });
        }
    }
}
