using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.jQuery;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    /// <summary>
    /// This is the entry point class for SocialShare widget edit wrapper.
    /// </summary>
    public class SocialShareWidgetEditWrapper : BaseWrapper
    {

        /// <summary>
        /// When the first control is added to a form a submit button is automatically added to the page. 
        /// This method waits for the submit button to be added.
        /// </summary>
        public void WaitForSaveButtonToAppear()
        {
            Manager.Current.Wait.For(this.WaitForSaveButton, Manager.Current.Settings.ClientReadyTimeout);
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Selects widget template from the drop-down in the widget designer
        /// </summary>
        /// <param name="templateTitle">widget template title</param>
        public void SelectWidgetListTemplate(string templateTitle)
        {
            var templateSelector = EM.SocialShare
                                     .SocialShareWidgetEditScreen
                                     .TemplateSelector
                                     .AssertIsPresent("Template selector drop-down");
            templateSelector.SelectByValue(templateTitle);
            templateSelector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            templateSelector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
            ActiveBrowser.WaitForAsyncOperations();
        }

        /// <summary>
        /// Selects the item.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        public void SelectSocialShareOptions(params string[] itemNames)
        {            
            foreach (var itemName in itemNames)
            {
                var div = ActiveBrowser.Find.ByCustom<HtmlDiv>(a => a.InnerText.Equals(itemName));
                var input = div.Find.ByExpression<HtmlInputCheckBox>("ng-model=group.IsChecked");
                if (!input.Checked)
                {
                    input.Click();
                }
                ActiveBrowser.RefreshDomTree();
            }
        }

        /// <summary>
        /// Unselects the item.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        public void UnselectSocialShareOptions(params string[] itemNames)
        {
            foreach (var itemName in itemNames)
            {
                var div = ActiveBrowser.Find.ByCustom<HtmlDiv>(a => a.InnerText.Equals(itemName));
                var input = div.Find.ByExpression<HtmlInputCheckBox>("ng-model=group.IsChecked");
                if (input.Checked)
                {
                    input.Click();
                }
                ActiveBrowser.RefreshDomTree();
            }
        }

        /// <summary>
        /// Selects the item.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        public void SelectUnselectAllSocialShareOptions(bool isSelectMode = true)
        {
            var inputs = ActiveBrowser.Find.AllByExpression<HtmlInputCheckBox>("ng-model=group.IsChecked");
            foreach (var input in inputs)
            {
                if (isSelectMode)
                {
                    if (!input.Checked)
                    {
                        input.Click();
                    }
                }
                else
                {
                    if (input.Checked)
                    {
                        input.Click();
                    }
                }
                ActiveBrowser.RefreshDomTree();
            }
        }

        private bool WaitForSaveButton()
        {
            Manager.Current.ActiveBrowser.RefreshDomTree();
            var saveButton = EM.Widgets
                                   .WidgetDesignerContentScreen.SaveChangesButton;

            bool result = saveButton != null && saveButton.IsVisible();

            return result;
        }
    }
}