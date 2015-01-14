using Feather.Widgets.TestUI.Framework.Framework.ElementMap.WidgetTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.WidgetTemplates
{
    /// <summary>
    /// Frame wrapper for CreateTemplateScreen screen
    /// </summary>
    public class WidgetTemplatesCreateScreenFrameWrapper : WidgetTemplatesCreateEditScreenWrapper<WidgetTemplatesCreateScreenFrame>
    {
        /// <summary>
        /// Gets the element map.
        /// </summary>
        /// <value>The element map.</value>
        protected override WidgetTemplatesCreateScreenFrame ActiveWindowEM
        {
            get
            {
                return new WidgetTemplatesCreateScreenFrame(ActiveBrowser.Find);
            }
        }
    }
}
