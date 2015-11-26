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
    /// EditFormSaveDraftThenPublishVerifyFrontend_ test class.
    /// </summary>
    [TestClass]
    public class EditFormSaveDraftThenPublishVerifyFrontend_ : FeatherTestCase
    {
        /// <summary>
        /// UI test EditFormSaveDraftThenPublishVerifyFrontend
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void EditFormSaveDraftThenPublishVerifyFrontend()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FeatherGlobals.FormName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().AddField(FeatherGlobals.CheckboxFieldName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().AddField(FeatherGlobals.DropdownFieldName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyFormCheckboxWidgetIsVisible();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyFormDropdownWidgetIsVisible();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ClickSaveDraft();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyPositiveMessageDraftIsShown();
            BAT.Wrappers().Backend().Forms().FormsContentScreen().BackToForms();
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyFormStatus(FeatherGlobals.FormName, FeatherGlobals.draftNewerThanPublished);
            BAT.Macros().NavigateTo().CustomPage("~/" + FeatherGlobals.BootstrapPageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyCheckboxesFieldIsNotVisible();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyDropdownListFieldIsNotVisible();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifySubmitButtonIsVisible();
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().PublishFormFromActionsMenu(FeatherGlobals.FormName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyFormStatus(FeatherGlobals.FormName, FeatherGlobals.published);
            BAT.Macros().NavigateTo().CustomPage("~/" + FeatherGlobals.BootstrapPageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyCheckboxesFieldLabelIsVisible(FeatherGlobals.SelectAChoiceLabelName);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyDropdownListFieldLabelIsVisible(FeatherGlobals.SelectAChoiceLabelName);
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

