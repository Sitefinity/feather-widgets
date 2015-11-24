using System;
using System.Collections.Generic;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Framework.Wrappers.Backend.PageTemplates;
using Telerik.Sitefinity.TestUI.Framework.Wrappers.Backend.PageEditor;

namespace FeatherWidgets.TestUI.TestCases.Forms.Navigation
{
    /// <summary>
    /// EditNavigationLabels test class.
    /// </summary>
    [TestClass]
    public class EditNavigationLabels_ : FeatherTestCase
    {
        /// <summary>
        /// UI test EditNavigationLabels
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void EditNavigationLabels()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyNavigationPagesLabels(this.pagesDefaultLabels);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(Navigation);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ChangePageLabel(this.pagesNewLabels);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyNavigationPagesLabels(this.pagesNewLabels);
            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();
            
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyNavigationPagesLabels(this.pagesNewLabels);
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
        private List<string> pagesDefaultLabels = new List<string>() { "Page1", "Page2" };
        private List<string> pagesNewLabels = new List<string>() { "Test 1", "Test 2" };
    }
}
