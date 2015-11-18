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
        public void AddField(string widgetName, string placeHolder = "Body")
        {
            var widget = GetWidgetByNameFromSideBar(widgetName);
            HtmlDiv dropZone = ActiveBrowser.Find
                                               .ByExpression<HtmlDiv>("placeholderid=" + placeHolder)
               .AssertIsPresent<HtmlDiv>(placeHolder);
            dropZone.ScrollToVisible();
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

        /// <summary>
        /// Verify form widget is deleted
        /// </summary>
        public void VerifyFormCheckboxWidgetIsDeleted()
        {
            var CheckboxesFieldControllerDiv = EM.Forms.FormsBackend.CheckboxesFieldControllerDiv;
            Assert.IsNull(CheckboxesFieldControllerDiv, String.Format("checkbox field is not deleted", CheckboxesFieldControllerDiv));
        }
        /// <summary>
        /// Verify form widget is visible
        /// </summary>
        public void VerifyFormCheckboxWidgetIsVisible()
        {
            var CheckboxesFieldControllerDiv = EM.Forms.FormsBackend.CheckboxesFieldControllerDiv;
            Assert.IsNotNull(CheckboxesFieldControllerDiv, String.Format("checkbox field is not added", CheckboxesFieldControllerDiv));
        }
        /// <summary>
        /// Verify Dropdown filed contrl is visible
        /// </summary>
        public void VerifyFormDropdownWidgetIsVisible()
        {
            var dropdownListFieldControllerDiv = EM.Forms.FormsBackend.DropdownListFieldControllerDiv;
            Assert.IsNotNull(dropdownListFieldControllerDiv, String.Format("dropdown list field is not added", dropdownListFieldControllerDiv));
        }
        /// <summary>
        /// Verify Dropdown filed control is NOT visible
        /// </summary>
        public void VerifyFormDropdownWidgetIsDeleted()
        {
            var dropdownListFieldControllerDiv = EM.Forms.FormsBackend.DropdownListFieldControllerDiv;
            Assert.IsNull(dropdownListFieldControllerDiv, String.Format("dropdown field is not added", dropdownListFieldControllerDiv));
        }
        /// <summary>
        /// Clicks on Widget menu item (only click is performed, no waiting)
        /// </summary>
        /// <param name="widgetName">The Widget Name</param>
        /// <param name="menuOption">The menu option</param>
        public void ClickOnWidgetMenuItem(string controlerName, string menuOption)
        {
            HtmlDiv controleZone = EM.Forms.FormsBackend.BodyDropZone;
            HtmlDiv widget = ActiveBrowser.Find.ByExpression<HtmlDiv>("class=RadDock RadDock_Default zeControlDock", "behaviourobjecttype=~" + controlerName);
            HtmlAnchor moreLink = GetWidgetMoreLink(widget);
            moreLink.MouseClick();
            HtmlAnchor option = GetMoreMenuOption(menuOption);
            option.MouseClick();
        }
        /// <summary>
        /// Gets the More Link from a widget in the form dropzone
        /// </summary>
        /// <param name="widget">The widget to search in.</param>
        /// <returns>The more link</returns>
        private HtmlAnchor GetWidgetMoreLink(HtmlDiv widget)
        {
            HtmlAnchor morelink = widget.Find.ByExpression<HtmlAnchor>("tagname=a", "innertext=More")
                .AssertIsPresent("More Link");

            return morelink;
        }
        /// <summary>
        /// Gets the Edit option from a widget in the form dropzone
        /// </summary>
        /// <param name="widget">The widget to search in.</param>
        /// <returns>The edit link</returns>
        private HtmlSpan GetEditOption(HtmlDiv widget)
        {
            HtmlSpan editOption = widget.Find.ByExpression<HtmlSpan>("class=rdEditCommand", "innertext=Edit")
                .AssertIsPresent("Edit Link");

            return editOption;
        }

        /// <summary>
        /// Gets an option link from the more menu.
        /// </summary>
        /// <param name="widget">The link to search in for</param>
        /// <param name="menuOption">The menu option</param>
        /// <returns></returns>
        private HtmlAnchor GetMoreMenuOption(string menuOption)
        {
            ActiveBrowser.RefreshDomTree();

            HtmlAnchor option = ActiveBrowser.Find.ByExpression<HtmlAnchor>("tagname=a", "innertext=" + menuOption);
            option.AssertIsPresent("Menu option: " + menuOption);

            return option;
        }

        /// <summary>
        /// Verify common header and footer are visible
        /// </summary>
        public void VerifyCommonHeaderAndFooterAreVisible()
        {
            HtmlDiv commonHeader = EM.Forms.FormsBackend.CommonHeaderDiv
                 .AssertIsPresent<HtmlDiv>("Common header ");
            Assert.IsTrue(commonHeader.InnerText.Contains("Common header"), "Common header text ");
            Assert.IsNotNull(commonHeader, String.Format("Common header ", commonHeader));
            commonHeader.Find.AllByExpression<HtmlDiv>("class=zeDockZoneLabel")
                .First().AssertIsVisible("header placeholder");

            HtmlDiv commonFooter = EM.Forms.FormsBackend.CommonFooterDiv
                    .AssertIsPresent<HtmlDiv>("Common footer ");
            Assert.IsTrue(commonFooter.InnerText.Contains("Common footer"), "Common footer text ");
            Assert.IsNotNull(commonFooter, String.Format("Common footer ", commonFooter));
            commonFooter.Find.AllByExpression<HtmlDiv>("class=zeDockZoneLabel")
                .First().AssertIsVisible("footer placeholder");
        }
    }
}
