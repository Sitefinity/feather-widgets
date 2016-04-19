using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArtOfTest.WebAii.Core;

namespace FeatherWidgets.TestUI.TestCases.Forms.Forms
{
    /// <summary>
    /// PublishUnpublishFormInUseVerifyFrontend test class.
    /// </summary>
    [TestClass]
    public class PublishUnpublishFormInUseVerifyFrontend_ : FeatherTestCase
    {
        /// <summary>
        /// UI test PublishUnpublishFormInUseVerifyFrontend
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void PublishUnpublishFormInUseVerifyFrontend()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + FeatherGlobals.BootstrapPageName.ToLower(), true, this.Culture, new HtmlFindExpression("TagName=button"));
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifySubmitButtonIsVisible();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyMultipleChoiceFieldContainerIsVisible();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyMultipleChoiceFieldLabelIsVisible(FeatherGlobals.SelectAChoiceLabelName);
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().UnpublishFormFromActionsMenu(FeatherGlobals.FormName);
            BAT.Macros().NavigateTo().CustomPage("~/" + FeatherGlobals.BootstrapPageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyMessageIsDisplayedAfterFormIsDeleted();
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().PublishFormFromActionsMenu(FeatherGlobals.FormName);
            BAT.Macros().NavigateTo().CustomPage("~/" + FeatherGlobals.BootstrapPageName.ToLower(), true, this.Culture, new HtmlFindExpression("TagName=button"));
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifySubmitButtonIsVisible();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyMultipleChoiceFieldContainerIsVisible();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyMultipleChoiceFieldLabelIsVisible(FeatherGlobals.SelectAChoiceLabelName);
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