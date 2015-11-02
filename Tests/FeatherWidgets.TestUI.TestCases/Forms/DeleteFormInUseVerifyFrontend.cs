using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.Forms
{
    /// <summary>
    /// DeleteFormInUseVerifyFrontend_ test class.
    /// </summary>
    [TestClass]
    public class DeleteFormInUseVerifyFrontend_ : FeatherTestCase
    {
        /// <summary>
        /// UI test DeleteFormInUseVerifyFrontend
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void DeleteFormInUseVerifyFrontend()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
           
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
            //BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string FormName = "MvcForm";
        private const string PageName = "FormPage";
        private const string WidgetName = "Form";
        private const string FieldName = "Checkboxes";
        private const string LabelName = "Select a choice";
        private const string Choice = "Second Choice";
        private const int ExpectedResponsesCount = 1;
        private const int ResponseNumber = 1;
        private const string ExpectedAuthorName = "admin";
    }
}