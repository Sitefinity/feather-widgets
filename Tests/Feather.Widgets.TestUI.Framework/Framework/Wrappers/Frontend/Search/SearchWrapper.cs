using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.jQuery;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend
{
    /// <summary>
    /// This is the entry point class for search on frontend wrapper.
    /// </summary>
    public class SearchWrapper : BaseWrapper
    {
        /// <summary>
        /// Type text in search box
        /// </summary>
        /// <param name="searchText">search text</param>
        public void EnterSearchText(string searchText)
        {
            HtmlInputText searchBox = EM.Search.SearchFrontend.SearchBox.AssertIsPresent("Search input field is not present.");
            searchBox.Text = searchText;
        }

        /// <summary>
        /// Click search button
        /// </summary>
        public void Search()
        {
            HtmlButton searchButton = EM.Search.SearchFrontend.SearchButton.AssertIsPresent("Search box not present.");
            searchButton.Click();
            ActiveBrowser.WaitUntilReady();
        }

        /// <summary>
        /// Verify search results label
        /// </summary>
        public void VerifySearchResultsLabel(int numberOfSearchResults, string searchText)
        {
            HtmlContainerControl labelH1 = ActiveBrowser.Find.ByExpression<HtmlContainerControl>("tagname=h1");

            if (numberOfSearchResults == 0)
            {
                Assert.AreEqual("No search results for" + searchText, labelH1.InnerText, "Search label is not correct - no results");
            }
            else
            {
                Assert.AreEqual(numberOfSearchResults + " search results for" + searchText, labelH1.InnerText, "Search label is not correct");
            }
        }

        /// <summary>
        /// Select sorting option
        /// </summary>
        /// <param name="sortingOption">sorting option to select</param>
        public void SelectSortingOption(string sortingOption)
        {
            HtmlSelect sortingOptionsDropdown = EM.Search.SearchFrontend.SortingOptionsDropdown.AssertIsPresent("Sorting option dropdown is not present.");
            sortingOptionsDropdown.SelectByText(sortingOption);
            sortingOptionsDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            sortingOptionsDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
            ActiveBrowser.WaitUntilReady();
        }

        /// <summary>
        /// Verify search results list
        /// </summary>
        public void VerifySearchResultsList(params string[] resultTitles)
        {
            IList<HtmlDiv> resultsList = EM.Search.SearchFrontend.ResultsDivList;
            Assert.IsNotNull(resultsList, "Search results list is null");
            Assert.AreNotEqual(0, resultsList.Count, "Search results list has no elements");

            Assert.AreEqual(resultTitles.Count(), resultsList.Count, "Expected and actual count of results are not equal");

            for (int i = 0; i < resultsList.Count(); i++)
            {
                Assert.AreEqual(resultTitles[i], resultsList[i].ChildNodes[0].InnerText, "Expected: " + resultTitles[i] + " Actual: " + resultsList[i]);
            }
        }
    }
}
