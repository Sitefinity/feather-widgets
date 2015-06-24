using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.EmailCampaigns
{
    /// <summary>
    /// Elements from Subscribe form edit screen.
    /// </summary>
    public class SubscribeFormEditScreen : HtmlElementContainer
    {
         /// <summary>
        /// Initializes a new instance of the <see cref="SubscribeFormEditScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public SubscribeFormEditScreen(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Selected existing page
        /// </summary>
        public HtmlInputRadioButton SelectedExistingPage
        {
            get
            {
                return this.Get<HtmlInputRadioButton>("id=specificPage");
            }
        }
    }
}
