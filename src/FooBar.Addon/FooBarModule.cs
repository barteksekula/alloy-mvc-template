using EPiServer.Framework.Web.Resources;
using EPiServer.Shell.Modules;

namespace FooBar.Addon
{
    public class FooBarModule : ShellModule
    {
        public FooBarModule(string name, string routeBasePath, string resourceBasePath)
            : base(name, routeBasePath, resourceBasePath)
        {
        }

        public override ModuleViewModel CreateViewModel(ModuleTable moduleTable, IClientResourceService clientResourceService)
        {
            return new ModuleViewModel(this, clientResourceService);
        }
    }
}
