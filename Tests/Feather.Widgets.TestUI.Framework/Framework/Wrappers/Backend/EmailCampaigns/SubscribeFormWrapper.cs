using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.EmailCampaigns
{
    /// <summary>
    /// This is the entry point class for subsribe form widget wrapper.
    /// </summary>
    public class SubscribeFormWrapper : BaseWrapper
    {
        /// <summary>
        /// Select existing page for single item
        /// </summary>
        public void SelectExistingPage()
        {
            HtmlInputRadioButton selectExistingPage = this.EM.EmailCampaigns.SubscribeFormEditScreen.SelectedExistingPage.AssertIsPresent("Selected existing page");
            selectExistingPage.Click();
        }

        /// <summary>
        /// Verifies selected mail list and page in designer.
        /// </summary>
        /// <param name="itemNames">Array of selected item names.</param>
        public void VerifySelectedMailListAndPage(string[] itemNames)
        {
            var divList = this.EM.Widgets.WidgetDesignerContentScreen.SelectedItemsDivList;
            int divListCount = divList.Count;
            Assert.AreNotEqual(0, divListCount, "Count equals 0");

            for (int i = 0; i < divListCount; i++)
            {
                bool isPresentSubscribe = divList[i].InnerText.Contains(itemNames[i]);
                Assert.IsTrue(isPresentSubscribe);
            }
        }

        /// <summary>
        ///  Verify error message for deleted mail list
        /// </summary>
        public void VerifyErrorMessageForDeletedMailList()
        {
            HtmlDiv errorMessage = this.EM.EmailCampaigns.SubscribeFormEditScreen.ErrorMessageMailingList.AssertIsPresent("Error message");
            bool isPresentSubscribe = errorMessage.InnerText.Contains("The selected mailing list has been deleted.");
            Assert.IsTrue(isPresentSubscribe);
        }

        /// <summary>
        ///  Verify error message for deleted page
        /// </summary>
        public void VerifyErrorMessageForDeletedPage()
        {
            HtmlDiv errorMessage = this.EM.EmailCampaigns.SubscribeFormEditScreen.ErrorMessagePage.AssertIsPresent("Error message");
            bool isPresentSubscribe = errorMessage.InnerText.Contains("The selected page has been deleted.");
            Assert.IsTrue(isPresentSubscribe);
        }

        /// <summary>
        /// Selects items in flat selector.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        public void SelectItemsInFlatSelector(params string[] itemNames)
        {
            this.WaitForContentToBeLoaded(itemNames.Length == 0);
            foreach (var itemName in itemNames)
            {
                var itemsToSelect = ActiveBrowser.Find.AllByCustom<HtmlContainerControl>(a => a.InnerText.Equals(itemName));
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
        /// Waits for content to be loaded.
        /// </summary>
        /// <param name="isEmptyScreen">The is empty screen.</param>
        public void WaitForContentToBeLoaded(bool isEmptyScreen)
        {
            ArtOfTest.WebAii.Core.Manager.Current.Wait.For(() => this.IsContentLoadedInMediaSelector(isEmptyScreen), 100000);
        }

        private bool IsContentLoadedInMediaSelector(bool isEmptyScreen)
        {
            bool result = false;
            Manager.Current.ActiveBrowser.RefreshDomTree();
            if (isEmptyScreen)
            {
                var noItemsCreatedMessage = this.EM.EmailCampaigns.SubscribeFormEditScreen.NoItemsCreatedMessage;
                if (noItemsCreatedMessage != null && noItemsCreatedMessage.IsVisible())
                {
                    result = true;
                }
            }
            else
            {
                if (this.EM.EmailCampaigns.SubscribeFormEditScreen.MailingListItems.Count > 0)
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
