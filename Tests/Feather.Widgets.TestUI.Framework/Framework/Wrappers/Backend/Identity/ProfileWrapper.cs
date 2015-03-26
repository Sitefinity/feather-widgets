using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.jQuery;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Identity
{
    /// <summary>
    /// This is the entry point class for profile widget wrapper.
    /// </summary>
    public class ProfileWrapper : BaseWrapper
    {
        /// <summary>
        /// Switch to Read mode only
        /// </summary>
        public void SwitchToReadMode()
        {
            HtmlInputRadioButton readMode = EM.Identity.ProfileEditScreen.ReadModeOnly
            .AssertIsPresent("Read mode only button");
            readMode.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncJQueryRequests();
        }

        /// <summary>
        /// Switch to Both mode only
        /// </summary>
        public void SwitchToBothMode()
        {
            HtmlInputRadioButton bothMode = EM.Identity.ProfileEditScreen.BothReadModeCanBeEditedMode
            .AssertIsPresent("Both mode only button");
            bothMode.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncJQueryRequests();
        }

        /// <summary>
        /// Select specially prepared page
        /// </summary>
        public void SelectSpeciallyPreparedPage()
        {
            HtmlInputRadioButton speciallyPreparedPage = EM.Identity.ProfileEditScreen.OpenSpeciallyPreparedPage
            .AssertIsPresent("Specially prepared page button");
            speciallyPreparedPage.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncJQueryRequests();
        }
    }
}
