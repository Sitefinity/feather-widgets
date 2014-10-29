using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.jQuery;
using ArtOfTest.Common.UnitTesting;
using System.Globalization;

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
            int position;
            HtmlDiv optionsDiv = EM.News.NewsWidgetContentScreen.WhichNewsToDisplayList
                .AssertIsPresent("Which news to display options list");

            List<HtmlDiv> newsDivs = optionsDiv.Find.AllByExpression<HtmlDiv>("tagname=div", "class=radio").ToList<HtmlDiv>();

            if (mode.Contains("Selected"))
            {
                position = 1;
            }
            else if (mode.Contains("Narrow"))
            {
                position = 2;
            }
            else 
            {
                position = 0;
            }

            HtmlInputRadioButton optionButton = newsDivs[position].Find.ByExpression<HtmlInputRadioButton>("tagname=input")
                .AssertIsPresent("Which news to display option radio button");

            optionButton.Click();
        }

        /// <summary>
        /// Selects the taxonomy.
        /// </summary>
        /// <param name="taxonomy">The taxonomy.</param>
        public void SelectCheckBox(string taxonomy)
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
            HtmlDiv newsList = EM.News.NewsWidgetContentScreen.NewsList
            .AssertIsPresent("News list");

            var itemDiv = newsList.Find.ByExpression<HtmlDiv>("class=ng-binding", "InnerText=" + newsTitle)
                .AssertIsPresent("News with this title was not found");

            itemDiv.Wait.ForVisible();
            itemDiv.ScrollToVisible();
            itemDiv.MouseClick();
            this.DoneSelectingButton();
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

        /// <summary>
        /// Select tag by title
        /// </summary>
        public void ClickSelectButton()
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

            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Search tag by title
        /// </summary>
        /// <param name="title">The title of the tag</param>
        public void SearchTagByTitle(string title)
        {
            this.ClickSelectButton();

            HtmlDiv inputDiv = EM.News.NewsWidgetContentScreen.SearchByTypingDiv
                .AssertIsPresent("Search field div");

            HtmlInputText input = inputDiv.Find.ByExpression<HtmlInputText>("placeholder=Narrow by typing")
            .AssertIsPresent("Search field");

            Manager.Current.Desktop.Mouse.Click(MouseClickType.LeftClick, input.GetRectangle());
            Manager.Current.Desktop.KeyBoard.TypeText(title);
            Manager.Current.ActiveBrowser.WaitUntilReady();
            Manager.Current.ActiveBrowser.WaitForAsyncJQueryRequests();
            Manager.Current.ActiveBrowser.RefreshDomTree();
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
        /// No news items found
        /// </summary>
        public void NoItemsFound()
        {
            HtmlDiv noItemsFound = EM.News.NewsWidgetContentScreen.NoItemsFoundDiv
            .AssertIsPresent("No items found div");

            var isContained = noItemsFound.InnerText.Contains("No items found");
            Assert.IsTrue(isContained, "Message not found");
        }

        /// <summary>
        /// Selects display items published in
        /// </summary>
        /// <param name="option">Selects display items published in</param>
        public void SelectDisplayItemsPublishedIn(string option, string divClass = "radio")
        {
            int position;
            HtmlForm optionsForm = EM.News.NewsWidgetContentScreen.DisplayItemsPublishedIn
                .AssertIsPresent("Selects display items published in");

            List<HtmlDiv> newsDivs = optionsForm.Find.AllByExpression<HtmlDiv>("tagname=div", "class=" + divClass).ToList<HtmlDiv>();

            if (option.Contains("Custom"))
            {
                position = 1;
            }
            else
            {
                position = 0;
            }

            HtmlInputRadioButton optionButton = newsDivs[position].Find.ByExpression<HtmlInputRadioButton>("tagname=input")
                .AssertIsPresent("Which news to display option radio button");

            optionButton.Click();
        }

        /// <summary>
        /// Set From date by typing
        /// </summary>
        /// <param name="dayAgo">Day ago</param>
        public void SetFromDateByTyping(int dayAgo)
        {
            DateTime publicationDateStart = DateTime.UtcNow.AddDays(dayAgo);
            String publicationDateStartFormat = publicationDateStart.ToString("dd-MMMM-yyyy", CultureInfo.CreateSpecificCulture("en-US"));

            HtmlDiv customRangeDiv = EM.News.NewsWidgetContentScreen.CustomRangeDiv
                .AssertIsPresent("Custom range");
            List<HtmlInputText> inputDate = customRangeDiv.Find.AllByExpression<HtmlInputText>("tagname=input", "id=fromInput").ToList<HtmlInputText>();
            
            Manager.Current.Desktop.Mouse.Click(MouseClickType.LeftClick, inputDate[0].GetRectangle());
            Manager.Current.Desktop.KeyBoard.TypeText(publicationDateStartFormat);
            Manager.Current.ActiveBrowser.WaitUntilReady();
            Manager.Current.ActiveBrowser.WaitForAsyncJQueryRequests();
            Manager.Current.ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Set To date by date picker 
        /// </summary>
        /// <param name="dayForward">Day forward</param>
        public void SetToDateByDatePicker(int dayForward)
        {
            DateTime publicationDateEnd = DateTime.UtcNow.AddDays(dayForward);
            String publicationDateEndFormat = publicationDateEnd.ToString("dd", CultureInfo.CreateSpecificCulture("en-US"));

            HtmlDiv customRangeDiv = EM.News.NewsWidgetContentScreen.CustomRangeDiv
                .AssertIsPresent("Custom range");

            List<HtmlSpan> buttonDate = customRangeDiv.Find.AllByExpression<HtmlSpan>("tagname=span", "class=input-group-btn").ToList<HtmlSpan>();
            Manager.Current.Desktop.Mouse.Click(MouseClickType.LeftClick, buttonDate[1].GetRectangle());

            List<HtmlTable> dateTable = customRangeDiv.Find.AllByExpression<HtmlTable>("tagname=table").ToList<HtmlTable>();
            List<HtmlTableCell> toDay = dateTable[1].Find.AllByExpression<HtmlTableCell>("tagname=td", "InnerText=" + publicationDateEndFormat).ToList<HtmlTableCell>();
            HtmlButton buttonToDay;

            if (toDay.Count == 2)
            {
                buttonToDay = toDay[1].Find.ByExpression<HtmlButton>("tagname=button");
            }
            else
            {
                buttonToDay = toDay[0].Find.ByExpression<HtmlButton>("tagname=button");
            }
            
            Manager.Current.Desktop.Mouse.Click(MouseClickType.LeftClick, buttonToDay.GetRectangle());
            Manager.Current.ActiveBrowser.WaitUntilReady();
        }

        /// <summary>
        /// Verify date format
        /// </summary>
        /// <param name="dayAgo">Day ago</param>
        /// <param name="dayForward">Day forward</param>
        public void VerifyCustomDateFormat(int dayAgo, int dayForward)
        {
            DateTime publicationDateStart = DateTime.UtcNow.AddDays(dayAgo);
            String publicationDateStartFormat = publicationDateStart.ToString("dd MMM yyyy", CultureInfo.CreateSpecificCulture("en-US"));

            DateTime publicationDateEnd = DateTime.UtcNow.AddDays(dayForward);
            String publicationDateEndFormat = publicationDateEnd.ToString("dd MMM yyyy", CultureInfo.CreateSpecificCulture("en-US"));

            HtmlSpan selectedItemsSpan = EM.News.NewsWidgetContentScreen.SelectedItemsSpan
                .AssertIsPresent("Date span");

            var isContained = selectedItemsSpan.InnerText.Contains("From " + publicationDateStartFormat + " to " + publicationDateEndFormat);
            Assert.IsTrue(isContained, "Date format is not as expected");
        }
    }
}
