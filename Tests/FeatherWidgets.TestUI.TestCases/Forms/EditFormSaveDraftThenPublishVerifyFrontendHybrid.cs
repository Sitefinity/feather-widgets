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
    /// EditFormSaveDraftThenPublishVerifyFrontendHybrid_ test class.
    /// </summary>
    [TestClass]
    public class EditFormSaveDraftThenPublishVerifyFrontendHybrid_ : FeatherTestCase
    {
        /// <summary>
        /// UI test EditFormSaveDraftThenPublishVerifyFrontendHybrid
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Forms),
        Telerik.TestUI.Core.Attributes.KnownIssue(BugId = 144860), Ignore]
        public void EditFormSaveDraftThenPublishVerifyFrontendHybrid()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FeatherGlobals.FormName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().AddField(FeatherGlobals.CapchtaFieldName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().AddField(FeatherGlobals.ParagraphTextboxFieldName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().AddField(FeatherGlobals.MultipleChoiceFieldName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyFormMultipleChoiceFieldIsVisible();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyFormParagraphTextFieldIsVisible();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyFormCheckboxWidgetIsVisible();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyFormCaptchaFieldIsVisible();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ClickSaveDraft();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyPositiveMessageDraftIsShown();
            BAT.Wrappers().Backend().Forms().FormsContentScreen().BackToForms();
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyFormStatus(FeatherGlobals.FormName, FeatherGlobals.draftNewerThanPublished);
            BAT.Macros().NavigateTo().CustomPage("~/" + FeatherGlobals.HybridPageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyParagraphTextFieldIsNotVisible();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyMultipleChoiceFieldIsNotVisible();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyCaptchaFieldIsNotVisible();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyCheckboxesFieldLabelIsVisible(FeatherGlobals.SelectAChoiceLabelName);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifySubmitButtonIsVisible();
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().PublishFormFromActionsMenu(FeatherGlobals.FormName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyFormStatus(FeatherGlobals.FormName, FeatherGlobals.published);
            BAT.Macros().NavigateTo().CustomPage("~/" + FeatherGlobals.HybridPageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyParagraphTextFieldLabelIsVisible(FeatherGlobals.UntitledLabelName);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyParagraphFieldContainerIsVisible();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyMultipleChoiceFieldLabelIsVisible(FeatherGlobals.SelectAChoiceLabelName);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyMultipleChoiceFieldContainerIsVisible();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyCaptchaFieldContainerIsVisible();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyCheckboxesFieldLabelIsVisible(FeatherGlobals.SelectAChoiceLabelName);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifySubmitButtonIsVisible();
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
    }
}   

