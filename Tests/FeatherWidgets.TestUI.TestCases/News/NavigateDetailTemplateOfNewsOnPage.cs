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
    /// NavigateDetailTemplateOfNewsOnPage test class.
    /// </summary>
    [TestClass]
    public class NavigateDetailTemplateOfNewsOnPage : FeatherTestCase
    {
        /// <summary>
        /// UI test NavigateDetailTemplateOfNewsOnPageBasedOnBootstrapTemplate
        /// </summary>
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void NavigateDetailTemplateOfNewsOnPageBasedOnBootstrapTemplate()
        {
            pageTemplateName = "Bootstrap.default";

            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.ArrangementClass).AddParameter("templateName", pageTemplateName).ExecuteSetUp();

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidget(WidgetName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, NewsTitle);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.VerifyNewsOnTheFrontend();
            BATFeather.Wrappers().Frontend().News().NewsWrapper().ClickNewsTitle(NewsTitle);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().News().NewsWrapper().IsNewsTitlePresentOnDetailMasterPage(NewsTitle));
        }

        /// <summary>
        /// UI test NavigateDetailTemplateOfNewsOnPageBasedOnFoundationTemplate
        /// </summary>
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.Foundation)]
        public void NavigateDetailTemplateOfNewsOnPageBasedOnFoundationTemplate()
        {
            pageTemplateName = "Foundation.default";

            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.ArrangementClass).AddParameter("templateName", pageTemplateName).ExecuteSetUp();

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidget(WidgetName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, NewsTitle);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            Assert.IsTrue(BATFeather.Wrappers().Frontend().News().NewsWrapper().IsNewsTitlesPresentOnThePageFrontend(this.newsTitles));
            BATFeather.Wrappers().Frontend().News().NewsWrapper().ClickNewsTitle(NewsTitle);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().News().NewsWrapper().IsNewsTitlePresentOnDetailMasterPage(NewsTitle));
        }

        /// <summary>
        /// UI test NavigateDetailTemplateOfNewsOnPageBasedOnSemanticUITemplate
        /// </summary>
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.SemanticUI)]
        public void NavigateDetailTemplateOfNewsOnPageBasedOnSemanticUITemplate()
        {
            pageTemplateName = "SemanticUI.default";

            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.ArrangementClass).AddParameter("templateName", pageTemplateName).ExecuteSetUp();

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidget(WidgetName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, NewsTitle);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.VerifyNewsOnTheFrontend();
            BATFeather.Wrappers().Frontend().News().NewsWrapper().ClickNewsTitle(NewsTitle);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().News().NewsWrapper().IsNewsTitlePresentOnDetailMasterPage(NewsTitle));
        }

        /// <summary>
        /// Verify news widget on the frontend
        /// </summary>
        public void VerifyNewsOnTheFrontend()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            Assert.IsTrue(BATFeather.Wrappers().Frontend().News().NewsWrapper().IsNewsTitlesPresentOnThePageFrontend(this.newsTitles));
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.ArrangementClass).ExecuteTearDown();
        }

        private string ArrangementClass
        {
            get { return ArrangementClassName; }
        }

        private const string PageName = "News";
        private const string WidgetName = "News";
        private const string NewsTitle = "NewsTitle";
        private readonly string[] newsTitles = new string[] { NewsTitle };
        private string pageTemplateName;
        private const string ArrangementClassName = "NavigateDetailTemplateOfNewsOnPage";
    }
}
