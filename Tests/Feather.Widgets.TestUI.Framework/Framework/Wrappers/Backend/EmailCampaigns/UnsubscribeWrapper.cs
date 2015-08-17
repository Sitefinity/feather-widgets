using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.jQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.EmailCampaigns
{
    /// <summary>
    /// This is the entry point class for unsubscribe widget wrapper.
    /// </summary>
    public class UnsubscribeWrapper : BaseWrapper
    {
        /// <summary>
        /// Select email address
        /// </summary>
        public void SelectEmailAddress()
        {
            HtmlInputRadioButton emailAddress = this.EM.EmailCampaigns.UnsubscribeEditScreen.EmailAddress.AssertIsPresent("Email address");
            emailAddress.Click();
            ActiveBrowser.RefreshDomTree();
            ActiveBrowser.WaitUntilReady();
        }

        /// <summary>
        /// Apply css class
        /// </summary>
        /// <param name="cssClassName">css class name</param>
        public void ApplyCssClasses(string cssClassName)
        {
            HtmlAnchor moreOptions = this.EM.Widgets.WidgetDesignerContentScreen.MoreOptionsDiv.AssertIsPresent("More options span");
            moreOptions.Click();
            HtmlInputText cssClassesTextbox = this.EM.EmailCampaigns.UnsubscribeEditScreen.CssClassesTextbox.AssertIsPresent("Css classes textbox");
            cssClassesTextbox.Text = cssClassName;
            cssClassesTextbox.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }
    }
}
