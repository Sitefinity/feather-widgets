using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;

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
            HtmlDiv errorMessage = this.EM.EmailCampaigns.SubscribeFormEditScreen.ErrorMessage.AssertIsPresent("Error message");
            bool isPresentSubscribe = errorMessage.InnerText.Contains("The selected mailing list has been deleted.");
            Assert.IsTrue(isPresentSubscribe);
        }

        /// <summary>
        /// Selects items in flat selector.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        public void SelectItemsInFlatSelector(params string[] itemNames)
        {
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
    }
}
