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
    /// TwoMultiPageFormsOnTheSamePage_ test class.
    /// </summary>
    [TestClass]
    public class TwoMultiPageFormsOnTheSamePage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test TwoMultiPageFormsOnTheSamePage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void TwoMultiPageFormsOnTheSamePage()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName1);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(TextField);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ChangeTexboxLabel(FormName1TextBox);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(CheckboxField);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ChangeLabel(FormName1Checkbox);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();

            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName2);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(TextField);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ChangeTexboxLabel(FormName2TextBox);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(CheckboxField);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ChangeLabel(FormName2Checkbox);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().ClickNextButton();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyMultiPageFormFieldOnForntend(fieldsLabel);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().SelectCheckbox(Choice);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().ClickSubmit();

            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().ViewFormResponses(FormName1);
            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyNumberOfResponses(ExpectedResponsesCountForm1);
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

        private const string FormName1 = "MultiPageForm1";
        private const string FormName1TextBox = "MultiPageForm1TextBox";
        private const string FormName1Checkbox = "MultiPageForm1Checkbox";
        private const string FormName2 = "MultiPageForm2";
        private const string FormName2TextBox = "MultiPageForm2TextBox";
        private const string FormName2Checkbox = "MultiPageForm2Checkbox";
        private const string TextField = "TextFieldController";
        private const string CheckboxField = "CheckboxesFieldController";
        private const string PreviousStepOld = "Previous step";
        private const string PreviousStepNew = "Previous page";
        private const string Choice = "Second Choice";
        private const string PageName = "FormPage";
        private const int ExpectedResponsesCountForm1 = 1;
        private const int ExpectedResponsesCountForm2 = 2;
        private string[] fieldsLabel = { "MultiPageForm1Checkbox", "MultiPageForm2TextBox" };
    }
}
