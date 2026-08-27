using Telerik.Sitefinity.DesignerToolbox;
using Telerik.Sitefinity.Frontend.AgenticRAG.Mvc.Controllers;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Frontend.AgenticRAG
{
    internal class AgenticRAGToolboxFilter : IToolboxFilter
    {
        private const string AgenticRAGConnector = "AgenticRAGConnector";

        public bool IsSectionVisible(IToolboxSection section, IToolboxFilterContext context)
        {
            if (section.Name != SitefinityAskBoxController.SectionName)
            {
                return true;
            }

            var ragModule = SystemManager.GetModule(AgenticRAGConnector);

            return ragModule != null;
        }

        public bool IsToolVisible(IToolboxItem tool)
        {
            if (tool.Name != SitefinityAskBoxController.WidgetName && tool.Name != SitefinityAnswerController.WidgetName && tool.Name != SitefinityResultsController.WidgetName)
            {
                return true;
            }

            var ragModule = SystemManager.GetModule(AgenticRAGConnector);

            return ragModule != null;
        }
    }
}
