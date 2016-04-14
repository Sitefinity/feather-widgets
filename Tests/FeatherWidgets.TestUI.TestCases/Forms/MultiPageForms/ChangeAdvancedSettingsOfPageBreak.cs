using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.WebAii.Controls.Html;

namespace FeatherWidgets.TestUI.TestCases.Forms.MultiPageForms
{
    /// <summary>
    /// ChangeAdvancedSettingsOfPageBreak test class.
    /// </summary>
    [TestClass]
    public class ChangeAdvancedSettingsOfPageBreak_ : FeatherTestCase
    {
        /// <summary>
        /// UI test ChangeAdvancedSettingsOfPageBreak
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void ChangeAdvancedSettingsOfPageBreak()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().WaitForFieldContent(NextStepOld, PageBreak);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyNextStepText(NextStepOld);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(PageBreak);
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncOperations();
            ActiveBrowser.RefreshDomTree();
            BATFeather.Wrappers().Backend().ModuleBuilder().DynamicWidgetAdvancedSettingsWrapper().ClickAdvancedSettingsButton();
            BATFeather.Wrappers().Backend().ModuleBuilder().DynamicWidgetAdvancedSettingsWrapper().ClickModelButton();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ChangeNextStepTextInAdvancedSettings(NextStepNew);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().WaitForFieldContent(NextStepNew, PageBreak);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyNextStepText(NextStepNew);
            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyNextStepText(NextStepNew);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().SetTextboxContent(TextBoxContent);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().ClickNextButton();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().SelectRadioButton(Choice);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().ClickSubmit();
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
        private const string PageBreak = "PageBreakController";
        private const string CssClassesToApply = "Css to apply";
        private const string PageName = "FormPage";
        private const string NextStepOld = "Next step";
        private const string NextStepNew = "Next page";
        private const string TextBoxContent = "Textbox Field Text";
        private const string Choice = "Second Choice";
    }
}
