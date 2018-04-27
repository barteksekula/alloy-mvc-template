using AlloyTemplates.Models.Pages;
using AlloyTemplates.Models.ViewModels;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace AlloyTemplates.Models
{
    [SiteContentType(GroupName = Global.GroupNames.Specialized, GUID = "CCCCADF2-3E89-4117-ADEB-F8D43565D2A8")]
    [SiteImageUrl(Global.StaticGraphicsFolderPath + "page-type-thumbnail.png")]
    [AvailableContentTypes(Availability.Specific, IncludeOn = new[] {typeof(StartPage)})]
    public class LoginPage : SitePageData
    {
    }

    public class LoginModel : PageViewModel<LoginPage>
    {
        public LoginFormPostbackData LoginPostbackData { get; set; } = new LoginFormPostbackData();

        public LoginModel(LoginPage currentPage)
            : base(currentPage)
        {
        }

        public string Message { get; set; }
    }

    public class LoginFormPostbackData
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
