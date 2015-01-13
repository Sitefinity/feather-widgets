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
            HtmlAnchor createButton = this.EM.WidgetTemplates.WidgetTemplatesGrid.CreateTemplateButton
                                    .AssertIsPresent("create button");

            createButton.Click();
            ActiveBrowser.WaitForAsyncRequests();
            ActiveBrowser.RefreshDomTree();
        }
    }
}
