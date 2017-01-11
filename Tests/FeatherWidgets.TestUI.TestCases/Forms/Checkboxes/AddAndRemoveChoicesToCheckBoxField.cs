using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Forms.Checkboxes
{
    /// <summary>
    /// AddAndRemoveChoicesToCheckBoxField test class.
    /// </summary>
    [TestClass]
    public class AddAndRemoveChoicesToCheckBoxField_ : FeatherTestCase
    {
        /// <summary>
        /// UI test AddAndRemoveChoicesToCheckBoxField
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void AddAndRemoveChoicesToCheckBoxField()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncOperations();
            ActiveBrowser.RefreshDomTree();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(CheckboxesField);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ChangeLabel(ChoiceText);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ClickToAddNewChoiceLink();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().AddNewChoiceLabel(NewChoice);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ClickToRemoveFirstChoiceLink();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().AddOtherChoice();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            Assert.IsTrue(ActiveBrowser.ContainsText(NewChoice), "Choice not found on the page");
            Assert.IsFalse(ActiveBrowser.ContainsText(Choice), "Choice was not found on the page");
            Assert.IsTrue(ActiveBrowser.ContainsText(OtherChoice), "Choice not found on the page");
            Assert.IsTrue(ActiveBrowser.ContainsText(ChoiceText), "Choice not found on the page");
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().SelectCheckbox(NewChoice);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().SubmitForm();

            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().ViewFormResponses(FormName);

            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyNumberOfResponses(ExpectedResponsesCount1);
            BAT.Wrappers().Backend().Forms().FormsResponseScreen().SelectResponse(ExpectedResponsesCount1);
            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyResponseAuthorUsername(ExpectedAuthorName);
            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyResponseSubmitDate();
            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyResponseMultipleChoiceAnswer(NewChoice);

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().SelectOtherCheckboxButton();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().SetTextToOtherChoiceInCheckBoxField(OtherChoiceText);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().SubmitForm();

            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().ViewFormResponses(FormName, TwoResponses);

            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyNumberOfResponses(ExpectedResponsesCount2);
            BAT.Wrappers().Backend().Forms().FormsResponseScreen().SelectResponse(ExpectedResponsesCount2);
            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyResponseAuthorUsername(ExpectedAuthorName);
            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyResponseSubmitDate();
            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyResponseMultipleChoiceAnswer(OtherChoiceText);
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

        private const string FormName = "NewForm";
        private const string PageName = "FormPage";
        private const string CheckboxesField = "CheckboxesFieldController";
        private const string NewChoice = "Fourth choice";
        private const string Choice = "First Choice";
        private const int ExpectedResponsesCount1 = 1;
        private const int ExpectedResponsesCount2 = 2;
        private const string ExpectedAuthorName = "admin@test.test";
        private const string OtherChoice = "Other...";
        private const string OtherChoiceText = "This is other choice";
        private const string TwoResponses = "2 responses";
        private const string ChoiceText = "Select your choice please";
    }
}