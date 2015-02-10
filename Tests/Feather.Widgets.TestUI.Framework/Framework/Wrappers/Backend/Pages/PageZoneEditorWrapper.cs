using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using Telerik.WebAii.Controls.Html;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    /// <summary>
    /// Wraps actions and elements available in the Page Editor
    /// </summary>
    public class PageZoneEditorWrapper : BaseWrapper
    {
        /// <summary>
        /// Gets the widget by name
        /// </summary>
        /// <param name="widgetLabelName">The widget label name</param>
        /// <returns>Returns the widget div</returns>
        public HtmlDiv GetWidgetByName(string widgetLabelName)
        {
            ActiveBrowser.RefreshDomTree();
            RadPanelBar toolbox = Manager.Current.ActiveBrowser.Find.ById<RadPanelBar>("ctl00_ControlToolboxContainer");
            foreach (var item in toolbox.AllItems)
            {
                var dockZone = item.Find.ByCustom<RadDockZone>(zone => zone.CssClass.Contains("RadDockZone"));
                var widgetLabel = dockZone.Find.ByContent(widgetLabelName);
                if (widgetLabel != null)
                {
                    if (!item.Expanded)
                        item.Expand();
                    return new HtmlDiv(widgetLabel.Parent);
                }
            }

            Assert.IsNotNull(null, "The widget with name: " + widgetLabelName);
            return null;
        }

        /// <summary>
        /// Gets the Mvc navigation widget.
        /// </summary>
        /// <returns>The Mvc navigation widget div element.</returns>
        public HtmlDiv GetMvcNavigationWidget()
        {
            var siblingWidgetLabel = "ContentBlock";
            var navigation = "Navigation";

            ActiveBrowser.RefreshDomTree();
            RadPanelBar toolbox = Manager.Current.ActiveBrowser.Find.ById<RadPanelBar>("ControlToolboxContainer");
            foreach (var item in toolbox.AllItems)
            {
                var dockZone = item.Find.ByCustom<RadDockZone>(zone => zone.CssClass.Contains("RadDockZone"));
                var widgetLabel = dockZone.Find.ByContent(siblingWidgetLabel);
                if (widgetLabel != null)
                {
                    if (!item.Expanded)
                        item.Expand();

                    var navigationLabel = dockZone.Find.ByContent(navigation);
                    return new HtmlDiv(navigationLabel.Parent);
                }
            }

            return null;
        }

        /// <summary>
        /// Add the widget by name
        /// </summary>
        /// <param name="widgetName">The widget name</param>
        public void AddWidget(string widgetName)
        {
            HtmlDiv radDockZone = ActiveBrowser.Find.ByExpression<HtmlDiv>("id=?RadDockZoneContentplaceholder1")
                .AssertIsPresent<HtmlDiv>("RadDockZoneContentplaceholder1");

            HtmlDiv widget = this.GetWidgetByName(widgetName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToDropZone(widget, radDockZone);
        }

        /// <summary>
        /// Add widget by name to certain placeholder.
        /// </summary>
        /// <param name="widgetName">The widget name.</param>
        /// <param name="placeHolder">The placeholder name.</param>
        public void AddWidgetToSelectedPlaceHolder(string widgetName, string placeHolder)
        {
            HtmlDiv radDockZone = ActiveBrowser.Find.ByExpression<HtmlDiv>("id=?" + placeHolder)
               .AssertIsPresent<HtmlDiv>(placeHolder);

            HtmlDiv widget = this.GetWidgetByName(widgetName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToDropZone(widget, radDockZone);
        }

        /// <summary>
        /// Adds Mvc Navigation widget to selected placeholder.
        /// </summary>
        /// <param name="placeHolder">The placeholder id.</param>
        public void AddMvcNavigationWidgetToSelectedPlaceHolder(string placeHolder)
        {
            HtmlDiv radDockZone = ActiveBrowser.Find.ByExpression<HtmlDiv>("id=?" + placeHolder)
               .AssertIsPresent<HtmlDiv>(placeHolder);

            HtmlDiv widget = this.GetMvcNavigationWidget();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToDropZone(widget, radDockZone);
        }

        /// <summary>
        /// Edit widget by name
        /// </summary>
        /// <param name="widgetName">The widget name</param>
        /// <param name="dropZoneIndex">The drop zone index</param>
        public void EditWidget(string widgetName, int dropZoneIndex = 0)
        {
            ActiveBrowser.RefreshDomTree();
            var widgetHeader = ActiveBrowser
                                      .Find
                                      .AllByCustom<HtmlDiv>(d => d.CssClass.StartsWith("rdTitleBar") && d.ChildNodes.First().InnerText.Equals(widgetName))[dropZoneIndex]
                                      .AssertIsPresent(widgetName);
            widgetHeader.ScrollToVisible();
            HtmlAnchor editLink = widgetHeader.Find
                                              .ByCustom<HtmlAnchor>(a => a.TagName == "a" && a.Title.Equals("Edit"))
                                              .AssertIsPresent("edit link");
            editLink.Focus();
            editLink.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncOperations();
            ActiveBrowser.RefreshDomTree();

            HtmlFindExpression expression = new HtmlFindExpression("class=modal-title", "InnerText=" + widgetName);
            ActiveBrowser.WaitForElement(expression, TimeOut, false);
            Manager.Current.Wait.For(this.WaitForSaveButton, Manager.Current.Settings.ClientReadyTimeout);
        }

        /// <summary>
        /// Selects "an extra option" (option from the 'More' menu)
        /// for a given widget
        /// </summary>
        /// <param name="extraOption">The option to be clicked</param>
        /// <param name="dropZoneIndex">The dropZone(location) of the widget</param>
        public void SelectExtraOptionForWidget(string extraOption, int dropZoneIndex = 0)
        {
            ActiveBrowser.WaitForAsyncOperations();
            Manager.Current.ActiveBrowser.RefreshDomTree();
            var widgetHeader = Manager.Current
                                      .ActiveBrowser
                                      .Find
                                      .AllByCustom<HtmlDiv>(d => d.CssClass.StartsWith("rdTitleBar"))[dropZoneIndex]
                                      .AssertIsPresent("Widget at position: " + dropZoneIndex);
            widgetHeader.ScrollToVisible();
            HtmlAnchor moreLink = widgetHeader.Find
                                              .ByCustom<HtmlAnchor>(a => a.TagName == "a" && a.Title.Equals("More"))
                                              .AssertIsPresent("more link");
            moreLink.Focus();
            moreLink.Click();
            ActiveBrowser.WaitForAsyncOperations();
            Manager.Current.ActiveBrowser.RefreshDomTree();
            HtmlDiv menuDiv = ActiveBrowser.Find.ByExpression<HtmlDiv>("tagName=div", "id=RadContextMenu1_detached")
                .AssertIsPresent<HtmlDiv>("More options menu");

            menuDiv.Find.ByCustom<HtmlSpan>(x => x.InnerText.Contains(extraOption))
                .AssertIsPresent<HtmlSpan>("option " + extraOption)
                .Click();
        }

        /// <summary>
        /// Verify shared label of content block widget
        /// </summary>
        /// <returns>Returns if the shared label exist</returns>
        public bool VerifyContentBlockWidgetSharedLabel()
        {
            bool hasLabel = true;
            HtmlDiv titleBar = BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().GetWidgetTitleContainer("ContentBlock")
                .AssertIsPresent("Title bar");

            HtmlSpan sharedLabel = titleBar.Find.ByExpression<HtmlSpan>("class=sfShared");

            if (sharedLabel == null)
            {
                hasLabel = false;
            }

            return hasLabel;
        }
       
        /// <summary>
        /// Verifies the correct order of items on backend.
        /// </summary>
        /// <param name="itemNames">The item names.</param>
        public void VerifyCorrectOrderOfItemsOnBackend(params string[] itemNames)
        {
            var items = ActiveBrowser.Find.AllByExpression<HtmlContainerControl>("tagname=h3");

            int itemsCount = items.Count;
            Assert.IsNotNull(itemsCount);
            Assert.AreNotEqual(0, itemsCount);

            for (int i = 0; i < itemsCount; i++)
            {
                Assert.IsTrue(items[i].InnerText.Contains(itemNames[i]));
            }
        }

        /// <summary>
        /// Verifies the created link.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="href">The href.</param>
        public void VerifyCreatedLink(string name, string href)
        {
            ActiveBrowser.Find.ByExpression<HtmlAnchor>("href=" + href, "InnerText=" + name).AssertIsPresent(name + " or " + href + " was not present.");
        }

        private bool WaitForSaveButton()
        {
            Manager.Current.ActiveBrowser.RefreshDomTree();
            var saveButton = EM.Widgets
                                   .WidgetDesignerContentScreen.SaveChangesButton;

            bool result = saveButton != null && saveButton.IsVisible();

            return result;
        }

        private const int TimeOut = 60000;
    }
}
