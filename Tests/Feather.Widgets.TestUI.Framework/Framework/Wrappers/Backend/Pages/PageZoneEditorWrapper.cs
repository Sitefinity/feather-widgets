using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
