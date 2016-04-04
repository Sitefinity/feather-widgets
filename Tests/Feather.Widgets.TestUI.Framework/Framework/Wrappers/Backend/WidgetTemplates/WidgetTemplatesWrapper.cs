using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    /// <summary>
    /// This is the entry point class for Widget Templates Wrapper.
    /// </summary>
    public class WidgetTemplatesWrapper : BaseWrapper
    {
        /// <summary>
        /// Creates the template.
        /// </summary>
        public void CreateTemplate()
        {
            HtmlAnchor createButton = this.EM.WidgetTemplates.WidgetTemplatesGrid.CreateTemplateButton;

            if (createButton == null)
            {
                createButton = ActiveBrowser.Find.ByExpression<HtmlAnchor>("tagname=a", "id = ctl04_controlTemplatesBackendList_ctl00_ctl00_noItemsExistScreen_ctl00_ctl00_actionsRepeater_actionLink_0");
            }

            createButton.Click();
            ActiveBrowser.WaitForAsyncRequests();
            ActiveBrowser.RefreshDomTree();
        }
    }
}
