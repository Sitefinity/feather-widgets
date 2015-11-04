using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Forms
{
    /// <summary>
    /// This is an entry point for FormsWrapper.
    /// </summary>
    public class FormsContentScreenWrapper : BaseWrapper
    {
        /// <summary>
        /// Drags a field from the Content form screen
        /// to the form dropzone
        /// </summary>
        /// <param name="widgetName">The Field Name</param>
        public void AddField(string widgetName)
        {
            var widget = GetWidgetByNameFromSideBar(widgetName);
            HtmlDiv dropZone = EM.Forms.FormsBackend.BodyDropZone;
            AddWidgetToDropZone(widget, dropZone);

            ActiveBrowser.WaitForAsyncRequests();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Gets a widget by Name
        /// </summary>
        /// <param name="widgetLabelName">The widget label name</param>
        /// <returns>The Widget</returns>
        private HtmlDiv GetWidgetByNameFromSideBar(string widgetLabelName)
        {
            HtmlDiv widget = ActiveBrowser.Find.ByExpression<HtmlDiv>("class=~RadDock", "class=~RadDock_Default", "class=~rdCollapsed", "class=~zeToolboxItem", "innertext=~" + widgetLabelName)
                .AssertIsPresent("Widget: " + widgetLabelName);

            return widget;
        }

        /// <summary>
        /// Drags the widget to the specified dropzone
        /// </summary>
        /// <param name="widgetElement">The Widget</param>
        /// <param name="dropZone">The Dropzone</param>
        private void AddWidgetToDropZone(HtmlDiv widgetElement, HtmlDiv dropZone)
        {
            ActiveBrowser.RefreshDomTree();
            widgetElement.Refresh();
            widgetElement.DragTo(dropZone);
        }

        /// <summary>
        /// Check required field checkbox.
        /// </summary>
        public void CheckRequiredFieldCheckbox()
        {
            HtmlInputCheckBox checkbox = this.EM.Forms.FormsBackend.RequiredFieldCheckBox.AssertIsPresent("Required field");
            checkbox.Click();
            checkbox.AssertIsPresent("checked");
        }
    }
}
