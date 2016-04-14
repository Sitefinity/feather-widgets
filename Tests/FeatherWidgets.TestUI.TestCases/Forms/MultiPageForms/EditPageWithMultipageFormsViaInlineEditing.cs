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
    /// EditPageWithMultipageFormsViaInlineEditing_ test class.
    /// </summary>
    [TestClass]
    public class EditPageWithMultipageFormsViaInlineEditing_ : FeatherTestCase
    {
        /// <summary>
        /// UI test EditPageWithMultipageFormsViaInlineEditing 
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void EditPageWithMultipageFormsViaInlineEditing()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            BAT.Wrappers().Frontend().InlineEditing().InlineEditingWrapper().OpenPageForEdit();
            BAT.Wrappers().Frontend().InlineEditing().InlineEditingWrapper().VerifyEditIsOn(PageName);
            BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().WaitForContentBlockForEdit();
            BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().EditContentBlock();
            BAT.Wrappers().Frontend().InlineEditing().InlineEditingWrapper().PublishPage();
            BAT.Wrappers().Frontend().InlineEditing().InlineEditingWrapper().VerifyEditIsOff();
            BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().VerifyContentOfContentBlockOnThePageFrontend(EditedContentBlockContent);

            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().ClickNextButton();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().SelectRadioButton(Choice);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().ClickNextButton();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().SelectCheckbox(Choice);
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

        private const string PageName = "FormPage";
        private const string ContentBlockContent = "Test content";
        private const string EditedContentBlockContent = "edited content block";
        private const int ExpectedResponsesCount = 1;
        private const string FormName = "MultiPageForm";
        private const string Choice = "Second Choice";
    }
}
