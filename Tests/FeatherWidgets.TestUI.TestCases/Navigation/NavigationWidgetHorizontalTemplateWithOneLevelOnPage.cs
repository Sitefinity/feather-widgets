using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArtOfTest.WebAii.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using Telerik.WebAii.Controls.Html;
using Feather.Widgets.TestUI.Framework;

namespace FeatherWidgets.TestUI.TestCases.Navigation
{
    /// <summary>
    /// This is a sample test class.
    /// </summary>
    [TestClass]
    public class NavigationWidgetHorizontalTemplateWithOneLevelOnPage_ : FeatherTestCase
    {
        [TestMethod,
      Microsoft.VisualStudio.TestTools.UnitTesting.Owner("Feather team"),
      TestCategory(FeatherTestCategories.PagesAndContent)]
        public void NavigationWidgetHorizontalTemplateWithOneLevelOnPage()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Macros().NavigateTo().Pages();
            this.CreatePageWithTemplate(PageName, PageTemplateName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidget(WidgetName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.NavigatePageOnTheFrontend(PageName);
            this.VerifyNavigationOnTheFrontend();
        }

        public void CreatePageWithTemplate(string pageName, string templateName)
        {
            var createPageLink = BAT.Wrappers().Backend().Pages().PagesWrapper().GetCreatePageFromDecisionScreen();
            createPageLink.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncJQueryRequests();
            BAT.Wrappers().Backend().Pages().CreatePageWrapper().SetPageTitle(pageName);
            BAT.Wrappers().Backend().Pages().CreatePageWrapper().ClickSelectAnotherTemplateButton();
            BAT.Wrappers().Backend().Pages().SelectTemplateWrapper().SelectATemplate(templateName);
            BAT.Wrappers().Backend().Pages().SelectTemplateWrapper().ClickDoneButton();
            BAT.Wrappers().Backend().Pages().PagesWrapper().SavePageDataAndContinue();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().WaitUntilReady();
        }

        public void NavigatePageOnTheFrontend(string pageName)
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + pageName.ToLower());
            ActiveBrowser.WaitUntilReady();
        }

        public void VerifyNavigationOnTheFrontend()
        {
            string[] selectedPages = new string[] { PageName };

            BAT.Wrappers().Frontend().Navigation().NavigationFrontendWrapper().VerifyPagesFrontEndNavigation(NavTemplateClass, selectedPages);
        }

        private const string PageName = "boot";
        private const string WidgetName = "Navigation";
        private const string NavTemplateClass = "nav navbar-nav";
        private const string PageTemplateName = "Bootstrap.default";
    }
}
