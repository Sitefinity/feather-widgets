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
    /// AddFieldsToHeaderAndFooter_ test class.
    /// </summary>
    [TestClass]
    public class AddFieldsToHeaderAndFooter_ : FeatherTestCase
    {
        /// <summary>
        /// UI test AddFieldsToHeaderAndFooter
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void AddFieldsToHeaderAndFooter()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().AddField(FieldName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyCommonHeaderAndFooterAreVisible();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().AddField(FieldNameContentBlock, Footer);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(FieldNameContentBlock);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().FillContentToContentBlockWidget(FooterContent);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().AddField(FieldNameContentBlock, Header);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(FieldNameContentBlock);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().FillContentToContentBlockWidget(HeaderContent);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();            
            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyTextFieldLabelIsVisible(LabelName);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyContentBlockFieldTextIsVisible(FooterContent);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyContentBlockFieldTextIsVisible(HeaderContent);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().SetTextboxContent(TextBoxContent);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyNextStepText();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().ClickNextButton();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifySubmitButtonIsVisible();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyContentBlockFieldTextIsVisible(FooterContent);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyContentBlockFieldTextIsVisible(HeaderContent);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().ClickSubmit();

            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().ViewFormResponses(FormName);
            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyNumberOfResponses(ExpectedResponsesCount);
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
    }
}
