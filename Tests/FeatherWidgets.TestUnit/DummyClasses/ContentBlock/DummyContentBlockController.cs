using System.Collections.Generic;
using Telerik.Sitefinity.Frontend.ContentBlock.Mvc.Controllers;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace FeatherWidgets.TestUnit.DummyClasses.ContentBlock
{
    /// <summary>
    /// DummyContentBlock Controller
    /// </summary>
    public class DummyContentBlockController : ContentBlockController
    {
        /// <summary>
        /// Initializes the widget commands.
        /// </summary>
        /// <returns>WidgetMenu items</returns>
        public IList<WidgetMenuItem> InitializeCommands()
        {
            return base.InitializeCommands();
        }

        /// <summary>
        /// Gets the current sitemap node.
        /// </summary>
        /// <returns></returns>
        protected override PageSiteNode ResolveCurrentSitemapNode()
        {
            return null;
        }
    }
}
