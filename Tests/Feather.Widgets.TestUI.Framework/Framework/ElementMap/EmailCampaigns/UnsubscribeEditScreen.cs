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
    /// Elements from Unsubscribe edit screen.
    /// </summary>
    public class UnsubscribeEditScreen : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnsubscribeEditScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public UnsubscribeEditScreen(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Selected email address
        /// </summary>
        public HtmlInputRadioButton EmailAddress
        {
            get
            {
                return this.Get<HtmlInputRadioButton>("id=emailAddress");
            }
        }
    }
}
