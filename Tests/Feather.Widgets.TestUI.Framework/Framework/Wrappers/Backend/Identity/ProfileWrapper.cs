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
        /// Selects profile widget display mode in the widget designer
        /// </summary>
        /// <param name="mode">Profile display mode</param>
        public void SelectDisplayModeWhenChangesAreSaved(string mode)
        {
            HtmlDiv optionsDiv = EM.Identity.ProfileEditScreen.WhenChangesAreSavedDiv 
                .AssertIsPresent("Profile div");

            List<HtmlDiv> profileDivs = optionsDiv.Find.AllByExpression<HtmlDiv>("tagname=div", "class=radio").ToList<HtmlDiv>();

            foreach (var div in profileDivs)
            {
                if (div.InnerText.Contains(mode))
                {
                    HtmlInputRadioButton optionButton = div.Find.ByExpression<HtmlInputRadioButton>("tagname=input", "type=radio");

                    if (optionButton != null && optionButton.IsVisible())
                    {
                        optionButton.Click();
                    }
                }
            }         
        }
    }
}
