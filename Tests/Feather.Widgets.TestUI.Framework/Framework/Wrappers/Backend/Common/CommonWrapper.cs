using System.Threading;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    /// <summary>
    /// Common wrapper for non specific actions
    /// </summary>
    public class CommonWrapper : BaseWrapper
    {
        /// <summary>
        /// Adds the provider to the site.
        /// </summary>
        /// <param name="elementId">The element id.</param>
        /// <param name="providerName">Name of the provider.</param>
        public void AddProviderToTheSite(string elementId, string providerName)
        {
            string elementFullId = "_configureModulesView_change_Telerik_Sitefinity_Modules_" + elementId;
            BAT.Wrappers().Backend().Multisite().MultisiteWrapper().NavigateToManageSites();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncOperations();
            ActiveBrowser.RefreshDomTree();
            this.ClickOnActionsMenuItem(elementFullId, "Configure Modules");
            var linkVar = ActiveBrowser.Find.ByExpression<HtmlAnchor>("id=?" + elementFullId).AssertIsPresent("linkVar");
            linkVar.Click();
            ActiveBrowser.WaitForAsyncOperations();

            //add the provider to the site
            string customProviderId = "_dataSourceProviderSelectorDialog_providerChkBox_" + providerName;
            BAT.Wrappers().Backend().Multisite().MultisiteWrapper().UpdateCheckbox(customProviderId, true);
            BAT.Wrappers().Backend().Multisite().MultisiteWrapper().PressDone();
            BAT.Wrappers().Backend().Multisite().MultisiteWrapper().PressSaveChanges();
        }

        /// <summary>
        /// Clicks the on actions menu item.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <param name="siteName">Name of the site.</param>
        private void ClickOnActionsMenuItem(string elementId, string itemName, string siteName = null)
        {
            var actionMenuList = this.OpenActionsMenu(siteName);

            HtmlAnchor menuItem = actionMenuList.Find.ByContent<HtmlAnchor>(itemName).AssertIsPresent("the link " + itemName + " in the actions menu");
            menuItem.Click();
            Manager.Current.Wait.For(() => this.WaitDialogToBeLoaded(elementId), 100000);
            ActiveBrowser.RefreshDomTree();         
        }

        /// <summary>
        /// Waits the dialog to be loaded.
        /// </summary>
        /// <param name="elementId">The element id.</param>
        /// <returns></returns>
        private bool WaitDialogToBeLoaded(string elementId)
        {
            ActiveBrowser.RefreshDomTree();
            var linkVar = ActiveBrowser.Find.ByExpression<HtmlAnchor>("id=?" + elementId);
            if (linkVar != null && linkVar.IsVisible())
            {
                return true;
            }
            return false;
        }
       
        /// <summary>
        /// Opens the actions menu.
        /// </summary>
        /// <param name="siteName">Name of the site.</param>
        /// <returns>actions menu</returns>
        private HtmlUnorderedList OpenActionsMenu(string siteName = null)
        {
            ActiveBrowser.RefreshDomTree();
            HtmlContainerControl workArea = null;
            HtmlAnchor actionsMenuLink = null;

            if (string.IsNullOrEmpty(siteName))
            {          
                actionsMenuLink = ActiveBrowser.Find
                                               .ByExpression<HtmlAnchor>("class=sfActivate k-link")
                                               .AssertIsPresent("actions menu in the grid row");
            }
            else
            {
                workArea = BAT.Wrappers().Backend().Multisite().MultisiteWrapper().GetSiteRowByName(siteName).AssertIsPresent("Site row");
                actionsMenuLink = workArea.Find
                                          .ByExpression<HtmlAnchor>("innertext=Actions")
                                          .AssertIsPresent("actions menu in the grid row");
            }
            actionsMenuLink.Click();
            ActiveBrowser.WaitForAsyncOperations();
            ActiveBrowser.RefreshDomTree();

            //find action menu here
            HtmlUnorderedList actionsMenuList = ActiveBrowser.Find
                                                             .ByExpression<HtmlUnorderedList>("class=~k-reset k-state-border-up")
                                                             .AssertIsPresent("actions menu in the grid row");
            return actionsMenuList;
        }
    }
}