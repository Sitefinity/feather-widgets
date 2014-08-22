using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatherWidgets.TestUI
{
    /// <summary>
    /// This is a sample test class.
    /// </summary>
    [TestClass]
    public class NavigationWidgetAddChangeRemoveCSSClass_ : FeatherTestCase
    {
        // <summary>
        /// Pefroms Server Setup and prepare the system with needed data.
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

        [TestMethod,
       Microsoft.VisualStudio.TestTools.UnitTesting.Owner("Feather team"),
       TestCategory(FeatherTestCategories.PagesAndContent)]
        public void NavigationWidgetAddChangeRemoveCSSClass()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidget(WidgetName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Navigation().MoreOptions();
            BATFeather.Wrappers().Backend().Navigation().FillCSSClass(CssClass);
            BATFeather.Wrappers().Backend().Navigation().SaveChanges();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Navigation().MoreOptions();
            BATFeather.Wrappers().Backend().Navigation().VerifyCSSClass(CssClass);
            BATFeather.Wrappers().Backend().Navigation().FillCSSClass(NewCssClass);
            BATFeather.Wrappers().Backend().Navigation().SaveChanges();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Navigation().MoreOptions();
            BATFeather.Wrappers().Backend().Navigation().VerifyCSSClass(EditedCssClass);
            BATFeather.Wrappers().Backend().Navigation().RemoveCSSClass();
            BATFeather.Wrappers().Backend().Navigation().SaveChanges();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Navigation().MoreOptions();
            BATFeather.Wrappers().Backend().Navigation().VerifyCSSClass(EmptyCssClass);
            BATFeather.Wrappers().Backend().Navigation().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
        }

        private const string PageName = "ParentPage";
        private const string WidgetName = "Navigation";
        private const string CssClass = "class";
        private const string NewCssClass = " edited";
        private const string EditedCssClass = "class edited";
        private const string EmptyCssClass = "";
    }
}
