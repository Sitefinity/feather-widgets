using System;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Framework.Wrappers.Backend.PageTemplates;
using Telerik.Sitefinity.TestUI.Framework.Wrappers.Backend.PageEditor;

namespace FeatherWidgets.TestUI.TestCases.Forms.MultiPageForms
{
    /// <summary>
    /// AddSubmitButtonInHeaderAndFooter test class.
    /// </summary>
    [TestClass]
    public class AddSubmitButtonInHeaderAndFooter_ : FeatherTestCase
    {
        /// <summary>
        /// UI test AddSubmitButtonInHeaderAndFooter
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void AddSubmitButtonInHeaderAndFooter()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifySubmitButtonsCountInFrontEnd(ExpectedSubmitButtonsCount);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().ClickNextButton();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifySubmitButtonsCountInFrontEnd(ExpectedSubmitButtonsCount);
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

        private const string PageName = "FormPage";
        private const int ExpectedSubmitButtonsCount = 2;
    }
}
