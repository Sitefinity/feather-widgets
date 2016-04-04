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
    public class FeedWidgetFrontend : HtmlElementContainer
    {
        public FeedWidgetFrontend(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets the feed image on frontend
        /// </summary>
        public HtmlSpan FeedImage
        {
            get
            {
                return this.Get<HtmlSpan>("TagName=span", "Class=sf-icon-feed");
            }
        }
    }
}
