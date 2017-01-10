using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Forms.MultiPageForms
{
    /// <summary>
    /// AddPageBreakToForm_ test class.
    /// </summary>
    [TestClass]
    public class AddPageBreakToForm_ : FeatherTestCase
    {
        /// <summary>
        /// UI test AddPageBreakToForm
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void AddPageBreakToForm()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().AddField(FieldName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyCommonHeaderAndFooterAreVisible();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyDropZonesCount(DropzonesCount);
            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyTextFieldLabelIsVisible(LabelName);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().SetTextboxContent(TextBoxContent);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyNextStepText();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifySubmitButtonIsNotVisible();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().ClickNextButton();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifySubmitButtonIsVisible();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyNextStepText("Next step", false);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().ClickSubmit();
            
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().ViewFormResponses(FormName);
            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyNumberOfResponses(ExpectedResponsesCount);
            BAT.Wrappers().Backend().Forms().FormsResponseScreen().SelectResponse(ResponseNumber);
            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyResponseAuthorUsername(ExpectedAuthorName);
            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyResponseSubmitDate();
            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyResponseTextboxAnswer(TextBoxContent);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyNextStepText();
            BAT.Wrappers().Backend().Forms().FormsResponseScreen().EditResponce();
            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyResponseTextboxAnswer(TextBoxContent);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyNextStepText();
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
        private const string FieldName = "Page break";
        private const string FieldName2 = "Textbox";
        private const string LabelName = "Untitled";
        private const string TextBoxContent = "Textbox Field Text";
        private const string WidgetName = "Form";
        private const int DropzonesCount = 3;
        private const int ExpectedResponsesCount = 1;
        private const int ResponseNumber = 1;
        private const string ExpectedAuthorName = "admin@test.test";
    }
}
