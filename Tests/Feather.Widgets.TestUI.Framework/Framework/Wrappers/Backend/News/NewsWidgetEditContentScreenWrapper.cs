using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    /// <summary>
    /// This is the entry point class for news widget edit wrapper.
    /// </summary>
    public class NewsWidgetEditContentScreenWrapper : BaseWrapper
    {
        /// <summary>
        /// Selects which news to display in the widget designer
        /// </summary>
        /// <param name="mode">Which news to display</param>
        public void SelectWhichNewsToDisplay(string mode)
        {
            HtmlUnorderedList optionsList = EM.News.NewsWidgetContentScreen.WhichNewsToDisplayList
                .AssertIsPresent("Which news to display options list");

            HtmlListItem option = optionsList.AllItems.Where(a => a.InnerText.Contains(mode)).FirstOrDefault()
                .AssertIsPresent("Which news to display option" + mode);

            HtmlInputRadioButton optionButton = option.Find.ByExpression<HtmlInputRadioButton>("tagname=input")
                .AssertIsPresent("Which news to display option radio button");

            optionButton.Click();
        }

        /// <summary>
        /// Selects the taxonomy.
        /// </summary>
        /// <param name="taxonomy">The taxonomy.</param>
        public void SelectTaxonomy(string taxonomy)
        {
            ActiveBrowser.WaitForAsyncOperations();

            HtmlInputCheckBox optionButton = ActiveBrowser.Find.ByExpression<HtmlInputCheckBox>("id=" + taxonomy)
                .AssertIsPresent("Taxonomy option");

            optionButton.Click();

            ActiveBrowser.WaitForAsyncOperations();
        }

        /// <summary>
        /// Provide access to done button
        /// </summary>
        public void DoneSelectingButton()
        {
            HtmlButton shareButton = EM.News.NewsWidgetContentScreen.DoneSelectingButton
            .AssertIsPresent("Done selecting button");
            shareButton.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
        }

        /// <summary>
        /// Select news item
        /// </summary>
        /// <param name="newsTitle">The title of the news item</param>
        public void SelectItem(string newsTitle)
        {
            HtmlDiv sharedContentBlockList = EM.News.NewsWidgetContentScreen.NewsList
            .AssertIsPresent("Shared content list");

            var itemSpan = sharedContentBlockList.Find.ByExpression<HtmlSpan>("class=~ng-binding", "InnerText=" + newsTitle);

            itemSpan.Wait.ForVisible();
            itemSpan.ScrollToVisible();
            itemSpan.MouseClick();
            this.DoneSelectingButton();

            ActiveBrowser.WaitForAsyncOperations();
            ActiveBrowser.RefreshDomTree();
            itemSpan.AssertIsPresent("Selected item");
        }
 
        /// <summary>
        /// Opens the selector.
        /// </summary>
        public void OpenSelector()
        {
            var selectButtons = EM.News.NewsWidgetContentScreen.SelectButtons;
            foreach (var button in selectButtons)
            {
                if (button.IsVisible())
                {
                    button.Click();
                    break;
                }
            }

            ActiveBrowser.WaitForAsyncOperations();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Sets a text to search in t he search input.
        /// </summary>
        /// <param name="text">The text to be searched for.</param>
        public void SetSearchText(string text)
        {
            HtmlInputText input = EM.News.NewsWidgetContentScreen.SearchInput
                                      .AssertIsPresent("input");

            input.ScrollToVisible();
            input.Focus();
            input.MouseClick();
            input.MouseClick();

            Manager.Current.Desktop.KeyBoard.TypeText(text);

            ActiveBrowser.WaitForAsyncOperations();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Waits for items count to appear.
        /// </summary>
        /// <param name="expectedCount">The expected items count.</param>
        public void WaitForItemsToAppear(int expectedCount)
        {
            Manager.Current.Wait.For(() => this.CountItems(expectedCount), 50000);
        }
      
        /// <summary>
        /// Counts the items.
        /// </summary>
        /// <param name="expected">The expected.</param>
        /// <returns></returns>
        public bool CountItems(int expected)
        {
            ActiveBrowser.RefreshDomTree();
            HtmlDiv scroller = ActiveBrowser.Find.ByExpression<HtmlDiv>("class=list-group s-items-list-wrp endlessScroll")
                .AssertIsPresent("Scroller");
            scroller.MouseClick(MouseClickType.LeftDoubleClick);
            Manager.Current.Desktop.Mouse.TurnWheel(1000, MouseWheelTurnDirection.Backward);
            var items = EM.News.NewsWidgetContentScreen.SelectorItems;
            return expected == items.Count;
        }

        /// <summary>
        /// Save news widget
        /// </summary>
        public void SaveChanges()
        {
            HtmlButton saveButton = EM.News.NewsWidgetContentScreen.SaveChangesButton
            .AssertIsPresent("Save button");
            saveButton.Click();
        }
    }
}
