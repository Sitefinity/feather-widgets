using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    public class SelectorsWrapper : BaseWrapper
    {
        /// <summary>
        /// Clicks Done selecting button.
        /// </summary>
        public void DoneSelecting()
        {
            HtmlButton shareButton = this.EM.Selectors.SelectorsScreen.DoneSelectingButton.AssertIsPresent("Done selecting button");

            shareButton.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
        }

        /// <summary>
        /// Selects items in flat selector.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        public void SelectItemsInFlatSelector(params string[] itemNames)
        {
            HtmlDiv activeTab = this.EM.Selectors.SelectorsScreen.ActiveTab.AssertIsPresent("active tab");

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
        /// Selects items in hierarchical selector.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        public void SelectItemsInHierarchicalSelector(params string[] itemNames)
        {
            HtmlDiv activeTab = this.EM.Selectors.SelectorsScreen.ActiveTab.AssertIsPresent("active tab");

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
            ActiveBrowser.Find
                         .ByExpression<HtmlAnchor>("class=active", "InnerText=~" + name, "ng-class=?'active': sfItemSelected({dataItem: dataItem})}")
                         .AssertIsPresent(name);
        }

        /// <summary>
        /// Verifies breadcrumb after search.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="fullName">Full name of item with parents</param>
        public void CheckBreadcrumbAfterSearchInHierarchicalSelector(string name, string fullName)
        {
            ActiveBrowser.Find
                         .ByExpression<HtmlSpan>("InnerText=" + name, "sf-shrinked-breadcrumb=" + fullName)
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
        /// Search item by title
        /// </summary>
        /// <param name="title">The title of the tag</param>
        public void SearchItemByTitle(string title)
        {
            var activeDialog = this.EM.Selectors.SelectorsScreen.ActiveTab.AssertIsPresent("Content container");
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
            Manager.Current.Wait.For(() => this.ScrollToLatestItemAndCountItems(expectedCount), 50000);
        }

        /// <summary>
        /// Verifies if the items count is as expected.
        /// </summary>
        /// <param name="expected">The expected items count.</param>
        /// <returns>True or false depending on the items count.</returns>
        public bool ScrollToLatestItemAndCountItems(int expected)
        {
            ActiveBrowser.RefreshDomTree();
            var activeDialog = this.EM.Selectors.SelectorsScreen.ActiveTab.AssertIsPresent("Content container");

            var items = activeDialog.Find.AllByExpression<HtmlControl>("ng-bind=~bindIdentifierField(item");
            int divsCount = items.Count;

            if (divsCount == 0)
            {
                items = activeDialog.Find.AllByExpression<HtmlControl>("ng-click=itemClicked(item)");
                divsCount = items.Count;
            }

            // If items count is more than 12 elements, then you need to scroll
            if (divsCount > 12)
            {
                HtmlControl itemsList = EM.Widgets
                                          .WidgetDesignerContentScreen
                                          .ItemsList
                                          .AssertIsPresent("items list");

                List<HtmlControl> itemDiv = itemsList.Find
                                                     .AllByExpression<HtmlControl>("class=~ng-scope list-group-item")
                                                     .ToList<HtmlControl>();
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
        public void VerifyNoItemsFound()
        {
            HtmlDiv noItemsFound = this.EM.Selectors.SelectorsScreen.NoItemsFoundDiv.AssertIsPresent("No items found div");

            var isContained = noItemsFound.InnerText.Contains("No items found");
            Assert.IsTrue(isContained, "Message not found");
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
            HtmlAnchor selectedTab = this.EM.Selectors.SelectorsScreen.SelectedTab.AssertIsPresent("selected tab");
            selectedTab.Click();
            ActiveBrowser.WaitForAsyncRequests();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Opens the all tab.
        /// </summary>
        public void OpenAllTab()
        {
            HtmlAnchor allTab = this.EM.Selectors.SelectorsScreen.AllTab.AssertIsPresent("all tab");

            allTab.Click();
            ActiveBrowser.WaitForAsyncRequests();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Opens the external urls tab.
        /// </summary>
        public void OpenExternalUrlsTab()
        {
            HtmlAnchor externalUrlsTab = this.EM.Selectors.SelectorsScreen.ExternalUrlsTab.AssertIsPresent("external urls tab");

            externalUrlsTab.Click();
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
            var activeDialog = this.EM.Selectors.SelectorsScreen.ActiveTab.AssertIsPresent("Content container");
            var divList = activeDialog.Find.AllByExpression<HtmlDiv>("ng-repeat=item in items");

            for (int i = 0; i < divList.Count; i++)
            {
                Assert.AreEqual(selectedItemNames[i], divList[i].InnerText, selectedItemNames[i] + "is not positioned correctly in Selected tab");
            }

            var spanList = this.EM.Widgets.WidgetDesignerContentScreen.Find.AllByExpression<HtmlSpan>("class=handler list-group-item-drag");

            foreach (KeyValuePair<int, int> reorderingPair in reorderedIndexMapping)
            {
                Rectangle dragToRectangle = spanList[reorderingPair.Value].Parent<HtmlDiv>().GetRectangle();

                Point dragToPoint = new Point(dragToRectangle.X, dragToRectangle.Y);

                spanList[reorderingPair.Key].DragTo(dragToPoint);

                // We need a pause before the next drag and drop.
                Thread.Sleep(10);   
            }

            activeDialog.Refresh();
            var reorderedDivList = activeDialog.Find.AllByExpression<HtmlDiv>("ng-repeat=^item in currentItems");
            Assert.IsNotNull(reorderedDivList, "reorderedDivList was not found.");

            for (int i = 0; i < reorderedDivList.Count; i++)
            {
                Assert.AreEqual(expectedOrder[i], reorderedDivList[i].InnerText, expectedOrder[i] + " is not reordered correctly");
            }
        }

        /// <summary>
        /// Verifies the page status and icon.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        /// <param name="status">The status.</param>
        public void VerifyPageStatusAndIcon(string pageName, string status)
        {
            var pageLink = this.EM.Selectors.SelectorsScreen.PageLink(pageName).AssertIsPresent("page link");
            string statusIconClass = "pull-left icon-item-" + status.ToString().ToLower();

            pageLink.Find.ByExpression<HtmlContainerControl>("tagname=i", "class=" + statusIconClass).AssertIsPresent("icon");
           
            pageLink.Find.ByExpression<HtmlSpan>("tagname=span", "class=small text-muted ng-binding", "innertext=" + status).AssertIsPresent("status text");
        }

        /// <summary>
        /// Verifies the page status and icon after search.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        /// <param name="status">The status.</param>
        public void VerifyPageStatusAndIconAfterSearch(string pageName, string status)
        {
            var pageDiv = this.EM.Selectors.SelectorsScreen.PageDiv(pageName).AssertIsPresent("page div");
            string statusIconClass = "pull-left icon-item-" + status.ToLower();

            pageDiv.Find.ByExpression<HtmlContainerControl>("tagname=i", "class=" + statusIconClass).AssertIsPresent("icon");

            pageDiv.Find.ByExpression<HtmlSpan>("tagname=span", "class=~u-db small ng-binding", "innertext=" + status).AssertIsPresent("status text");
        }

        /// <summary>
        /// Enters the external urls title.
        /// </summary>
        /// <param name="titleContent">Content of the title.</param>
        /// <param name="urlContent">Content of the URL.</param>
        /// <param name="isFirstRowSelected">The is first row selected.</param>
        public void EnterExternalUrlsInfo(string titleContent, string urlContent, bool isFirstRowSelected = true)
        {
            HtmlInputText title;
            HtmlInputText url;
            if (isFirstRowSelected)
            {
                title = this.EM.Selectors.SelectorsScreen.AllExternalUrlsTitles.FirstOrDefault().AssertIsPresent("first title");
            }
            else
            {
                title = this.EM.Selectors.SelectorsScreen.AllExternalUrlsTitles.LastOrDefault().AssertIsPresent("second title");
            }

            title.ScrollToVisible();
            title.Focus();
            title.MouseClick();
            title.Text = string.Empty;
            Manager.Current.Desktop.KeyBoard.TypeText(titleContent);

            if (isFirstRowSelected)
            {
                url = this.EM.Selectors.SelectorsScreen.AllExternalUrlsUrls.FirstOrDefault().AssertIsPresent("first url");
            }
            else
            {
                url = this.EM.Selectors.SelectorsScreen.AllExternalUrlsUrls.LastOrDefault().AssertIsPresent("second url");
            }

            url.ScrollToVisible();
            url.Focus();
            url.MouseClick();
            url.Text = string.Empty;
            Manager.Current.Desktop.KeyBoard.TypeText(urlContent);
        }

        /// <summary>
        /// Presses the add external urls button.
        /// </summary>
        public void PressAddExternalUrlsButton()
        {
            HtmlInputButton addExternalUrlsButton = this.EM.Selectors.SelectorsScreen.AddExternalUrlButton.AssertIsPresent("Add external urls button button");

            addExternalUrlsButton.Click();
            ActiveBrowser.WaitForAsyncRequests();
        }

        /// <summary>
        /// Removes the external URL.
        /// </summary>
        public void RemoveExternalUrl()
        {
            HtmlAnchor removeIcon = this.EM.Selectors.SelectorsScreen.RemoveExternalUrlsIcon.AssertIsPresent("remove icon");

            removeIcon.Click();
            ActiveBrowser.WaitForAsyncRequests();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Selects the open in new window option.
        /// </summary>
        /// <param name="isSelected">The is selected.</param>
        public void SelectOpenInNewWindowOption(bool isSelected = true)
        {
            HtmlInputCheckBox checkbox = this.EM.Selectors
                                                .SelectorsScreen
                                                .OpenExternalUrlsInNewTabCheckbox
                                                .AssertIsPresent("open in new window");
            if (isSelected && !checkbox.Checked)
            {
                checkbox.Click();               
            }
            else if (!isSelected && checkbox.Checked) 
            {
                checkbox.Click();                
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