using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WebAii.Controls.Html;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    /// <summary>
    /// Wraps actions and elements available in the Page Editor
    /// </summary>
    public class PageZoneEditorWrapper : BaseWrapper
    {
        public HtmlDiv GetWidgetByName(string widgetLabelName)
        {
            RadPanelBar toolbox = Manager.Current.ActiveBrowser.Find.ById<RadPanelBar>("ctl00_ControlToolboxContainer");
            //dockZone.Refresh();
            foreach (var item in toolbox.AllItems)
            {
                var dockZone = item.Find.ByCustom<RadDockZone>(zone => zone.CssClass.Contains("RadDockZone"));
                var widgetLabel = dockZone.Find.ByContent(widgetLabelName);
                if (widgetLabel != null)
                {
                    //expand the panel before dragging a widget
                    if (!item.Expanded)
                        item.Expand();
                    return new HtmlDiv(widgetLabel.Parent);
                }
            }

            // Reaching this section of code means, that such widget is not found
            // This "fake" assert is added in order to make a more readable/informational exception
            // if the requested widget is NOT found in the RadPanelBar.
            // Letting the Assert to throw an exception is the preferred way to inform the user, that
            // something goes wrong, INSTEAD of explicitly throwing a custom exception!
            Assert.IsNotNull(null, "The widget with name: " + widgetLabelName);
            //throw new ApplicationException(string.Concat("Cannot find a widget with name : ", widgetLabelName));
            return null;
        }

        public void AddWidget(string widgetName)
        {
            HtmlDiv radDockZone = ActiveBrowser.Find.ByExpression<HtmlDiv>("id=?RadDockZoneContentplaceholder1")
                .AssertIsPresent<HtmlDiv>("RadDockZoneContentplaceholder1");

            HtmlDiv lastRadZone = radDockZone.ChildNodes.Where(p => p.InnerText.Contains("Contentplaceholder1")).FirstOrDefault().As<HtmlDiv>();
            lastRadZone.AssertIsPresent("Contentplaceholder1");

            HtmlDiv widget = this.GetWidgetByName(widgetName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToDropZone(widget, lastRadZone);
        }

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
    }
}
