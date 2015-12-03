using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace FeatherWidgets.TestUI.TestCases.Forms
{
    /// <summary>
    /// VerifyPreviewWhileCreatingForm test class.
    /// </summary>
    [TestClass]
    public class VerifyPreviewWhileCreatingForm_ : FeatherTestCase
    {
        /// <summary>
        /// UI test VerifyPreviewWhileCreatingForm
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.Forms)]
        public void VerifyPreviewWhileCreatingForm()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().ClickCreateAFormButton();
            BAT.Wrappers().Backend().Forms().FormsCreateScreen().SetFormName(FeatherGlobals.FormName);
            BAT.Wrappers().Backend().Forms().FormsCreateScreen().ClickCreateAndAddContent();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().AddField(FeatherGlobals.CheckboxFieldName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().AddField(FeatherGlobals.ParagraphTextboxFieldName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().AddField(FeatherGlobals.MultipleChoiceFieldName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().AddField(FeatherGlobals.CapchtaFieldName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().AddField(FeatherGlobals.TexboxFieldName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().AddField(FeatherGlobals.DropdownFieldName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().AddField(FeatherGlobals.FileUploadFieldName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ClickPreviewButtonAndWaitForNewBrowser();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyCheckboxesFieldLabelIsVisible(FeatherGlobals.SelectAChoiceLabelName);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyCheckboxesFieldContainerIsVisible();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyCaptchaFieldContainerIsVisible();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyMultipleChoiceFieldContainerIsVisible();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyMultipleChoiceFieldLabelIsVisible(FeatherGlobals.SelectAChoiceLabelName);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyParagraphFieldContainerIsVisible();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyParagraphTextFieldLabelIsVisible(FeatherGlobals.UntitledLabelName);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifySubmitButtonIsVisible();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyTextboxFieldContainerIsVisible();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyTextFieldLabelIsVisible(FeatherGlobals.UntitledLabelName);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyDropdownListFieldLabelIsVisible(FeatherGlobals.SelectAChoiceLabelName);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyDropdownFieldContainerIsVisible();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyFileUploadFieldContainerIsVisible();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().CloseBrowser();
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }
    }
}