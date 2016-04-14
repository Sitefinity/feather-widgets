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
    /// ChangeConfirmationMessageOfFormWidget test class.
    /// </summary>
    [TestClass]
    public class ChangeConfirmationMessageOfFormWidget_ : FeatherTestCase
    {
        /// <summary>
        /// UI test ChangeConfirmationMessageOfFormWidget
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void ChangeConfirmationMessageOfFormWidget()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();

            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().SelectSettingsTabInFormWidget();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().CheckUseCustomConfirmationCheckboxInFormWidget();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().SetNewMessageForSuccess(NewConfirmationMessage);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().SetTextboxContent(TextBoxContent);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().SubmitFormWithCustomMessage(NewConfirmationMessage);
            Assert.IsTrue(ActiveBrowser.ContainsText(NewConfirmationMessage), "Text was not found on the page");
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
        private const string NewConfirmationMessage = "Successfully submitted form. Thank you!";
        private const string PageName = "FormPage";
        private const string WidgetName = "TestForm";
        private const string TextBoxContent = "Textbox Field Text";
    }
}
