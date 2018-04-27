using System.Web.Mvc;
using AlloyTemplates.Models;
using System.Web.Http;
using EPiServer.ServiceLocation;
using EPiServer.Shell.Security;

namespace AlloyTemplates.Controllers
{
    public class LoginPageController : PageControllerBase<LoginPage>
    {
        protected Injected<UIUserProvider> UserProvider;

        public ActionResult Index(LoginPage currentPage, [FromUri] string returnUrl)
        {
            return View(new LoginModel(currentPage) {LoginPostbackData = {ReturnUrl = returnUrl}});
        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Post(LoginPage currentPage, [FromBody] LoginFormPostbackData loginPostBackData)
        {
            var model = new LoginModel(currentPage);

            if (!UISignInManager.Service.SignIn(UserProvider.Service.Name, loginPostBackData.Username,
                loginPostBackData.Password))
            {
                model.Message = "Wrong credentials, try again";
                return View("Index", model);
            }

            return Redirect(loginPostBackData.ReturnUrl);
        }
    }
}
