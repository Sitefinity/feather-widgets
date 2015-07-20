using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.EmailCampaigns
{
    /// <summary>
    /// Elements from unsubscribe frontend.
    /// </summary>
    public class UnsubscribeFrontend : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnsubscribeFrontend" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public UnsubscribeFrontend(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Unsubscribe form.
        /// </summary>
        public HtmlForm UnsubscribeForm
        {
            get
            {
                return this.Get<HtmlForm>("tagname=form", "name=defaultForm");
            }
        }

        /// <summary>
        /// Gets unsubscribe button.
        /// </summary>
        public HtmlButton UnsubscribeButton
        {
            get
            {
                return this.Get<HtmlButton>("type=submit", "Innertext=Unsubscribe");
            }
        }

        /// <summary>
        /// Gets not existing email.
        /// </summary>
        public HtmlDiv NotExisting
        {
            get
            {
                return this.Get<HtmlDiv>("tagname=div", "class=alert alert-danger");
            }
        }
    }
}
