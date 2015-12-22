using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.jQuery;
using Telerik.WebAii.Controls.Html;

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
        /// Verify Captcha field is visible
        /// </summary>
        public void VerifyFormCaptchaFieldIsVisible()
        {
            var captchaControllerDiv = EM.Forms.FormsBackend.CaptchaControllerDiv;
            Assert.IsNotNull(captchaControllerDiv, String.Format("captcha field is not added", captchaControllerDiv));
        }

        /// <summary>
        /// Verify ParagraphTextField is visible
        /// </summary>
        public void VerifyFormParagraphTextFieldIsVisible()
        {
            var paragraphTextFieldControllerDiv = EM.Forms.FormsBackend.ParagraphTextFieldControllerDiv;
            Assert.IsNotNull(paragraphTextFieldControllerDiv, String.Format("paragraphTextField is not added", paragraphTextFieldControllerDiv));
        }

        /// <summary>
        /// Verify MultipleChoiceField is visible
        /// </summary>
        public void VerifyFormMultipleChoiceFieldIsVisible()
        {
            var multipleChoiceFieldControllerDiv = EM.Forms.FormsBackend.MultipleChoiceFieldControllerDiv;
            Assert.IsNotNull(multipleChoiceFieldControllerDiv, String.Format("multipleChoiceField is not added", multipleChoiceFieldControllerDiv));
        }

        /// <summary>
        /// Verify Captcha field is NOT visible
        /// </summary>
        public void VerifyFormCaptchaFieldIsNotVisible()
        {
            var captchaControllerDiv = EM.Forms.FormsBackend.CaptchaControllerDiv;
            Assert.IsNull(captchaControllerDiv, String.Format("captcha field is still visible", captchaControllerDiv));
        }

        /// <summary>
        /// Verify ParagraphTextField is NOT visible
        /// </summary>
        public void VerifyFormParagraphTextFieldIsNotVisible()
        {
            var paragraphTextFieldControllerDiv = EM.Forms.FormsBackend.ParagraphTextFieldControllerDiv;
            Assert.IsNull(paragraphTextFieldControllerDiv, String.Format("paragraphTextField is still visible", paragraphTextFieldControllerDiv));
        }

        /// <summary>
        /// Verify MultipleChoiceField is NOT visible
        /// </summary>
        public void VerifyFormMultipleChoiceFieldIsNotVisible()
        {
            var multipleChoiceFieldControllerDiv = EM.Forms.FormsBackend.MultipleChoiceFieldControllerDiv;
            Assert.IsNull(multipleChoiceFieldControllerDiv, String.Format("multipleChoiceField is still visible", multipleChoiceFieldControllerDiv));
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
            moreLink.Click();
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
            ActiveBrowser.WaitUntilReady();

            HtmlAnchor option = ActiveBrowser.Find.ByExpression<HtmlAnchor>("tagname=a", "innertext=" + menuOption);
            option.AssertIsPresent("Menu option: " + menuOption);

            return option;
        }

        /// <summary>
        /// Clicks on Save draft button
        /// <summary>
        public void ClickSaveDraft()
        {
            HtmlAnchor saveDraftButton = EM.Forms.FormsBackend.SaveDraftButton;
            Assert.IsNotNull(saveDraftButton, "Save draft button was not found");
            saveDraftButton.MouseClick();
            ActiveBrowser.WaitForAsyncRequests();
        }

        /// <summary>
        /// Verify PositiveMessageDraftIs shown
        /// <summary>
        public void VerifyPositiveMessageDraftIsShown()
        {
            HtmlSpan positiveMessageDraftIsSaved = EM.Forms.FormsBackend.PositiveMessageDraftIsSaved;
            Assert.IsNotNull(positiveMessageDraftIsSaved, "positiveMessageDraftIsSaved was not found");
        }

        /// <summary>
        /// Verify Verify forms status
        /// <summary>
        public void VerifyFormStatus(string formName, string status)
        {
            var formStatus = BAT.Wrappers().Backend().Forms().FormsDashboard().GetFormStatus(FeatherGlobals.FormName);
            Assert.AreEqual(status, formStatus, String.Format("Form with tatus {0} is not found", status));
        }

        /// <summary>
        /// Clicks on Preview button
        /// <summary>
        public void ClickPreviewButtonAndWaitForNewBrowser()
        {
            Manager.Current.SetNewBrowserTracking(true);
            this.WaitForPreviewButtonAndClickIt();
            Manager.Current.WaitForNewBrowserConnect("Preview", true, Manager.Current.Settings.ClientReadyTimeout);
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.RefreshDomTree();
            Manager.Current.SetNewBrowserTracking(false);
        }

        /// <summary>
        /// Wait for Preview button and click it
        /// <summary>
        private void WaitForPreviewButtonAndClickIt()
        {
            HtmlAnchor previewLink = EM.Forms.FormsBackend.PreviewButton;
            previewLink.Wait.ForExists();
            Assert.IsNotNull(previewLink, "The Preview button was not found.");
            Assert.IsTrue(previewLink.IsVisible(), "The Preview button was not visible.");
            previewLink.Click();
        }

        /// <summary>
        /// Close Browser
        /// <summary>
        public void CloseBrowser()
        {
            ActiveBrowser.Close();
        }

        /// <summary>
        /// Close Browser
        /// <summary>
        public void CloseBrowserAndConfirmDialog()
        {          
            var confirmDialog = BAT.Macros().DialogFacade().ConfirmDialog();
            ActiveBrowser.Close();
        }
    }
}
