using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Telerik.TestUI.Core.Utilities;

namespace FeatherWidgets.TestUI.TestCases.Forms.Forms
{
    /// <summary>
    /// DeleteFormInUseVerifyFrontend_ test class.
    /// </summary>
    [TestClass]
    public class DeleteFormInUseVerifyFrontend_ : FeatherTestCase
    {
        /// <summary>
        /// UI test DeleteFormInUseVerifyFrontend
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms),
        Telerik.TestUI.Core.Attributes.KnownIssue(BugId = 212789), Ignore]
        public void DeleteFormInUseVerifyFrontend()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().CustomPage("~/" + FeatherGlobals.BootstrapPageName.ToLower(), true, this.Culture, new HtmlFindExpression("TagName=button")));
            
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.RefreshDomTree();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyCheckboxesFieldLabelIsVisible(FeatherGlobals.SelectAChoiceLabelName);
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().DeleteFormFromActionsMenu(FeatherGlobals.FormName);
            bool formIsPresent = BAT.Wrappers().Backend().Forms().FormsDashboard().IsFormPresentInGridView(FeatherGlobals.FormName);
            Assert.IsTrue(!formIsPresent);
            BAT.Macros().NavigateTo().CustomPage("~/" + FeatherGlobals.BootstrapPageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyMessageIsDisplayedAfterFormIsDeleted();
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
    }
}