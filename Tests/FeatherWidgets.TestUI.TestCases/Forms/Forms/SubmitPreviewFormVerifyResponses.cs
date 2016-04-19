using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Forms.Forms
{
    /// <summary>
    /// SubmitPreviewFormVerifyResponses test class.
    /// </summary>
    [TestClass]
    public class SubmitPreviewFormVerifyResponses_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SubmitPreviewFormVerifyResponses
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Forms)]
        public void SubmitPreviewFormVerifyResponses()
        {
            string formIdAsString = BAT.Arrange(this.TestName).ExecuteSetUp().Result.Values["formId"];
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().PreviewForm("form_" + formIdAsString, this.Culture);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().ClickSubmit();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifySuccessSubmitMessageIsNotShown();
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().ViewFormResponses(FeatherGlobals.FormName, ResponseNumber);
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

        private const string ResponseNumber = "0 responses";
    }
}