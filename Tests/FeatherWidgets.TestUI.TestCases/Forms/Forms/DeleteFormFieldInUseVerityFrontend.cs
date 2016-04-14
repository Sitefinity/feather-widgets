using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArtOfTest.WebAii.Core;
using Telerik.TestUI.Core.Utilities;

namespace FeatherWidgets.TestUI.TestCases.Forms.Forms
{
    /// <summary>
    /// DeleteFormFieldInUseVerityFrontend_ test class.
    /// </summary>
    [TestClass]
    public class DeleteFormFieldInUseVerityFrontend_ : FeatherTestCase
    {
        /// <summary>
        /// UI test DeleteFormFieldInUseVerityFrontend
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void DeleteFormFieldInUseVerityFrontend()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().CustomPage("~/" + FeatherGlobals.BootstrapPageName.ToLower(), true, this.Culture, new HtmlFindExpression("id=PublicWrapper")));
            
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.RefreshDomTree();
			
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyCheckboxesFieldLabelIsVisible(FeatherGlobals.SelectAChoiceLabelName);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyDropdownListFieldLabelIsVisible(FeatherGlobals.SelectAChoiceLabelName);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifySubmitButtonIsVisible();
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FeatherGlobals.FormName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ClickOnWidgetMenuItem(FeatherGlobals.CheckboxControlName, FeatherGlobals.DeleteWidgetMenuOption);
            ActiveBrowser.WaitForAsyncOperations();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyFormCheckboxWidgetIsDeleted();
            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();
            BAT.Macros().NavigateTo().CustomPage("~/" + FeatherGlobals.BootstrapPageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyCheckboxesFieldIsNotVisible();
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