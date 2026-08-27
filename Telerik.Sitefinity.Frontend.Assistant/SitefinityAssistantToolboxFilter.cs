using System.Linq;
using Telerik.Sitefinity.Assistant;
using Telerik.Sitefinity.DesignerToolbox;
using Telerik.Sitefinity.Frontend.Assistant.Mvc.Controllers;
using Telerik.Sitefinity.Modules.Libraries.BlobStorage;
using Telerik.Sitefinity.Security.HttpSecurityHeaders;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Frontend.Assistant
{
    internal class SitefinityAssistantToolboxFilter : IToolboxFilter
    {
        public bool IsSectionVisible(IToolboxSection section, IToolboxFilterContext context)
        {
            return true;
        }

        public bool IsToolVisible(IToolboxItem tool)
        {
            if (tool.Name != SitefinityAssistantController.WidgetName)
            {
                return true;
            }

            var modules = SystemManager.ApplicationModules.Values;
            var assistantProviders = modules.Where(x => x is IAssistantProvider && SystemManager.GetModule(x.Name) != null);

            return assistantProviders.Count() > 0;
        }
    }
}
