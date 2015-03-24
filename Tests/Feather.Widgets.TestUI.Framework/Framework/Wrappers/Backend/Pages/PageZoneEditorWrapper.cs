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
        /// Gets the feather Mvc widget.
        /// </summary>
        /// <param name="mvcWidgetName">feather mvc widget name</param>
        /// <returns>The Mvc widget div element.</returns>
        public HtmlDiv GetMvcWidget(string mvcWidgetName)
        {
            var siblingWidgetLabel = "ContentBlock";

            ActiveBrowser.RefreshDomTree();
            RadPanelBar toolbox = Manager.Current.ActiveBrowser.Find.ById<RadPanelBar>("ControlToolboxContainer");
            foreach (var item in toolbox.AllItems)
            {
                var dockZone = item.Find.ByCustom<RadDockZone>(zone => zone.CssClass.Contains("RadDockZone"));
                var contentBlockWidgetLabel = dockZone.Find.ByContent(siblingWidgetLabel);
                if (contentBlockWidgetLabel != null)
                {
                    if (!item.Expanded)
                        item.Expand();

                    var mvcWidgetLabel = dockZone.Find.ByContent(mvcWidgetName);
                    return new HtmlDiv(mvcWidgetLabel.Parent);
                }
            }

            return null;
        }

        /// <summary>
        /// Add widget by name to placeholder in pure mvc page.
        /// </summary>
        /// <param name="widgetName">The widget name.</param>
        /// <param name="placeHolder">The placeholder id.</param>
        public void AddWidgetToPlaceHolderPureMvcMode(string widgetName, string placeHolder = "Contentplaceholder1")
        {            
            HtmlDiv widget = this.GetWidgetByName(widgetName);

            HtmlDiv radDockZone = ActiveBrowser.Find.ByExpression<HtmlDiv>("placeholderid=" + placeHolder)
               .AssertIsPresent<HtmlDiv>(placeHolder);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToDropZone(widget, radDockZone);
        }

        /// <summary>
        /// Adds Mvc widget to drop zone in hybrid mode page.
        /// </summary>
        public void AddMvcWidgetHybridModePage(string widgetName)
        {
            HtmlDiv widget = this.GetMvcWidget(widgetName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToDropZone(widget);
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

        /// <summary>
        /// Verifies the created image link.
        /// </summary>
        /// <param name="src">The URL.</param>
        /// <param name="href">The href.</param>
        public void VerifyCreatedImageLink(string src, string href)
        {
            var anchor = ActiveBrowser.Find.ByExpression<HtmlAnchor>("href=" + href).AssertIsPresent(href + " was not present.");
            anchor.Find.ByExpression<HtmlImage>("src=" + src).AssertIsPresent(src + " was not present.");
        }

        /// <summary>
        /// Verifies the image thumbnail.
        /// </summary>
        /// <param name="altText">The alt text.</param>
        /// <param name="src">The SRC.</param>
        public void VerifyImageThumbnail(string altText, string src)
        {
            HtmlImage image = ActiveBrowser.Find.ByExpression<HtmlImage>("alt=~" + altText)
                .AssertIsPresent(altText);

            Assert.IsTrue(image.Src.StartsWith(src), "src is not correct");
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
