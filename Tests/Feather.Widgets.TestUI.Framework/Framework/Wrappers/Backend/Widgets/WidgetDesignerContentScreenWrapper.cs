using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.jQuery;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    public class WidgetDesignerContentScreenWrapper : BaseWrapper
    {
        /// <summary>
        /// Selects which items to display in the widget designer
        /// </summary>
        /// <param name="mode">Which items to display</param>
        public void SelectWhichItemsToDisplay(string mode)
        {
            int position;
            HtmlDiv optionsDiv = EM.Widgets
                                   .WidgetDesignerContentScreen
                                   .WhichItemsToDisplayList
                                   .AssertIsPresent("Which items to display options list");

            List<HtmlDiv> itemsDivs = optionsDiv.Find.AllByExpression<HtmlDiv>("tagname=div", "class=radio").ToList<HtmlDiv>();

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

            HtmlInputRadioButton optionButton = itemsDivs[position].Find.ByExpression<HtmlInputRadioButton>("tagname=input")
                                                                  .AssertIsPresent("Which items to display option radio button");
            optionButton.Click();
        }
 
        /// <summary>
        /// Selects the taxonomy.
        /// </summary>
        /// <param name="taxonomy">The taxonomy.</param>
        public void SelectCheckBox(string taxonomy)
        {
            ActiveBrowser.WaitForAsyncOperations();

            HtmlInputCheckBox optionButton = ActiveBrowser.Find
                                                          .ByExpression<HtmlInputCheckBox>("id=" + taxonomy)
                                                          .AssertIsPresent("Taxonomy option");

            optionButton.Click();
            ActiveBrowser.WaitForAsyncOperations();
        }

        /// <summary>
        /// Provide access to done button
        /// </summary>
        public void DoneSelecting()
        {
            HtmlButton shareButton = EM.Widgets
                                       .WidgetDesignerContentScreen
                                       .DoneSelectingButton
                                       .AssertIsPresent("Done selecting button");
            shareButton.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
        }

        /// <summary>
        /// Selects the item.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        public void SelectItemsInFlatSelector(params string[] itemNames)
        {
            HtmlDiv activeTab = this.EM.Widgets.WidgetDesignerContentScreen.ActiveTab
                                    .AssertIsPresent("active tab");

            foreach (var itemName in itemNames)
            {
                var itemsToSelect = activeTab.Find.AllByCustom<HtmlContainerControl>(a => a.InnerText.Equals(itemName));
                foreach (var item in itemsToSelect)
                {
                    if (item.IsVisible())
                    {
                        item.Wait.ForVisible();
                        item.ScrollToVisible();
                        item.MouseClick();
                        ActiveBrowser.RefreshDomTree();
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Selects the item.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        public void SelectItemsInHierarchicalSelector(params string[] itemNames)
        {
            HtmlDiv activeTab = this.EM.Widgets.WidgetDesignerContentScreen.ActiveTab
                                    .AssertIsPresent("active tab");
            foreach (var itemName in itemNames)
            {
                this.SelectElementInTree(itemName, activeTab);
            }
        }

        /// <summary>
        /// Asserts the single item is selected in hierarchical selector.
        /// </summary>
        /// <param name="name">The name.</param>
        public void AssertSingleItemIsSelectedInHierarchicalSelector(string name)
        {
            ActiveBrowser.Find.ByExpression<HtmlAnchor>("class=active", "InnerText=~" + name, "ng-class=?'active': sfItemSelected({dataItem: dataItem})}")
                                    .AssertIsPresent(name);
        }

        public void CheckBreadcrumbAfterSearchInHierarchicalSelector(string name, string fullName)
        {
            ActiveBrowser.Find.ByExpression<HtmlSpan>("InnerText=" + name, "sf-shrinked-breadcrumb=" + fullName)
                                    .AssertIsPresent("Breadcrumb");
        }

        /// <summary>
        /// Check breadcrumb in flat selector after search.
        /// </summary>
        /// <param name="itemName">name of hierarchical item</param>
        /// <param name="breadcrumb">breadcrumb for given item</param>
        public void CheckBreadcrumbAfterSearchInFlatSelector(string itemName, string breadcrumb)
        {
            ActiveBrowser.Find.ByExpression<HtmlControl>("class=~ng-binding", "InnerText=" + itemName).AssertIsPresent(itemName + " was not present.");
            ActiveBrowser.Find.ByExpression<HtmlSpan>("sf-shrinked-breadcrumb=" + breadcrumb).AssertIsPresent(breadcrumb + " was not present.");
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        public void SaveChanges()
        {
            HtmlButton saveButton = EM.Widgets
                                      .WidgetDesignerContentScreen
                                      .SaveChangesButton
                                      .AssertIsPresent("Save button");
            saveButton.Click();

            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Click select button
        /// </summary>
        public void ClickSelectButton()
        {
            var selectButtons = EM.Widgets.WidgetDesignerContentScreen.SelectButtons;
            foreach (var button in selectButtons)
            {
                if (button.IsVisible())
                {
                    button.Click();
                    break;
                }
            }

            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncOperations();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Click select button by date
        /// </summary>
        public void ClickSelectButtonByDate()
        {
            var selectButtons = EM.Widgets.WidgetDesignerContentScreen.SelectButtonsDate;
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
        public void SearchItemByTitle(string title)
        {
            var activeDialog = this.EM.Widgets.WidgetDesignerContentScreen.ActiveTab.AssertIsPresent("Content container");
            HtmlDiv inputDiv = activeDialog.Find.ByExpression<HtmlDiv>("class=input-group m-bottom-sm");
            
            HtmlInputText input = inputDiv.Find
                                          .ByExpression<HtmlInputText>("placeholder=Narrow by typing")
                                          .AssertIsPresent("Search field");

            Manager.Current.Desktop.Mouse.Click(MouseClickType.LeftClick, input.GetRectangle());
            input.Text = string.Empty;
            Manager.Current.Desktop.KeyBoard.TypeText(title);
            Manager.Current.ActiveBrowser.WaitForAsyncOperations();
            Manager.Current.ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Waits for items count to appear.
        /// </summary>
        /// <param name="expectedCount">The expected items count.</param>
        public void WaitForItemsToAppear(int expectedCount)
        {
            Manager.Current.Wait.For(() => this.ScrollToLatestItemAndCountItems(expectedCount), 150000);
        }

        /// <summary>
        /// Verifies if the items count is as expected.
        /// </summary>
        /// <param name="expected">The expected items count.</param>
        /// <returns>True or false depending on the items count.</returns>
        public bool ScrollToLatestItemAndCountItems(int expected)
        {
            ActiveBrowser.RefreshDomTree();
            var activeDialog = this.EM.Widgets.WidgetDesignerContentScreen.ActiveTab.AssertIsPresent("Content container");

            var items = activeDialog.Find.AllByExpression<HtmlControl>("ng-bind=~bindIdentifierField(item");
            int divsCount = items.Count;

            if (divsCount == 0)
            {
                items = activeDialog.Find.AllByExpression<HtmlControl>("ng-click=itemClicked(item)");
                divsCount = items.Count;
            }

            //// if items count is more than 12 elements, then you need to scroll
            if (divsCount > 12)
            {
                HtmlControl itemsList = EM.Widgets
                                     .WidgetDesignerContentScreen
                                     .ItemsList
                                     .AssertIsPresent("items list");

                List<HtmlControl> itemDiv = itemsList.Find
                                      .AllByExpression<HtmlControl>("class=~ng-scope list-group-item").ToList<HtmlControl>();
                divsCount = itemDiv.Count;

                itemDiv[divsCount - 1].Wait.ForVisible();
                itemDiv[divsCount - 1].ScrollToVisible();
            }

            bool isCountCorrect = expected == divsCount;
            return isCountCorrect;
        }

        /// <summary>
        /// No items items found
        /// </summary>
        public void NoItemsFound()
        {
            HtmlDiv noItemsFound = EM.Widgets
                                     .WidgetDesignerContentScreen
                                     .NoItemsFoundDiv
                                     .AssertIsPresent("No items found div");

            var isContained = noItemsFound.InnerText.Contains("No items found");
            Assert.IsTrue(isContained, "Message not found");
        }

        /// <summary>
        /// Verifies selected items from flat selector in designer.
        /// </summary>
        /// <param name="itemNames">Array of selected item names.</param>
        public void VerifySelectedItemsFromFlatSelector(string[] itemNames)
        {
            var divList = this.EM.Widgets.WidgetDesignerContentScreen.SelectedItemsDivList;
            int divListCount = divList.Count;
            Assert.IsNotNull(divListCount, "Invalid count");
            Assert.AreNotEqual(0, divListCount, "Count equals 0");

            for (int i = 0; i < divListCount; i++)
            {
                Assert.AreEqual(itemNames[i], divList[i].InnerText, itemNames[i] + "not found");
            }
        }

        /// <summary>
        /// Verifies selected items from hierarchical selector in designer.
        /// </summary>
        /// <param name="itemNames">Array of selected item names.</param>
        public void VerifySelectedItemsFromHierarchicalSelector(string[] itemNames)
        {
            var divList = this.EM.Widgets.WidgetDesignerContentScreen.SelectedItemsDivList;
            int divListCount = divList.Count;
            Assert.IsNotNull(divListCount, "Invalid count");
            Assert.AreNotEqual(0, divListCount, "Count equals 0");

            for (int i = 0; i < divListCount; i++)
            {
                divList[i].Find.ByAttributes<HtmlSpan>("sf-shrinked-breadcrumb=" + itemNames[i]).AssertIsPresent("Span for " + itemNames[i] + " was not present.");
            }
        }

        /// <summary>
        /// Checks the notification in selected tab.
        /// </summary>
        /// <param name="itemNames">The item names.</param>
        public void CheckNotificationInSelectedTab(int expectedCout)
        {
            var span = this.EM.Widgets.WidgetDesignerContentScreen.Find.ByExpression<HtmlSpan>("class=badge ng-binding", string.Format("InnerText=~{0}", expectedCout));
            span.AssertIsPresent("item name not present");
        }

        /// <summary>
        /// Opens the selected tab.
        /// </summary>
        public void OpenSelectedTab()
        {
            HtmlAnchor selectedTab = this.EM.Widgets.WidgetDesignerContentScreen.SelectedTab
                                         .AssertIsPresent("selected tab");
            selectedTab.Click();
            ActiveBrowser.WaitForAsyncRequests();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Opens the all tab.
        /// </summary>
        public void OpenAllTab()
        {
            HtmlAnchor allTab = this.EM.Widgets.WidgetDesignerContentScreen.AllTab
                                    .AssertIsPresent("all tab");

            allTab.Click();
            ActiveBrowser.WaitForAsyncRequests();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Selects display items published in
        /// </summary>
        /// <param name="option">Selects display items published in</param>
        public void SelectDisplayItemsPublishedIn(string option, string divClass = "radio")
        {
            int position;
            HtmlForm optionsForm = EM.Widgets
                                     .WidgetDesignerContentScreen
                                     .DisplayItemsPublishedIn
                                     .AssertIsPresent("Selects display items published in");

            List<HtmlDiv> itemsDivs = optionsForm.Find.AllByExpression<HtmlDiv>("tagname=div", "class=" + divClass).ToList<HtmlDiv>();

            if (option.Contains("Custom"))
            {
                position = 1;
            }
            else
            {
                position = 0;
            }

            HtmlInputRadioButton optionButton = itemsDivs[position].Find.ByExpression<HtmlInputRadioButton>("tagname=input")
                                                                  .AssertIsPresent("Which items to display option radio button");

            optionButton.Click();
        }

        /// <summary>
        /// Set From date by typing to custom date selector
        /// </summary>
        /// <param name="dayAgo">Day ago</param>
        public void SetFromDateByTyping(int dayAgo)
        {
            DateTime publicationDateStart = DateTime.UtcNow.AddDays(dayAgo);
            string publicationDateStartFormat = publicationDateStart.ToString("dd-MMMM-yyyy", CultureInfo.CreateSpecificCulture("en-US"));

            List<HtmlInputText> inputDate = ActiveBrowser.Find.AllByExpression<HtmlInputText>("tagname=input", "id=fromInput").ToList<HtmlInputText>();

            Manager.Current.Desktop.Mouse.Click(MouseClickType.LeftClick, inputDate[0].GetRectangle());
            Manager.Current.Desktop.KeyBoard.TypeText(publicationDateStartFormat);
            Manager.Current.ActiveBrowser.WaitUntilReady();
            Manager.Current.ActiveBrowser.WaitForAsyncJQueryRequests();
            Manager.Current.ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Set To date by date picker to custom date selector
        /// </summary>
        /// <param name="dayForward">Day forward</param>
        public void SetToDateByDatePicker(int dayForward)
        {
            DateTime publicationDateEnd = DateTime.UtcNow.AddDays(dayForward);
            string publicationDateEndFormat = publicationDateEnd.ToString("dd", CultureInfo.CreateSpecificCulture("en-US"));

            List<HtmlSpan> buttonDate = ActiveBrowser.Find.AllByExpression<HtmlSpan>("tagname=span", "class=input-group-btn").ToList<HtmlSpan>();
            Manager.Current.Desktop.Mouse.Click(MouseClickType.LeftClick, buttonDate[1].GetRectangle());

            List<HtmlTable> dateTable = ActiveBrowser.Find.AllByExpression<HtmlTable>("tagname=table", "role=grid").ToList<HtmlTable>();
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
        ///  Add hour to custom date selector
        /// </summary>
        /// <param name="hour">Hour value to select</param>
        /// <param name="isFrom">Is from or to hour</param>
        public void AddHour(string hour, bool isFrom)
        {
            List<HtmlAnchor> hourAnchor = ActiveBrowser.Find.AllByExpression<HtmlAnchor>("tagname=a", "InnerText=Add hour").ToList<HtmlAnchor>();
            int fromOrTo = 0;

            if (isFrom.Equals(false))
            {
                fromOrTo = 1;
            }

            Manager.Current.Desktop.Mouse.Click(MouseClickType.LeftClick, hourAnchor[fromOrTo].GetRectangle());
            Manager.Current.ActiveBrowser.WaitUntilReady();
            Manager.Current.ActiveBrowser.WaitForAsyncJQueryRequests();
            Manager.Current.ActiveBrowser.RefreshDomTree();

            List<HtmlSelect> hoursSelector = ActiveBrowser.Find.AllByExpression<HtmlSelect>("tagname=select", "ng-change=updateHours(hstep)").ToList<HtmlSelect>();
            hoursSelector[fromOrTo].SelectByValue(hour);
            hoursSelector[fromOrTo].AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            hoursSelector[fromOrTo].AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
            Manager.Current.ActiveBrowser.WaitUntilReady();
            Manager.Current.ActiveBrowser.WaitForAsyncJQueryRequests();
            Manager.Current.ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        ///  Add minute to custom date selector
        /// </summary>
        /// <param name="minute">Minute value to select</param>
        /// <param name="isFrom">Is from or to minute</param>
        public void AddMinute(string minute, bool isFrom)
        {
            List<HtmlAnchor> minutesAnchor = ActiveBrowser.Find.AllByExpression<HtmlAnchor>("tagname=a", "InnerText=Add minutes").ToList<HtmlAnchor>();
            int fromOrTo = 0;

            if (isFrom.Equals(false))
            {
                fromOrTo = 1;
            }

            Manager.Current.Desktop.Mouse.Click(MouseClickType.LeftClick, minutesAnchor[fromOrTo].GetRectangle());
            Manager.Current.ActiveBrowser.WaitUntilReady();
            Manager.Current.ActiveBrowser.WaitForAsyncJQueryRequests();
            Manager.Current.ActiveBrowser.RefreshDomTree();

            List<HtmlSelect> minutesSelector = ActiveBrowser.Find.AllByExpression<HtmlSelect>("tagname=select", "ng-change=updateMinutes(mstep)").ToList<HtmlSelect>();
            minutesSelector[fromOrTo].Click();
            minutesSelector[fromOrTo].SelectByValue(minute);
            minutesSelector[fromOrTo].AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            minutesSelector[fromOrTo].AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
            Manager.Current.ActiveBrowser.WaitUntilReady();
            Manager.Current.ActiveBrowser.WaitForAsyncJQueryRequests();
            Manager.Current.ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Verify message in items widget - when items module is deactivated
        /// </summary>
        public void CheckInactiveitemsWidget()
        {
            HtmlDiv optionsDiv = EM.Widgets
                                   .WidgetDesignerContentScreen
                                   .InactiveWidget
                                   .AssertIsPresent("Inactive widget");
            var isContained = optionsDiv.InnerText.Contains("This widget doesn't work, becauseitemsmodule has been deactivated.");
            Assert.IsTrue(isContained, "Message not found");
        }

        /// <summary>
        /// Opens the Single item settings tab.
        /// </summary>
        public void SingleItemSettingsTab()
        {
            HtmlAnchor singleTab = this.EM.Widgets.WidgetDesignerContentScreen.SingleItemSetting
                                    .AssertIsPresent("Single item settings tab");

            singleTab.Click();
            ActiveBrowser.WaitForAsyncRequests();
            ActiveBrowser.RefreshDomTree();
        }
   
        /// <summary>
        /// Lists the settings tab.
        /// </summary>
        public void SelectListSettingsTab()
        {
            HtmlAnchor listSettingsTab = this.EM.Widgets.WidgetDesignerContentScreen.ListSettings
                                    .AssertIsPresent("List settings tab");

            listSettingsTab.Click();
            ActiveBrowser.WaitForAsyncRequests();
            ActiveBrowser.RefreshDomTree();
        }
     
        /// <summary>
        /// Selects the sorting option.
        /// </summary>
        /// <param name="option">The option.</param>
        public void SelectSortingOption(string option)
        {
            HtmlSelect selectDetailTemplate = this.EM.Widgets.WidgetDesignerContentScreen.SortingOptionsDropdown
                                    .AssertIsPresent("Sorting dropdown");

            selectDetailTemplate.Click();
            selectDetailTemplate.SelectByValue(option);
            selectDetailTemplate.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            selectDetailTemplate.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
            Manager.Current.ActiveBrowser.WaitUntilReady();
            Manager.Current.ActiveBrowser.WaitForAsyncJQueryRequests();
            Manager.Current.ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Select detail template
        /// </summary>
        public void SelectDetailTemplate(string templateName)
        {
            HtmlSelect selectDetailTemplate = this.EM.Widgets.WidgetDesignerContentScreen.SelectDetailTemplate
                                    .AssertIsPresent("Detail template select");

            selectDetailTemplate.Click();
            selectDetailTemplate.SelectByValue(templateName);
            selectDetailTemplate.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            selectDetailTemplate.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
            Manager.Current.ActiveBrowser.WaitUntilReady();
            Manager.Current.ActiveBrowser.WaitForAsyncJQueryRequests();
            Manager.Current.ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Select existing page for single item
        /// </summary>
        public void SelectExistingPage()
        {
            HtmlInputRadioButton selectExistingPage = this.EM.Widgets.WidgetDesignerContentScreen.SelectedExistingPage.AssertIsPresent("Selected existing page");
            selectExistingPage.Click();

            Manager.Current.ActiveBrowser.WaitUntilReady();
            Manager.Current.ActiveBrowser.WaitForAsyncOperations();
            Manager.Current.ActiveBrowser.RefreshDomTree();
        }
       
        /// <summary>
        /// Selects the provider.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        public void SelectProvider(string providerName)
        {
            HtmlAnchor providerDropDown = this.EM.Widgets.WidgetDesignerContentScreen.ProviderDropDown
                                    .AssertIsPresent("Provider DropDown");

            providerDropDown.Click();
            ActiveBrowser.WaitForAsyncRequests();
            ActiveBrowser.RefreshDomTree();

            HtmlUnorderedList providerList = this.EM.Widgets.WidgetDesignerContentScreen.ProvidersList
                                    .AssertIsPresent("Provider DropDown");

            HtmlAnchor provider = providerList.Find.ByCustom<HtmlAnchor>(a => a.InnerText.Equals(providerName))
                                    .AssertIsPresent("Provider DropDown");

            provider.Click();
            ActiveBrowser.WaitForAsyncRequests();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Reorders the selected items.
        /// </summary>
        /// <param name="expectedOrder">The expected order.</param>
        /// <param name="selectedItemNames">The selected item names.</param>
        public void ReorderSelectedItems(string[] expectedOrder, string[] selectedItemNames, Dictionary<int, int> reorderedIndexMapping)
        {
            var activeDialog = this.EM.Widgets.WidgetDesignerContentScreen.ActiveTab.AssertIsPresent("Content container");
            var divList = activeDialog.Find.AllByExpression<HtmlDiv>("ng-repeat=item in items");

            for (int i = 0; i < divList.Count; i++)
            {
                Assert.AreEqual(selectedItemNames[i], divList[i].InnerText, selectedItemNames[i] + "is not positioned correctly in Selected tab");
            }

            var spanList = this.EM.Widgets.WidgetDesignerContentScreen.Find.AllByExpression<HtmlSpan>("class=handler list-group-item-drag");

            foreach (KeyValuePair<int, int> reorderingPair in reorderedIndexMapping)
            {
                spanList[reorderingPair.Key].DragTo(spanList[reorderingPair.Value]);
            }

            activeDialog.Refresh();
            var reorderedDivList = activeDialog.Find.AllByExpression<HtmlDiv>("ng-repeat=item in items");

            for (int i = 0; i < reorderedDivList.Count; i++)
            {
                Assert.AreEqual(expectedOrder[i], reorderedDivList[i].InnerText, expectedOrder[i] + " is not reordered correctly");
            }
        }

        private void SelectElementInTree(string itemName, HtmlDiv activeTab)
        {
            var element = activeTab.Find.ByExpression<HtmlSpan>("innertext=" + itemName);

            if (element != null && element.IsVisible())
            {
                element.Click();
                ActiveBrowser.RefreshDomTree();
            }
            else
            {
                var arrows = this.EM.Widgets.WidgetDesignerContentScreen.Find.AllByCustom<HtmlSpan>(a => a.CssClass.Contains("k-icon k-plus"));
                Assert.AreNotEqual(0, arrows.Count, "No arrows appear");

                this.SearchAndSelectElementByExpandingArrows(arrows, element, itemName, activeTab);

                this.isHierarchicalItemFound = false;
            }
        }

        private void SearchAndSelectElementByExpandingArrows(ICollection<HtmlSpan> arrows, HtmlSpan element, string itemName, HtmlDiv activeTab)
        {
            if (this.isHierarchicalItemFound)
            {
                return;
            }

            foreach (var arrow in arrows)
            {
                if (this.isHierarchicalItemFound)
                {
                    return;
                }

                if (arrow.IsVisible())
                {
                    arrow.Click();
                    activeTab.Refresh();
                    element = activeTab.Find.ByCustom<HtmlSpan>(a => a.InnerText.Equals(itemName));
                    if (element != null && element.IsVisible())
                    {
                        element.Click();
                        this.isHierarchicalItemFound = true;
                    }
                    else
                    {
                        var newArrows = this.EM.Widgets.WidgetDesignerContentScreen.Find.AllByCustom<HtmlSpan>(a => a.CssClass.Contains("k-icon k-plus"));
                        if (newArrows.Count != 0)
                        {
                            this.SearchAndSelectElementByExpandingArrows(newArrows, element, itemName, activeTab);
                        }
                        else
                        {
                            throw new Exception(itemName + " " + "not found");
                        }
                    }
                }
            }
        }

        private bool isHierarchicalItemFound = false;
    }
}
