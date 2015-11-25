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
    /// PreviewMultipageForm_ test class.
    /// </summary>
    [TestClass]
    public class PreviewMultipageForm_ : FeatherTestCase
    {
        /// <summary>
        /// UI test PreviewMultipageForm
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void PreviewMultipageForm()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(TextField);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ChangeTexboxLabel(FormName1TextBox);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(CheckboxField);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ChangeLabel(FormName1Checkbox);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ClickPreviewButton();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyIfFieldExistInPreviewMode(Page1, true);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyIfFieldExistInPreviewMode(Page2, true);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyIfFieldExistInPreviewMode(FormName1TextBox, true);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyIfFieldExistInPreviewMode(FormName1Checkbox, false);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyIfFieldExistInPreviewMode(NextStep, true);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyIfFieldExistInPreviewMode(Submit, false);
            ActiveBrowser.Close();
            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();

            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BAT.Wrappers().Backend().Pages().PagesWrapper().PreviewPage(PageName, true);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyIfFieldExistInPreviewMode(Page1, true);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyIfFieldExistInPreviewMode(Page2, true);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyIfFieldExistInPreviewMode(FormName1TextBox, true);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyIfFieldExistInPreviewMode(FormName1Checkbox, false);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyIfFieldExistInPreviewMode(NextStep, true);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyIfFieldExistInPreviewMode(Submit, false);
            ActiveBrowser.Close();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
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
        private const string FormName1TextBox = "MultiPageForm1TextBox";
        private const string FormName1Checkbox = "MultiPageForm1Checkbox";
        private const string TextField = "TextFieldController";
        private const string CheckboxField = "CheckboxesFieldController";
        private const string NextStep = "Next step";
        private const string Submit = "Submit";
        private const string Page1 = "Page1";
        private const string Page2 = "Page2";
    }
}
