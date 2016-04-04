using System;
using System.Collections.Generic;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Framework.Wrappers.Backend.PageTemplates;
using Telerik.Sitefinity.TestUI.Framework.Wrappers.Backend.PageEditor;

namespace FeatherWidgets.TestUI.TestCases.Forms.Navigation
{
    /// <summary>
    /// DeleteNavigationFromForm test class.
    /// </summary>
    [TestClass]
    public class DeleteNavigationFromForm_ : FeatherTestCase
    {
        /// <summary>
        /// UI test DeleteNavigationFromForm
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void DeleteNavigationFromForm()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyNavigationPagesLabels(this.pagesDefaultLabels);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyCommonHeaderAndFooterAreVisible(false);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().DeleteWidget(Navigation);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().DeleteWidget(Navigation, 1);
            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyCommonHeaderAndFooterAreVisible(true);
            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();
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
        private const string Navigation = "NavigationFieldController";
        private const string PageName = "FormPage";
        private List<string> pagesDefaultLabels = new List<string>() { "Step 1", "Step 2" };
    }
}
