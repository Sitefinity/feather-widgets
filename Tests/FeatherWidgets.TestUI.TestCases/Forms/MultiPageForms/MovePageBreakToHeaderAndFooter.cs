using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Forms.MultiPageForms
{
    /// <summary>
    /// MovePageBreakToHeaderAndFooter_ test class.
    /// </summary>
    [TestClass]
    public class MovePageBreakToHeaderAndFooter_ : FeatherTestCase
    {
        /// <summary>
        /// UI test MovePageBreakToHeaderAndFooter
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void MovePageBreakToHeaderAndFooter()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().MoveFieldInZoneEditor(FieldName, Header);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyFieldsInPlaceholder(FieldName, Header, false);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyFieldsInPlaceholder(FieldName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().MoveFieldInZoneEditor(FieldName, Footer);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyFieldsInPlaceholder(FieldName, Footer, false);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyFieldsInPlaceholder(FieldName);

            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ClickOnFieldMenuItem(FieldName, FeatherGlobals.DuplicateWidgetMenuOption);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().MoveFieldInZoneEditor(FieldName, Header);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyFieldsInPlaceholder(FieldName, Header, false);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyFieldsInPlaceholder(FieldName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().MoveFieldInZoneEditor(FieldName, Footer);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyFieldsInPlaceholder(FieldName, Footer, false);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyFieldsInPlaceholder(FieldName);

            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();
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

        private const string FormName = "MultiPageForm";
        private const string PageName = "FormPage";
        private const string FieldName = "PageBreakController";
        private const string Footer = "Footer";
        private const string Header = "Header";
    }
}
