using System;
using System.Collections.Generic;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Framework.Wrappers.Backend.PageTemplates;
using Telerik.Sitefinity.TestUI.Framework.Wrappers.Backend.PageEditor;

namespace FeatherWidgets.TestUI.TestCases.Forms.Navigation
{
    /// <summary>
    /// VerifyProgressBarTemplate test class.
    /// </summary>
    [TestClass]
    public class VerifyProgressBarTemplate_ : FeatherTestCase
    {
        /// <summary>
        /// UI test VerifyProgressBarTemplate
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void VerifyProgressBarTemplate()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(Navigation);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().MoreOptions();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().SelectNavigationTemplate(TemplateName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(PageBreak, 1);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ClickAllowUsersToStepBackwardCheckBox();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();
            
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyPercentageForTheProgressBar("0%");
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().ClickNextButton();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyPercentageForTheProgressBar("33%");
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().ClickPreviousButton();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyPercentageForTheProgressBar("0%");
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().ClickNextButton();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().ClickNextButton();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyPercentageForTheProgressBar("67%");
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
        private const string Navigation = "NavigationFieldController";
        private const string TemplateName = "ProgressBar";
    }
}
