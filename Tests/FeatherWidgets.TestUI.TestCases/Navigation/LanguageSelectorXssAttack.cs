using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Navigation
{
    /// <summary>
    /// This is a test class with tests for language selector xss attack
    /// </summary>
    [TestClass]
    public class LanguageSelectorXssAttack_ : FeatherTestCase
    {
        /// <summary>
        /// UI test LanguageSelectorXssAttack
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam7),
        TestCategory(FeatherTestCategories.PagesAndContent)]
        public void LanguageSelectorXssAttack()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            BAT.Macros().NavigateTo().CustomPage("~/" + PageNameXss1.ToLower(), false, this.Culture);
            BAT.Macros().NavigateTo().CustomPage("~/" + PageNameXss2.ToLower(), false, this.Culture);
            BAT.Macros().NavigateTo().CustomPage("~/" + PageNameXss2.ToLower(), false, this.Culture);
            Assert.IsTrue(ActiveBrowser.Url.Contains(ExpectedUrl));
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string PageName = "LanguagePage";
        private const string PageNameXss1 = "LanguagePage?test=test";
        private const string PageNameXss2 = "LanguagePage?test%27%3E%3Cscript%3Ealert(document.cookie)%3C/script%3E8c02c=1%27%3E";
        private const string ExpectedUrl = "languagepage?test'><script>alert(document.cookie)</script>8c02c=1'>";
    }
}
