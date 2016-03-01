﻿using System;
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
        /// Verifies if an Mvc widget is present in pages toolbox section.
        /// </summary>
        /// <param name="mvcWidgetName">The title of the widget.</param>
        /// <returns>true or false depending on the widget presence in the toolbox.</returns>
        public bool IsMvcWidgetPresentInToolbox(string mvcWidgetName)
        {
            var mvcWidget = this.GetMvcWidget(mvcWidgetName);

            if (mvcWidget == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Verifies if any Feather MVC widget is presented in pages toolbox section.
        /// </summary>
        /// <returns>true or false depending on the widgets presence in the toolbox.</returns>
        public bool IsAnyMvcWidgetPersentInToolbox()
        {
            ActiveBrowser.RefreshDomTree();
            RadPanelBar toolbox = Manager.Current.ActiveBrowser.Find.ById<RadPanelBar>("~ControlToolboxContainer");
            var mvcItems = toolbox.Find.AllByExpression("class=~sfMvcIcn");

            return mvcItems != null && mvcItems.Count > 0;
        }

        /// <summary>
        /// Gets the feather Mvc widget.
        /// </summary>
        /// <param name="mvcWidgetName">feather mvc widget name</param>
        /// <returns>The Mvc widget div element.</returns>
        public HtmlDiv GetMvcWidget(string mvcWidgetName)
        {
            ActiveBrowser.RefreshDomTree();
            RadPanelBar toolbox = Manager.Current.ActiveBrowser.Find.ById<RadPanelBar>("~ControlToolboxContainer");
            foreach (var item in toolbox.AllItems)
            {
                var dockZone = item.Find.ByCustom<RadDockZone>(zone => zone.CssClass.Contains("RadDockZone"));
                var mvcWidgetLabel = dockZone.Find.ByExpression("class=~sfMvcIcn", "InnerText=" + mvcWidgetName);
                if (mvcWidgetLabel != null)
                {
                    if (!item.Expanded)
                        item.Expand();

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
            HtmlDiv widget = this.GetMvcWidget(widgetName);
            HtmlDiv radDockZone = ActiveBrowser.Find
                                               .ByExpression<HtmlDiv>("placeholderid=" + placeHolder)
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
        public void EditWidget(string widgetName, int dropZoneIndex = 0, bool isMediaWidgetEdited = false)
        {
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.RefreshDomTree();
            ActiveBrowser.WaitUntilReady();
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
            ActiveBrowser.WaitForAjax(TimeOut);
            ActiveBrowser.RefreshDomTree();

            if (!isMediaWidgetEdited)
            {
                HtmlFindExpression expression = new HtmlFindExpression("class=modal-title", "InnerText=" + widgetName);
                ActiveBrowser.WaitForElement(expression, TimeOut, false);
                Manager.Current.Wait.For(this.WaitForSaveButton, TimeOut);
            }
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
            HtmlDiv menuDiv = ActiveBrowser.Find
                                           .ByExpression<HtmlDiv>("tagName=div", "id=RadContextMenu1_detached")
                .AssertIsPresent<HtmlDiv>("More options menu");

            menuDiv.Find
                   .ByCustom<HtmlSpan>(x => x.InnerText.Contains(extraOption))
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
            HtmlDiv titleBar = BAT.Wrappers()
                                  .Backend()
                                  .Pages()
                                  .PageZoneEditorWrapper()
                                  .GetWidgetTitleContainer("ContentBlock")
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

            for (int i = 0; i < itemsCount-1; i++)
            {
                Assert.IsTrue(items[i].InnerText.Contains(itemNames[i]), items[i].InnerText + " not contain" + itemNames[i]);
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
        /// Verifies the java script widget text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="isPresent">The is present.</param>
        public void VerifyJavaScriptWidgetText(string text, bool isPresent = true)
        {
            if (isPresent)
        {
                ActiveBrowser.Find.AllByExpression<HtmlDiv>("class=rdContent", "innertext=~" + text).FirstOrDefault().AssertIsPresent("text");
            }
            else 
            {
                ActiveBrowser.Find.AllByExpression<HtmlDiv>("class=rdContent", "innertext=~" + text).FirstOrDefault().AssertIsNull("text");
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

        private const int TimeOut = 60000;
    }
}
