using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.jQuery;

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
            HtmlSelect selectDetailTemplate = this.EM.Widgets.WidgetDesignerContentScreen.SortingOptionsDropdown
                                                  .AssertIsPresent("Sorting dropdown");

            selectDetailTemplate.Click();
            selectDetailTemplate.SelectByValue(option);
            selectDetailTemplate.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            selectDetailTemplate.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
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
    }
}