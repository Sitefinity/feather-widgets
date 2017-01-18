using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Forms.MultiPageForms
{
    /// <summary>
    /// CreateFormResponseForMultipageForm test class.
    /// </summary>
    [TestClass]
    public class CreateFormResponseForMultipageForm_ : FeatherTestCase
    {
        /// <summary>
        /// UI test CreateFormResponseForMultipageForm
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Forms)]
        public void CreateFormResponseForMultipageForm()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();
            BAT.Wrappers().Backend().Forms().FormsDashboard().ViewFormResponses(FormName, "0 responses");
            BAT.Wrappers().Backend().Forms().FormsResponseScreen().CreateResponce();
            BATFeather.Wrappers().Backend().Forms().FormResponseWrapper().VerifyNavigationPagesLabelsInCreateResponseScreen(this.pagesDefaultLabels);
            BATFeather.Wrappers().Backend().Forms().FormResponseWrapper().VerifyNextStepTextInCreateResponseScreen();
            BATFeather.Wrappers().Backend().Forms().FormResponseWrapper().SetTextboxContentInCreateResponseScreen(TextBoxContent);
            BATFeather.Wrappers().Backend().Forms().FormResponseWrapper().ClickSubmitInCreateResponseScreen();
            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyNumberOfResponses(ExpectedResponsesCount);
            BAT.Wrappers().Backend().Forms().FormsResponseScreen().SelectResponse(ResponseNumber);
            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyResponseAuthorUsername(ExpectedAuthorName);
            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyResponseSubmitDate();
            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyResponseTextboxAnswer(TextBoxContent);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyNavigationPagesLabels(this.pagesDefaultLabels);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyNextStepText();
            BAT.Wrappers().Backend().Forms().FormsResponseScreen().EditResponce();
            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyResponseTextboxAnswer(TextBoxContent);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyNavigationPagesLabels(this.pagesDefaultLabels);
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
        private const string FooterContent = "Footer content";
        private const string HeaderContent = "Header content";
        private const string PageName = "FormPage";
        private const string FieldName = "Page break";
        private const string FieldNameContentBlock = "Content block";
        private const string FieldName2 = "Textbox";
        private const string LabelName = "Untitled";
        private const string TextBoxContent = "Textbox Field Text";
        private const string Footer = "Footer";
        private const string Header = "Header";
        private const int ExpectedResponsesCount = 1;
        private const int ResponseNumber = 1;
        private const string ExpectedAuthorName = "admin";
        private const string Choice = "Second Choice";
        private List<string> pagesDefaultLabels = new List<string>() { "Step 1", "Step 2" };
    }
}
