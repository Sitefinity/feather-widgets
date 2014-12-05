using System.Collections.Generic;
using Telerik.Sitefinity.Frontend.ContentBlock.Mvc.Controllers;
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
    }
}
