using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Forms.Forms
{
    /// <summary>
    /// SubmitNonAjaxFormOnPageWithTwoFormsOneOfThemUseAjax test class.
    /// </summary>
    [TestClass]
    public class SubmitNonAjaxFormOnPageWithTwoFormsOneOfThemUseAjax_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SubmitNonAjaxFormOnPageWithTwoFormsOneOfThemUseAjax
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms), Ignore]
        public void SubmitNonAjaxFormOnPageWithTwoFormsOneOfThemUseAjax()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName2);
            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();

            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(FormName2);
            BATFeather.Wrappers().Backend().ModuleBuilder().DynamicWidgetAdvancedSettingsWrapper().ClickAdvancedSettingsButton();
            BATFeather.Wrappers().Backend().ModuleBuilder().DynamicWidgetAdvancedSettingsWrapper().ClickModelButton();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ChangeUseAjaxSubmitInAdvancedSettings(IsAjaxSet);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().SetTextboxContent(TextBoxContent);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().ClickSubmit();

            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().ViewFormResponses(FormName);
            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyNumberOfResponses(ExpectedResponsesCount);
            BAT.Wrappers().Backend().Forms().FormsResponseScreen().SelectResponse(ResponseNumber);
            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyResponseAuthorUsername(ExpectedAuthorName);
            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyResponseSubmitDate();
            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyResponseTextboxAnswer(TextBoxContent);
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().ViewFormResponses(FormName2, "0 responses");
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

        private const string FormName = "NewForm1";
        private const string FormName2 = "NewForm2";
        private const string PageName = "FormPage";
        private const string IsAjaxSet = "True";
        private const string WidgetName = "TestForm";
        private const string TextBoxContent = "Textbox Field Text";
        private const int ExpectedResponsesCount = 1;
        private const int ResponseNumber = 1;
        private const string ExpectedAuthorName = "admin";
    }
}
