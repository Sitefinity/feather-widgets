using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.FeedWidget
{
    /// <summary>
    /// Provides access to Feed widget designer elements.
    /// </summary>
    public class FeedWidgetEditScreen : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeedWidgetEditScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public FeedWidgetEditScreen(Find find)
            : base(find)
        {
        }
    }
}
