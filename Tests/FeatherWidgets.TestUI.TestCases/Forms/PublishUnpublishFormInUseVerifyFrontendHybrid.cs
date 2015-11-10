using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.Forms
{
    /// <summary>
    /// PublishUnpublishFormInUseVerifyFrontendHybrid test class.
    /// </summary>
    [TestClass]
    public class PublishUnpublishFormInUseVerifyFrontendHybrid_ : FeatherTestCase
    {
        /// <summary>
        /// UI test PublishUnpublishFormInUseVerifyFrontendHybrid
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.Forms)]
        public void PublishUnpublishFormInUseVerifyFrontendHybrid()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + FeatherGlobals.HybridPageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifySubmitButtonIsVisible();
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().UnpublishFormFromActionsMenu(FeatherGlobals.FormName);
            BAT.Macros().NavigateTo().CustomPage("~/" + FeatherGlobals.HybridPageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyMessageIsDisplayedAfterFormIsDeleted();
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().PublishFormFromActionsMenu(FeatherGlobals.FormName);
            BAT.Macros().NavigateTo().CustomPage("~/" + FeatherGlobals.HybridPageName.ToLower(), true, this.Culture);
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