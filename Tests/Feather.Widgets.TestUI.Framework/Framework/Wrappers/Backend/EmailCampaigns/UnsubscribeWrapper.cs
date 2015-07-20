using ArtOfTest.WebAii.Controls.HtmlControls;
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
    }
}
