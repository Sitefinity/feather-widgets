using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.jQuery;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend
{
    /// <summary>
    /// This is the entry point class for search on frontend wrapper.
    /// </summary>
    public class SearchWrapper : BaseWrapper
    {
        /// <summary>
        /// Type text in search box for Sitefinity template and Semantic UI template
        /// </summary>
        /// <param name="searchText">search text</param>
        public void EnterSearchText(string searchText)
        {
            HtmlInputText searchBox = this.EM.Search.SearchFrontend.SearchTextBox.AssertIsPresent("Search input field");
            searchBox.Text = searchText;
        }

        /// <summary>
        /// Type text in search box for Bootstrap template and Foundation template
        /// </summary>
        /// <param name="searchText">search text</param>
        public void EnterSearchInput(string searchText)
        {
            HtmlInputSearch searchBox = this.EM.Search.SearchFrontend.SearchInput.AssertIsPresent("Search input field");
            searchBox.Text = searchText;
        }

        /// <summary>
        /// Click search button
        /// </summary>
        /// <param name="resultPageUrl">results page URL</param>
        public void ClickSearchButton(string resultsPageUrl)
        {
            HtmlButton searchButton = this.EM.Search.SearchFrontend.SearchButton.AssertIsPresent("Search button");

            searchButton.ScrollToVisible();
            searchButton.Focus();
            searchButton.MouseClick();
            searchButton.InvokeClick();

            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForUrl(resultsPageUrl);
            ActiveBrowser.WaitForAsyncJQueryRequests();
        }

        /// <summary>
        /// Press Enter to activate search
        /// </summary>
        public void PressEnter()
        {
            HtmlButton searchButton = this.EM.Search.SearchFrontend.SearchButton.AssertIsPresent("Search button");
            searchButton.Focus();
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Enter);

            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncJQueryRequests();
        }

        /// <summary>
        /// Click search link
        /// </summary>
        /// <param name="resultPageUrl">results page URL</param>
        public void ClickSearchLink(string resultsPageUrl)
        {
            HtmlAnchor searchButton = this.EM.Search.SearchFrontend.SearchLink.AssertIsPresent("Search link");

            searchButton.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForUrl(resultsPageUrl);
            ActiveBrowser.WaitForAsyncJQueryRequests();
        }

        /// <summary>
        /// Verify search results label
        /// </summary>
        public void VerifySearchResultsLabel(int numberOfSearchResults, string searchText)
        {
            HtmlContainerControl resultsLabelH1 = this.EM.Search.SearchFrontend.ResultsLabel.AssertIsPresent("Results label");

            if (numberOfSearchResults == 0)
            {
                Assert.AreEqual("No search results for" + searchText, resultsLabelH1.InnerText, "Search label is not correct - no results");
            }
            else
            {
                Assert.AreEqual(numberOfSearchResults + " search results for" + searchText, resultsLabelH1.InnerText, "Search label is not correct");
            }
        }

        /// <summary>
        /// Select sorting option
        /// </summary>
        /// <param name="sortingOption">sorting option to select</param>
        public void SelectSortingOption(string sortingOption)
        {
            HtmlSelect sortingOptionsDropdown = this.EM.Search.SearchFrontend.SortingOptionsDropdown.AssertIsPresent("Sorting option dropdown");
            
            sortingOptionsDropdown.SelectByText(sortingOption);
            sortingOptionsDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            sortingOptionsDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);

            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncJQueryRequests();
        }

        /// <summary>
        /// Verify search results list
        /// </summary>
        /// <param name="resultTitles">expected titles for search result</param>
        public void VerifySearchResultsList(params string[] resultTitles)
        {
            IList<HtmlDiv> resultsList = this.EM.Search.SearchFrontend.ResultsDivList;
            Assert.IsNotNull(resultsList, "Search results list is null");
            Assert.AreNotEqual(0, resultsList.Count, "Search results list has no elements");

            Assert.AreEqual(resultTitles.Count(), resultsList.Count, "Expected and actual count of results are not equal");

            for (int i = 0; i < resultsList.Count(); i++)
            {
                Assert.AreEqual("h3", resultsList[i].ChildNodes[0].TagName, "First row is not h3");
                Assert.AreEqual(resultTitles[i], resultsList[i].ChildNodes[0].InnerText);

                Assert.AreEqual("p", resultsList[i].ChildNodes[1].TagName, "Second row is not paragraph");
                Assert.IsTrue(resultsList[i].ChildNodes[1].InnerText.Contains(resultTitles[i].Replace(" ", string.Empty)));

                //// if there is a page with widget displaying search result item, then a row with link exist on frontend
                if (resultsList[i].ChildNodes.Count == 3)
                {
                    Assert.AreEqual("a", resultsList[i].ChildNodes[2].TagName, "Third row is not anchor");
                    Assert.IsTrue(resultsList[i].ChildNodes[2].InnerText.Contains(resultTitles[i]));
                }
            }
        }

        /// <summary>
        /// Verify css class of div element containing search box
        /// </summary>
        /// <param name="cssClass">css class</param>
        public void VerifySearchBoxCssClass(string cssClass)
        {
            HtmlDiv searchBoxDiv = this.ActiveBrowser.Find.ByExpression<HtmlDiv>("class=" + cssClass);
            searchBoxDiv.AssertIsNotNull("search box div is not found");

            searchBoxDiv.Find.ByExpression<HtmlButton>("tagname=button", "innerText=Search").AssertIsNotNull("search button is not found");
        }

        /// <summary>
        /// Verify css class of div element containing search results
        /// </summary>
        /// <param name="cssClass">css class</param>
        public void VerifySearchResultsCssClass(string cssClass)
        {
            HtmlDiv searchResultsDiv = this.ActiveBrowser.Find.ByExpression<HtmlDiv>("class=" + cssClass);
            searchResultsDiv.AssertIsNotNull("search results div is not found");

            searchResultsDiv.Find.ByExpression<HtmlContainerControl>("tagname=h1").AssertIsNotNull("search result label is not found");
        }
    }
}
