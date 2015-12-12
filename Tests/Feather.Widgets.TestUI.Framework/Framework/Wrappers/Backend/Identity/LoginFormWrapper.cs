using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Identity
{
    /// <summary>
    /// This is the entry point class for login form widget wrapper.
    /// </summary>
    public class LoginFormWrapper : BaseWrapper
    {
        /// <summary>
        /// Clicks the select button in redirect to page section.
        /// </summary>
        public void ClickSelectButtonForRedirectToPage()
        {
            var button = EM.Identity.LoginFormEditScreen.RedirectToPageSelectButton.AssertIsPresent("Select button");
            
            button.Click();

            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncOperations();
        }

        /// <summary>
        /// Clicks the select button in registration page section.
        /// </summary>
        public void ClickSelectButtonForRegistrationPage()
        {
            Manager.Current.Wait.For(() =>
            {
                ActiveBrowser.RefreshDomTree();
                return EM.Identity.LoginFormEditScreen.RegistrationPageSelectButton != null;
            }, 10000);

            var button = EM.Identity.LoginFormEditScreen.RegistrationPageSelectButton.AssertIsPresent("Select button");

            button.Click();

            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncOperations();
        }
    }
}
