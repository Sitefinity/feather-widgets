using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Forms.Textbox
{
    /// <summary>
    /// SetRequiredTextboxField test class.
    /// </summary>
    [TestClass]
    public class SetRequiredTextboxField_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SetRequiredTextboxField
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void SetRequiredTextboxField()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().ClickCreateAFormButton();
            BAT.Wrappers().Backend().Forms().FormsCreateScreen().SetFormName(FormName);
            BAT.Wrappers().Backend().Forms().FormsCreateScreen().ClickCreateAndAddContent();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncOperations();
            ActiveBrowser.RefreshDomTree();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().AddField(FieldName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(FieldName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().CheckRequiredFieldCheckbox();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ChangeRequiredMessage(NewRequiredMessage);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();

            BAT.Macros().NavigateTo().Pages(this.Culture);

            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncOperations();
            ActiveBrowser.RefreshDomTree();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInFormWidgetSelector(FormName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().ClickSubmit();
            Assert.IsTrue(ActiveBrowser.ContainsText(NewRequiredMessage), "Text was not found on the page");
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyPageUrl(PageName);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().SetTextboxContent(TextBoxContent);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().SubmitForm();

            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().ViewFormResponses(FormName);

            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyNumberOfResponses(ExpectedResponsesCount);
            BAT.Wrappers().Backend().Forms().FormsResponseScreen().SelectResponse(ResponseNumber);
            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyResponseAuthorUsername(ExpectedAuthorName);
            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyResponseSubmitDate();
            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyResponseTextboxAnswer(TextBoxContent);
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

        private const string FormName = "MvcForm";
        private const string PageName = "FormPage";
        private const string WidgetName = "Form";
        private const string FieldName = "Textbox";
        private const string LabelName = "Untitled";
        private const string TextBoxContent = "Textbox Field Text";
        private const int ExpectedResponsesCount = 1;
        private const int ResponseNumber = 1;
        private const string ExpectedAuthorName = "admin";
        private const string NewRequiredMessage = "This is required field";
    }
}