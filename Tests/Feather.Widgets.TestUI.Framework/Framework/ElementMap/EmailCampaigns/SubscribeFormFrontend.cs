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

        /// <summary>
        /// Email field.
        /// </summary>
        public HtmlInputEmail EmailAddressField
        {
            get
            {
                return this.Get<HtmlInputEmail>("tagname=input", "id=Email");
            }
        }

        /// <summary>
        /// Gets subscribe button.
        /// </summary>
        public HtmlButton SubscribeButton
        {
            get
            {
                return this.Get<HtmlButton>("type=submit", "Innertext=Subscribe");
            }
        }

        /// <summary>
        /// Subscribe div.
        /// </summary>
        public HtmlDiv SubscribeDiv
        {
            get
            {
                return this.Get<HtmlDiv>("tagname=div", "id=defaultForm");
            }
        }

        /// <summary>
        /// Email field.
        /// </summary>
        public HtmlInputText EmailAddressTextField
        {
            get
            {
                return this.Get<HtmlInputText>("tagname=input", "id=Email");
            }
        }

        /// <summary>
        /// Gets subscribe button.
        /// </summary>
        public HtmlInputSubmit SubscribeInputButton
        {
            get
            {
                return this.Get<HtmlInputSubmit>("type=submit", "value=Subscribe");
            }
        }
    }
}
