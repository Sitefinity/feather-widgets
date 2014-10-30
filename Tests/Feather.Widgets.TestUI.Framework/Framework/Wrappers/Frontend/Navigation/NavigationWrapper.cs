using System;
using System.Collections.Generic;
using System.Linq;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using System.Drawing;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend
{
    /// <summary>
    /// This is the entry point class for navigation widget on the frontend.
    /// </summary>
    public class NavigationWrapper : BaseWrapper
    {
        /// <summary>
        /// Provides list of all navigation div
        /// </summary>
        /// <returns>Returns list of all navigation div</returns>
        public List<HtmlDiv> ListWithNavigationDiv()
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            List<HtmlDiv> navigationList = frontendPageMainDiv.Find.AllByExpression<HtmlDiv>("tagname=div", "id=bs-example-navbar-collapse-1").ToList<HtmlDiv>();

            return navigationList;
        }

        /// <summary>
        /// Verify navigation on the page frontend
        /// </summary>
        /// <param name="cssClass">The navigation css class</param>
        /// <param name="pages">Expected pages</param>
        public void VerifyNavigationOnThePageFrontend(string cssClass, string[] pages)
        {
            List<HtmlDiv> navigationCount = this.ListWithNavigationDiv();

            for (int i = 0; i < navigationCount.Count; i++)
            {
                HtmlUnorderedList navList = navigationCount[i].Find.ByExpression<HtmlUnorderedList>("class=^" + cssClass)
                .AssertIsPresent("Navigation with selected css class");
                Assert.AreEqual(pages.Count(), navList.ChildNodes.Count(), "Unexpected number of pages");

                foreach (string page in pages)
                {
                    Assert.IsTrue(navList.InnerText.Contains(page), "Navigation does not contain the expected page " + page);
                }
            }
        }

        /// <summary>
        /// Verify navigation count on the page frontend
        /// </summary>
        /// <param name="expectedCount">The expected count</param>
        public void VerifyNavigationCountOnThePageFrontend(int expectedCount)
        {
            List<HtmlDiv> navigationCount = this.ListWithNavigationDiv();
            Assert.AreEqual<int>(expectedCount, navigationCount.Count, "unexpected number");
        }

        /// <summary>
        /// Verify child pages on the frontend
        /// </summary>
        /// <param name="cssClass">The navigation css class</param>
        /// <param name="pages">Expected pages</param>
        public void VerifyChildPagesFrontEndNavigation(string cssClass, string[] pages)
        {
            HtmlUnorderedList navList = ActiveBrowser.Find.ByExpression<HtmlUnorderedList>("class=" + cssClass)
                .AssertIsPresent("Navigation with selected class");

            Assert.AreEqual(pages.Count(), navList.ChildNodes.Count(), "Unexpected number of pages");

            foreach (string page in pages)
            {
                Assert.IsTrue(navList.InnerText.Contains(page), "Navigation does not contain the expected page " + page);
            }
        }

        /// <summary>
        /// Verify that a list of pages is NOT present in the frontend navigation
        /// </summary>
        /// <param name="cssClass"></param>
        /// <param name="pages"></param>
        public void VerifyPagesNotPresentFrontEndNavigation(string cssClass, string[] pages)
        {
            HtmlUnorderedList navList = ActiveBrowser.Find.ByExpression<HtmlUnorderedList>("class=^" + cssClass)
                .AssertIsPresent("Navigation with selected css class");

            for (int i = 0; i < navList.ChildNodes.Count; i++)
            {
                foreach (string page in pages)
                {
                    Assert.IsFalse(navList.ChildNodes[i].InnerText.Contains(page), "Navigation contains this page " + page);
                }
            }
        }

        /// <summary>
        /// Opens the toggle navigation menu
        /// </summary>
        public void OpenNavigationToggleMenu()
        {
            HtmlButton toggleButton = this.EM.Navigation.NavigationWidgetFrontend.ToggleButton
                .AssertIsPresent<HtmlButton>("Toggle Button");

            toggleButton.Click();
        }

        /// <summary>
        /// Gets the page html anchor link by title.
        /// </summary>
        /// <param name="pageTitle">The page title.</param>
        /// <returns></returns>
        public HtmlAnchor GetPageLinkByTitleFromNavigation(string pageTitle, TemplateType templateType)
        {
            HtmlUnorderedList list = null;

            if (templateType == TemplateType.Bootstrap)
            {
                list = ActiveBrowser.Find.ByExpression<HtmlUnorderedList>("class=nav navbar-nav");
                list.AssertIsNotNull("list");
            }

            else if (templateType == TemplateType.Foundation)
            {
                HtmlControl section = ActiveBrowser.Find.ByExpression<HtmlControl>("tagname=section", "class=top-bar-section");
                section.AssertIsNotNull("section");

                list = section.Find.AllByTagName<HtmlUnorderedList>("ul").First();
            }

            HtmlListItem listItem = list.ChildNodes.Where(i => i.InnerText.Contains(pageTitle)).FirstOrDefault().As<HtmlListItem>();
            listItem.AssertIsPresent<HtmlListItem>("List Item");

            HtmlAnchor link = listItem.Find.ByExpression<HtmlAnchor>("InnerText=" + pageTitle);

            if (link == null || !link.IsVisible())
            {
                throw new ArgumentNullException("Link not found");
            }

            else return link;
        }

        /// <summary>
        /// Clicks on a page link from the Mvc navigation on the frontend.
        /// </summary>
        /// <param name="pageTitle">The page title.</param>
        public void ClickOnPageLinkFromNavigationMenu(string pageTitle, TemplateType templateType)
        {
            HtmlAnchor pageLink = this.GetPageLinkByTitleFromNavigation(pageTitle, templateType);
            pageLink.Click();
            ActiveBrowser.WaitForUrl("/" + pageTitle.ToLower(), true, TimeOut);
            ActiveBrowser.WaitUntilReady();
        }

        /// <summary>
        /// Asserts that toggle button is visible.
        /// </summary>
        /// <returns>True or False depending on the button visibility.</returns>
        public bool AssertToggleButtonIsVisible()
        {
            HtmlButton toggleButton = this.EM.Navigation.NavigationWidgetFrontend.ToggleButton;

            if (toggleButton.IsVisible())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Asserts that navigation menu is visible on the frontend.
        /// </summary>
        /// <returns>true or false depending on the navigation visibility.</returns>
        public bool AssertNavigationIsVisible()
        {
            HtmlUnorderedList nav = this.EM.Navigation.NavigationWidgetFrontend.Navigation;

            if (nav.IsVisible())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Asserts if the dropdown navigation is visible.
        /// </summary>
        /// <returns>true or false</returns>
        public bool AsserNavigationDropDownMenuIsVisible()
        {
            HtmlSelect menu = this.EM.Navigation.NavigationWidgetFrontend.NavigationDropDown;

            if (menu.IsVisible())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Resizes the browser window to selected width.
        /// </summary>
        /// <param name="width">The window width.</param>
        public void ResizeBrowserWindow(int width)
        {
            Rectangle rect = new Rectangle(200, 200, width, 500);

            ActiveBrowser.ResizeContent(rect);
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Restores the browser window to default.
        /// </summary>
        public void RestoreBrowserWindow()
        {
            ActiveBrowser.Window.Restore();
            ActiveBrowser.Window.Maximize();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Opens the toggle navigation menu in foundation template
        /// </summary>
        public void OpenToggleMenuForFoundationTemplate()
        {
            HtmlAnchor toggleButton = this.EM.Navigation.NavigationWidgetFrontend.FoundationMenuLink
                .AssertIsPresent<HtmlAnchor>("Menu Button");

            toggleButton.Click();
        }

        private const int TimeOut = 60000;
    }
}

/// <summary>
/// Different types of templates.
/// </summary>
public enum TemplateType
{
    /// <summary>
    /// Bootstrap template.
    /// </summary>
    Bootstrap,

    /// <summary>
    /// Foundation template.
    /// </summary>
    Foundation,

    /// <summary>
    /// Semantic template.
    /// </summary>
    Semantic
}