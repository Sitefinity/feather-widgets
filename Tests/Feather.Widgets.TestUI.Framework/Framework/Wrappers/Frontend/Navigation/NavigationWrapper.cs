using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend
{
    /// <summary>
    /// This is the entry point class for navigation widget on the frontend.
    /// </summary>
    public class NavigationWrapper : BaseWrapper
    {
        /// <summary>
        /// Provides list of all navigation widgets
        /// </summary>
        /// <returns>Returns list of all navigation widgets</returns>
        public List<HtmlControl> ListWithNavigationWidgets()
        {
            List<HtmlControl> list = ActiveBrowser.Find.AllByTagName<HtmlControl>("nav").ToList<HtmlControl>();

            return list;
        }

        /// <summary>
        /// Verify navigation on the page frontend
        /// </summary>
        /// <param name="cssClass">The navigation css class</param>
        /// <param name="pages">Expected pages</param>
        public void VerifyNavigationOnThePageFrontend(string cssClass, string[] pages, TemplateType templateType = TemplateType.Bootstrap)
        {
            HtmlControl navList = null;

            switch (templateType)
            {
                case TemplateType.Bootstrap:
                    navList = EM.Navigation.NavigationWidgetFrontend.GetBootstrapNavigation(cssClass);
                    navList.AssertIsPresent("Navigation List");
                    Assert.AreEqual(pages.Count(), navList.ChildNodes.Count(), "Unexpected number of pages");
                    break;
                case TemplateType.Foundation:
                    navList = EM.Navigation.NavigationWidgetFrontend.GetFoundationNavigation(cssClass);
                    navList.AssertIsPresent("Navigation List");
                    Assert.AreEqual(pages.Count(), navList.ChildNodes.Count(), "Unexpected number of pages");
                    break;
                case TemplateType.Semantic:
                    navList = EM.Navigation.NavigationWidgetFrontend.GetSemanticNavigation(cssClass);
                    navList.AssertIsPresent("Navigation List");
                    Assert.AreEqual(pages.Count(), navList.ChildNodes.Where(n => n.TagName.Equals("a")).Count(), "Unexpected number of pages");
                    break;
            }

            for (int i = 0; i < pages.Count(); i++)
            {
                Assert.IsTrue(navList.ChildNodes[i].InnerText.Contains(pages[i]), "Navigation child node does not contain the expected page: " + pages[i]);
            }
        }

        /// <summary>
        /// Verify child pages on the frontend
        /// </summary>
        /// <param name="cssClass">The navigation css class</param>
        /// <param name="pages">Expected pages</param>
        public void VerifyChildPagesFrontEndNavigation(string cssClass, string[] pages, TemplateType templateType = TemplateType.Bootstrap)
        {
            HtmlControl navList = null;

            switch (templateType)
            {
                case TemplateType.Bootstrap:
                    navList = EM.Navigation.NavigationWidgetFrontend.GetBootstrapNavigation(cssClass);
                    break;
                case TemplateType.Foundation:
                    navList = EM.Navigation.NavigationWidgetFrontend.GetFoundationNavigationChild(cssClass);
                    break;
                case TemplateType.Semantic:
                    navList = EM.Navigation.NavigationWidgetFrontend.GetSemanticNavigationChild(cssClass);
                    break;
            }

            navList.AssertIsPresent("Navigation List");
            Assert.AreEqual(pages.Count(), navList.ChildNodes.Count(), "Unexpected number of pages");

            foreach (string page in pages)
            {
                navList.AssertContainsText<HtmlControl>(page, "Navigation does not contain the expected page " + page);
            }
        }

        /// <summary>
        /// Verify that a list of pages is NOT present in the frontend navigation
        /// </summary>
        /// <param name="cssClass"></param>
        /// <param name="pages"></param>
        public void VerifyPagesNotPresentFrontEndNavigation(string cssClass, string[] pages, TemplateType templateType = TemplateType.Bootstrap)
        {
            HtmlControl navList = null;

            switch (templateType)
            {
                case TemplateType.Bootstrap:
                    navList = EM.Navigation.NavigationWidgetFrontend.GetBootstrapNavigation(cssClass);                   
                    break;
                case TemplateType.Foundation:
                    navList = EM.Navigation.NavigationWidgetFrontend.GetFoundationNavigation(cssClass);
                    break;
                case TemplateType.Semantic:
                    navList = EM.Navigation.NavigationWidgetFrontend.GetSemanticNavigation(cssClass);
                    break;
            }

            navList.AssertIsPresent("Navigation List");

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
        /// Clicks on a page link from the Mvc navigation on the frontend.
        /// </summary>
        /// <param name="pageTitle">The page title.</param>
        public void ClickOnPageLinkFromNavigationMenu(string pageTitle, TemplateType templateType, string cssClass, bool isParentPage = true)
        {
            HtmlAnchor pageLink = null;

            switch (templateType)
            {
                case TemplateType.Bootstrap:
                    pageLink = this.GetPageLinkByTitleFromBootstrapNavigation(cssClass, pageTitle);
                    break;
                case TemplateType.Foundation:
                    pageLink = this.GetPageLinkByTitleFromFoundationNavigation(cssClass, pageTitle);
                    break;
                case TemplateType.Semantic:
                    pageLink = this.GetPageLinkByTitleFromSemanticNavigation(cssClass, pageTitle, isParentPage);
                    break;
            }

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
        public bool AssertNavigationIsVisible(string cssClass, TemplateType templateType)
        {
            HtmlControl navList = null;

            switch (templateType)
            {
                case TemplateType.Bootstrap:
                    navList = EM.Navigation.NavigationWidgetFrontend.GetBootstrapNavigation(cssClass);
                    break;
                case TemplateType.Foundation:
                    navList = EM.Navigation.NavigationWidgetFrontend.GetFoundationNavigation(cssClass);
                    break;
                case TemplateType.Semantic:
                    navList = EM.Navigation.NavigationWidgetFrontend.GetSemanticNavigation(cssClass);
                    break;
            }

            if (navList != null && navList.IsVisible())
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

        private HtmlAnchor GetPageLinkByTitleFromBootstrapNavigation(string cssClass, string pageTitle)
        {
            HtmlUnorderedList list = this.EM.Navigation.NavigationWidgetFrontend.GetBootstrapNavigation(cssClass)
                .AssertIsNotNull("list");

            HtmlListItem listItem = list.ChildNodes.Where(i => i.InnerText.Contains(pageTitle)).FirstOrDefault().As<HtmlListItem>();
            listItem.AssertIsPresent<HtmlListItem>("List Item");

            HtmlAnchor link = listItem.Find.ByExpression<HtmlAnchor>("InnerText=" + pageTitle);

            if (link == null || !link.IsVisible())
            {
                throw new ArgumentNullException("Link not found");
            }
            else return link;
        }

        private HtmlAnchor GetPageLinkByTitleFromFoundationNavigation(string cssClass, string pageTitle)
        {
            HtmlControl list = this.EM.Navigation.NavigationWidgetFrontend.GetFoundationNavigation(cssClass)
                .AssertIsNotNull("list");

            HtmlListItem listItem = list.ChildNodes.Where(i => i.InnerText.Contains(pageTitle)).FirstOrDefault().As<HtmlListItem>()
                .AssertIsPresent<HtmlListItem>("List Item");

            HtmlAnchor link = listItem.Find.ByExpression<HtmlAnchor>("InnerText=" + pageTitle);

            if (link == null || !link.IsVisible())
            {
                throw new ArgumentNullException("Link not found");
            }
            else return link;
        }

        private HtmlAnchor GetPageLinkByTitleFromSemanticNavigation(string cssClass, string pageTitle, bool isParentPage = true)
        {
            HtmlControl nav = null;

            if (isParentPage)
            {
                nav = this.EM.Navigation.NavigationWidgetFrontend.GetSemanticNavigation(cssClass);
            }
            else
            {
                nav = this.EM.Navigation.NavigationWidgetFrontend.GetSemanticNavigationChild(cssClass);
            }

            nav.AssertIsNotNull("Navigation");
            HtmlAnchor link = nav.ChildNodes.Where(n => n.InnerText.Contains(pageTitle)).FirstOrDefault().As<HtmlAnchor>();

            if (link == null || !link.IsVisible())
            {
                throw new ArgumentNullException("Link not found");
            }
            else return link;
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