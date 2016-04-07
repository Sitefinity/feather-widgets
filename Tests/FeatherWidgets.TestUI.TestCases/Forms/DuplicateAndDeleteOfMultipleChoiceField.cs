using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.WebAii.Controls.Html;
namespace FeatherWidgets.TestUI.TestCases.Forms
{
    /// <summary>
    /// DuplicateAndDeleteOfMultipleChoiceField test class.
    /// </summary>
    [TestClass]
    public class DuplicateAndDeleteOfMultipleChoiceField_ : FeatherTestCase
    {
        /// <summary>
        /// UI test DuplicateAndDeleteOfMultipleChoiceField
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void DuplicateAndDeleteOfMultipleChoiceField()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(MultipleChoiceField);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ApplyCssClasses(CssClassesToApply);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationNameDuplicate);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(MultipleChoiceField);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ApplyCssClasses(CssClassesToApplyNew);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            Assert.IsTrue(ActiveBrowser.ContainsText(CssClassesToApply), "Css class was not found on the page");
            Assert.IsTrue(ActiveBrowser.ContainsText(CssClassesToApplyNew), "Css class was not found on the page");

            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationNameDelete);
            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            Assert.IsTrue(ActiveBrowser.ContainsText(CssClassesToApply), "Css class was not found on the page");
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
        private const string MultipleChoiceField = "MultipleChoiceFieldController";
        private const string CssClassesToApply = "First";
        private const string PageName = "FormPage";
        private const string CssClassesToApplyNew = "Second";
        private const string OperationNameDuplicate = "Duplicate";
        private const string OperationNameDelete = "Delete";
    }
}
