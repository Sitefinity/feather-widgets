using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;
using ArtOfTest.WebAii.Controls.HtmlControls;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.EmailCampaigns
{
     /// <summary>
    /// Elements from Subscribe form frontend.
    /// </summary>
    public class SubscribeFormFrontend : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscribeFormFrontend" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public SubscribeFormFrontend(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Subscribe form.
        /// </summary>
        public HtmlForm SubscribeForm
        {
            get
            {
                return this.Get<HtmlForm>("tagname=form", "name=defaultForm");
            }
        }
    }
}
