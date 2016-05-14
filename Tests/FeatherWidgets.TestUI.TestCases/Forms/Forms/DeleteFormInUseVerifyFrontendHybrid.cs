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
    /// DeleteFormInUseVerifyFrontendHybrid_ test class.
    /// </summary>
    [TestClass]
    public class DeleteFormInUseVerifyFrontendHybrid_ : FeatherTestCase
    {
        /// <summary>
        /// UI test DeleteFormInUseVerifyFrontendHybrid
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Forms)]
        public void DeleteFormInUseVerifyFrontendHybrid()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().CustomPage("~/" + FeatherGlobals.HybridPageName.ToLower(), true, this.Culture, new HtmlFindExpression("TagName=button")));
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyTextboxFieldContainerIsVisible();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifySubmitButtonIsVisible();
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().DeleteFormFromActionsMenu(FeatherGlobals.FormName);
            bool formIsPresent = BAT.Wrappers().Backend().Forms().FormsDashboard().IsFormPresentInGridView(FeatherGlobals.FormName);
            Assert.IsTrue(!formIsPresent);
            BAT.Macros().NavigateTo().CustomPage("~/" + FeatherGlobals.HybridPageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyMessageIsDisplayedAfterFormIsDeleted();
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