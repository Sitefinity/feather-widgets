using System;
using System.Collections.Generic;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Framework.Wrappers.Backend.PageTemplates;
using Telerik.Sitefinity.TestUI.Framework.Wrappers.Backend.PageEditor;

namespace FeatherWidgets.TestUI.TestCases.Forms.Navigation
{
    /// <summary>
    /// VerifyCheckedStatusInNavigation test class.
    /// </summary>
    [TestClass]
    public class VerifyCheckedStatusInNavigation_ : FeatherTestCase
    {
        /// <summary>
        /// UI test VerifyCheckedStatusInNavigation
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void VerifyCheckedStatusInNavigation()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(PageBreak, 1);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ClickAllowUsersToStepBackwardCheckBox();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyNavigationPagesLabels(this.pagesDefaultLabels);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyVisibleNumbersInNavigation(3);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().ClickNextButton();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyVisibleNumbersInNavigation(2);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().ClickPreviousButton();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyVisibleNumbersInNavigation(3);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().ClickNextButton();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyVisibleNumbersInNavigation(2);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().ClickNextButton();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyVisibleNumbersInNavigation(1);
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

        private const string FormName = "MultiPageForm";
        private const string PageName = "FormPage";
        private const string PageBreak = "PageBreakController";
        private List<string> pagesDefaultLabels = new List<string>() { "Step 1", "Step 2", "Step 3" };
    }
}
