using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Win32.Dialogs;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Navigation
{
    /// <summary>
    /// DuplicateNavigationWidgetFromPage test class.
    /// </summary>
    [TestClass]
    public class DuplicateNavigationWidgetFromPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test DuplicateNavigationWidgetFromPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void DuplicateNavigationWidgetFromPage()
        {
            string pageTemplateName = "Bootstrap.default";

            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.ArrangementClass).AddParameter("templateName", pageTemplateName).ExecuteSetUp();
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            Assert.AreEqual(2, BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ListWithNavigationWidgets().Count);
        }

        /// <summary>
        /// UI test DuplicateNavigationWidgetFromPageFoundation
        /// </summary>
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.Foundation)]
        public void DuplicateNavigationWidgetFromPageFoundation()
        {
            string pageTemplateName = "Foundation.default";

            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.ArrangementClass).AddParameter("templateName", pageTemplateName).ExecuteSetUp();
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            Assert.AreEqual(2, BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ListWithNavigationWidgets().Count);
        }

        /// <summary>
        /// UI test DuplicateNavigationWidgetFromPageSemanticUI
        /// </summary>
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.SemanticUI)]
        public void DuplicateNavigationWidgetFromPageSemanticUI()
        {
            string pageTemplateName = "SemanticUI.default";

            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.ArrangementClass).AddParameter("templateName", pageTemplateName).ExecuteSetUp();
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationName);     
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            Assert.AreEqual(2, BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ListWithNavigationWidgets().Count);
        }

        private string ArrangementClass
        {
            get { return ArrangementClassName; }
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.ArrangementClass).ExecuteTearDown();
        }

        private const string PageName = "ParentPage";
        private const string WidgetName = "Navigation";
        private const string OperationName = "Duplicate";
        private const string ArrangementClassName = "DuplicateNavigationWidgetFromPage";
    }
}
