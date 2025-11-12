using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Frontend.Assistant.StringResources
{

    internal class SitefinityAssistantWidgetResources : Resource
    {
        [ResourceEntry("AIAssistant",
            Value = "AI assistant",
            Description = "The title of the AI assistant widget.",
            LastModified = "2025/10/06")]
        public string AIAssistant
        {
            get
            {
                return this["AIAssistant"];
            }
        }

        [ResourceEntry("EmptyWidgetText",
            Value = "Select an AI assistant",
            Description = "EmptyWidgetText",
            LastModified = "2025/10/06")]
        public string EmptyWidgetText
        {
            get
            {
                return this["EmptyWidgetText"];
            }
        }
    }
}
