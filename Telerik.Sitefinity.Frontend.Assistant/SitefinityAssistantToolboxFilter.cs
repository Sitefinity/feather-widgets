using Telerik.Sitefinity.DesignerToolbox;
using Telerik.Sitefinity.Frontend.Assistant.Mvc.Controllers;
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

            var module = SystemManager.GetModule("SitefinityAssistant");
            bool hasAssistantModule = module != null;

            if (!hasAssistantModule)
            {
                return false;
            }

            using (var config = new SitefinityAssistantConfigAccessor())
            {
                bool isFeatureEnabled = config.FeatureState == "enabled";
                return isFeatureEnabled;
            }
        }
    }
}
