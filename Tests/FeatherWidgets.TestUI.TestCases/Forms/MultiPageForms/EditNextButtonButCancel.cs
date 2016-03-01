using System;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Framework.Wrappers.Backend.PageTemplates;
using Telerik.Sitefinity.TestUI.Framework.Wrappers.Backend.PageEditor;

namespace FeatherWidgets.TestUI.TestCases.Forms.MultiPageForms
{
    /// <summary>
    /// EditNextButtonButCancel test class.
    /// </summary>
    [TestClass]
    public class EditNextButtonButCancel_ : FeatherTestCase
    {
        /// <summary>
        /// UI test EditNextButtonButCancel
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void EditNextButtonButCancel()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyNextStepText(NextStepOld);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(PageBreak);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ChangeNextStepText(NextStepNew);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().SelectCancel();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyNextStepText(NextStepOld);
            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyNextStepText(NextStepOld);
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
        private const string PageBreak = "PageBreakController";
        private const string NextStepOld = "Next step";
        private const string NextStepNew = "Next page";
        private const string PageName = "FormPage";
    }
}
