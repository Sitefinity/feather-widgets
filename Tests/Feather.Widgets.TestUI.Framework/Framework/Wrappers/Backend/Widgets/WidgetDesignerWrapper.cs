using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.jQuery;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Widgets;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    public class WidgetDesignerWrapper : BaseWrapper
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
            HtmlInputCheckBox optionButton = ActiveBrowser.Find
                                                          .ByExpression<HtmlInputCheckBox>("id=" + taxonomy)
                                                          .AssertIsPresent("Taxonomy option");
            optionButton.Click();
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
        /// Click select button
        /// </summary>
        /// <param name="selectButtonNumber">select button number among all select buttons starting from 0</param>
        public void ClickSelectButton(int selectButtonNumber)
        {
            var selectButtons = EM.Widgets.WidgetDesignerContentScreen.SelectButtons;
            Assert.IsNotNull(selectButtons);
            Assert.IsTrue(selectButtons.Count != 0, "no select numbers found");
            Assert.IsTrue(selectButtonNumber < selectButtons.Count, "number is higher than the length of buttons list");

            var selectButton = selectButtons.ElementAt(selectButtonNumber);
            Assert.IsTrue(selectButton.IsVisible());

            selectButton.Click();
            ActiveBrowser.WaitForAsyncOperations();
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

            List<HtmlDiv> itemsDivs = optionsForm.Find.AllByExpression<HtmlDiv>("tagname=div", "class=~" + divClass).ToList<HtmlDiv>();

            switch (option)
            {
                case "Any time":
                    position = 0;
                    break;
                case "Last":
                    position = 1;
                    break;
                case "Custom range...":
                    position = 2;
                    break;
                default:
                    position = 0;
                    break;
            }

            HtmlInputRadioButton optionButton = itemsDivs[position].Find.ByExpression<HtmlInputRadioButton>("tagname=input")
                                                                   .AssertIsPresent("Display itesm published in radio button");

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
        /// Switches to Single item settings tab.
        /// </summary>
        public void SwitchToSingleItemSettingsTab()
        {
            HtmlAnchor singleTab = this.EM.Widgets.WidgetDesignerContentScreen.SingleItemSetting
                                       .AssertIsPresent("Single item settings tab");

            singleTab.Click();
            ActiveBrowser.WaitForAsyncRequests();
            ActiveBrowser.RefreshDomTree();
        }
   
        /// <summary>
        /// Switches to List settings tab.
        /// </summary>
        public void SwitchToListSettingsTab()
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
            HtmlSelect sortingDropdown = this.EM.Widgets.WidgetDesignerContentScreen.SortingOptionsDropdown
                                                  .AssertIsPresent("Sorting dropdown");

            sortingDropdown.Click();
            sortingDropdown.SelectByValue(option);
            sortingDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            sortingDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }

        /// <summary>
        /// Verifies sorting options in Sorting dropdown
        /// </summary>
        /// <param name="sortingOptions">Sorting options to be verified.</param>
        public void VerifySortingOptions(string[] sortingOptions)
        {
            HtmlSelect sortingDropdown = this.EM.Widgets.WidgetDesignerContentScreen.SortingOptionsDropdown
                                                  .AssertIsPresent("Sorting dropdown");

            Assert.AreEqual(sortingOptions.Count(), sortingDropdown.Options.Count(), "Sorting options are not equal to expected options");

            for (int i = 0; i < sortingOptions.Count(); i++)
            {
                Assert.AreEqual(sortingOptions[i], sortingDropdown.Options[i].Text, "Sorting option is different");
            }
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
        }

        /// <summary>
        /// Select existing page for single item
        /// </summary>
        public void SelectExistingPage()
        {
            HtmlInputRadioButton selectExistingPage = this.EM.Widgets.WidgetDesignerContentScreen.SelectedExistingPage.AssertIsPresent("Selected existing page");
            selectExistingPage.Click();
        }

        /// <summary>
        /// Select auto-generated page for single item
        /// </summary>
        public void SelectAutoGeneratedPage()
        {
            HtmlInputRadioButton autoGeneratedPageRadioButton = this.EM.Widgets.WidgetDesignerContentScreen.AutoGeneratedPage.AssertIsPresent("Auto-generated page");
            autoGeneratedPageRadioButton.Click();
        }

        /// <summary>
        /// Verifies Open single items options
        /// </summary>
        public void VerifyOpenSingleItemsOptions()
        {
            this.EM.Widgets.WidgetDesignerContentScreen.SelectedExistingPage.AssertIsPresent("Selected existing page");
            this.EM.Widgets.WidgetDesignerContentScreen.AutoGeneratedPage.AssertIsPresent("Auto-generated page");
        }

        /// <summary>
        /// Selects the provider.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        public void SelectProvider(string providerName)
        {
            HtmlSelect providerDropDown = this.EM.Widgets.WidgetDesignerContentScreen.ProviderDropDown
                                              .AssertIsPresent("Provider DropDown");

            providerDropDown.SelectByText(providerName);
            providerDropDown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            providerDropDown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
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
            int notVisibleItemNames = 0;

            for (int i = 0; i < divListCount; i++)
            {
                if (!divList[i].IsVisible())
                {
                    notVisibleItemNames++;
                }
                else
                {
                    divList[i].Find.ByAttributes<HtmlSpan>("sf-shrinked-breadcrumb=" + itemNames[i - notVisibleItemNames]).AssertIsPresent("Span for " + itemNames[i - notVisibleItemNames] + " was not present.");                 
                }
            }
        }

        /// <summary>
        /// Selects the radio button option.
        /// </summary>
        /// <param name="optionId">The option id.</param>
        public void SelectRadioButtonOption(WidgetDesignerRadioButtonIds optionId)
        {
            HtmlFindExpression expression = new HtmlFindExpression("tagname=input", "id=" + optionId);
            ActiveBrowser.WaitForElement(expression, 60000, false);
            HtmlInputRadioButton radioButton = ActiveBrowser.Find.ByExpression<HtmlInputRadioButton>("tagname=input", "id=" + optionId)
                  .AssertIsPresent("radio button");

            radioButton.Click();
        }

        /// <summary>
        /// Verifies the checked radio button option.
        /// </summary>
        /// <param name="optionId">The option id.</param>
        public void VerifyCheckedRadioButtonOption(WidgetDesignerRadioButtonIds optionId)
        {
            HtmlFindExpression expression = new HtmlFindExpression("tagname=input", "id=" + optionId);
            ActiveBrowser.WaitForElement(expression, 60000, false);
            HtmlInputRadioButton radioButton = ActiveBrowser.Find.ByExpression<HtmlInputRadioButton>("tagname=input", "id=" + optionId)
                  .AssertIsPresent("radio button");

            Assert.IsTrue(radioButton.Checked);
        }

        /// <summary>
        /// Expands the narrow selection by arrow.
        /// </summary>
        public void ExpandNarrowSelectionByArrow()
        {
            HtmlAnchor arrow = this.EM.Widgets.WidgetDesignerContentScreen.NarrowSelectionByArrow
                  .AssertIsPresent("radio button");

            arrow.Click();
        }

        /// <summary>
        /// Changes the paging or limit value.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="selectedListSettingOption">The selected list setting option.</param>
        public void ChangePagingOrLimitValue(string number, string selectedListSettingOption)
        {
            HtmlInputText itemsTextBox = ActiveBrowser.Find.ByExpression<HtmlInputText>("ng-disabled=~" + selectedListSettingOption)
                 .AssertIsPresent("text box");

            itemsTextBox.Text = string.Empty;
            itemsTextBox.Text = number;
            itemsTextBox.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }

        /// <summary>
        /// Sorts the options selector.
        /// </summary>
        /// <param name="optionValue">The option value.</param>
        public void SelectOptionInSortingSelector(string optionValue)
        {
            HtmlSelect selector = this.EM.Widgets.WidgetDesignerContentScreen.SortImagesSelector
               .AssertIsPresent("sorting selector");

            selector.SelectByValue(optionValue);
            selector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            selector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }

        /// <summary>
        /// Selects the option in list template selector.
        /// </summary>
        /// <param name="optionValue">The option value.</param>
        public void SelectOptionInListTemplateSelector(string optionValue)
        {
            HtmlSelect selector = this.EM.Widgets.WidgetDesignerContentScreen.ListTemplateSelector
               .AssertIsPresent("List template selector");

            selector.SelectByValue(optionValue);
            selector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            selector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }

        /// <summary>
        /// Selects option from thumbnail selector.
        /// </summary>
        /// <param name="optionValue">Option value.</param>
        public void SelectOptionInThumbnailSelector(string optionValue, bool isListSettingsTabSelected = true)
        {
            HtmlSelect selector = null;
            if (isListSettingsTabSelected)
            {
                selector = this.EM.Widgets.WidgetDesignerContentScreen.ThumbnailSelector.FirstOrDefault().AssertIsPresent("Thumbnail selector");
            }
            else
            {
                selector = this.EM.Widgets.WidgetDesignerContentScreen.ThumbnailSelector.LastOrDefault().AssertIsPresent("Image selector");
            }

            selector.SelectByText(optionValue);
            selector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            selector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }

        /// <summary>
        /// Verifies paging and limit options
        /// </summary>
        public void VerifyPagingAndLimitOptions()
        {
            this.EM.Widgets.WidgetDesignerContentScreen.UsePaging.AssertIsPresent("Use paging radio button");
            this.EM.Widgets.WidgetDesignerContentScreen.UseLimit.AssertIsPresent("Use limit radio button");
            this.EM.Widgets.WidgetDesignerContentScreen.NoLimitAndPaging.AssertIsPresent("No limit and paging");
        }

        /// <summary>
        /// Verifies filter by category, tag, date options
        /// </summary>
        public void VerifyFilterByCategoryTagDateOptions()
        {
            this.EM.Widgets.WidgetDesignerContentScreen.FilterByCategory.AssertIsPresent("Filter by category checkbox");
            this.EM.Widgets.WidgetDesignerContentScreen.FilterByTag.AssertIsPresent("Filter by tag checkbox");
            this.EM.Widgets.WidgetDesignerContentScreen.FilterByDate.AssertIsPresent("Filter by date checkbox");
        }

        /// <summary>
        /// Apply css class
        /// </summary>
        /// <param name="cssClassName">css class name</param>
        public void ApplyCssClasses(string cssClassName)
        {
            HtmlAnchor moreOptions = this.EM.Widgets.WidgetDesignerContentScreen.MoreOptionsSpan.AssertIsPresent("More options span");
            moreOptions.Click();
            HtmlInputText cssClassesTextbox = this.EM.Widgets.WidgetDesignerContentScreen.CssClassesTextbox.AssertIsPresent("Css classes textbox");
            cssClassesTextbox.Text = cssClassName;
            cssClassesTextbox.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }
    }
}