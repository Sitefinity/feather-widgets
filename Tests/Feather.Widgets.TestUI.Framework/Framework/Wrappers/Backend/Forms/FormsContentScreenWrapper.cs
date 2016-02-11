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
        /// Gets a widget by Name
        /// </summary>
        /// <param name="widgetLabelName">The widget label name</param>
        /// <returns>The Widget</returns>
        private HtmlDiv GetWidgetByNameFromZoneEditor(string widgetLabelName)
        {
            HtmlDiv widget = ActiveBrowser.Find.ByExpression<HtmlDiv>("class=rdTitleBar zeControlTitlebar", "innertext=~" + widgetLabelName)
                .AssertIsPresent("Widget: " + widgetLabelName);

            return widget;
        }

        /// <summary>
        /// Drags a field in zone editor
        /// to the form dropzone
        /// </summary>
        /// <param name="widgetName">The Field Name</param>
        public void MoveFieldInZoneEditor(string widgetName, string placeHolder = "Body")
        {
            var widget = GetWidgetByNameFromZoneEditor(widgetName);
            HtmlDiv dropZone = ActiveBrowser.Find
                                               .ByExpression<HtmlDiv>("placeholderid=" + placeHolder)
               .AssertIsPresent<HtmlDiv>(placeHolder);
            dropZone.ScrollToVisible();

            AddWidgetToDropZonePoint(widget, dropZone);

            ActiveBrowser.WaitForAsyncRequests();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Drags a field in zone editor
        /// to the form dropzone
        /// </summary>
        /// <param name="widgetName">The Field Name</param>
        public void VerifyFieldsInPlaceholder(string widgetName, string placeHolder = "Body", bool isContained = true)
        {
            HtmlDiv dropZone = ActiveBrowser.Find
                                               .ByExpression<HtmlDiv>("placeholderid=" + placeHolder)
               .AssertIsPresent<HtmlDiv>(placeHolder);

            if (isContained)
            {
                Assert.IsTrue(dropZone.InnerText.Contains(widgetName), "Widget is not in placeholder");
            }
            else 
            {
                Assert.IsFalse(dropZone.InnerText.Contains(widgetName), "Widget is in placeholder");
            }
        }

        /// <summary>
        /// Drags the widget to the specified point in dropzone
        /// </summary>
        /// <param name="widgetElement">The Widget</param>
        /// <param name="dropZone">The Dropzone</param>
        private void AddWidgetToDropZonePoint(HtmlDiv widgetElement, HtmlDiv dropZone)
        {
            ActiveBrowser.RefreshDomTree();
            widgetElement.Refresh();            
            widgetElement.DragTo(dropZone);
        }

        /// <summary>
        /// Clicks on Widget menu item (only click is performed, no waiting)
        /// </summary>
        /// <param name="widgetName">The Widget Name</param>
        /// <param name="menuOption">The menu option</param>
        public void ClickOnFieldMenuItem(string widgetName, string menuOption)
        {
            HtmlDiv widget = GetWidgetByNameFromZoneEditor(widgetName);
            widget.ScrollToVisible();
            widget.Focus();
            HtmlAnchor moreLink = GetWidgetMoreLink(widget);
            moreLink.MouseClick();
            HtmlAnchor option = GetMoreMenuOption(menuOption);
            option.MouseClick();
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
        /// Checks the required file upload field checkbox.
        /// </summary>
        public void CheckRequiredFileUploadFieldCheckbox()
        {
            HtmlInputCheckBox checkbox = this.EM.Forms.FormsBackend.RequiredFileUploadFieldCheckBox.AssertIsPresent("Required field");
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
        /// Verify common header and footer are visible
        /// </summary>
        /// <param name="areVisible">if set to <c>true</c> [are visible].</param>
        public void VerifyCommonHeaderAndFooterAreVisible(bool areVisible = true)
        {
            if (!areVisible)
            {
                EM.Forms.FormsBackend.CommonHeaderDiv.Find.AllByExpression<HtmlDiv>("class=zeDockZoneLabel")
                    .First().AssertIsNotVisible("header placeholder");

                EM.Forms.FormsBackend.CommonFooterDiv.Find.AllByExpression<HtmlDiv>("class=zeDockZoneLabel")
                    .First().AssertIsNotVisible("footer placeholder");
            }
            else
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

        /// <summary>
        /// Verifies the drop zones count.
        /// </summary>
        /// <param name="count">The expected count.</param>
        public void VerifyDropZonesCount(int expectedCount)
        {
            var dropZones = ActiveBrowser.Find.AllByExpression<HtmlDiv>("class=^RadDockZone RadDockZone_Default rdVertical zeDockZoneHasLabel");
            Assert.AreEqual(expectedCount, dropZones.Count);
        }

        /// <summary>
        /// Changes the next step text.
        /// </summary>
        /// <param name="newValue">The new value.</param>
        /// <param name="oldValue">The old value.</param>
        public void ChangeNextStepText(string newValue, string oldValue = "Next step")
        {
            HtmlInputText nextStep = EM.Forms.FormsBackend.NextStepInput;
            nextStep.AssertIsVisible("Next step input");
            Assert.AreEqual(oldValue, nextStep.Text);
            nextStep.Click();

            Manager.Current.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
            Manager.Current.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Delete);
            Manager.Current.Desktop.KeyBoard.TypeText(newValue);

            Assert.AreEqual(newValue, nextStep.Text);
        }

        /// <summary>
        /// Click option Allow users to step backward.
        /// </summary>
        public void ClickAllowUsersToStepBackwardCheckBox()
        {
            HtmlInputCheckBox checkbox = this.EM.Forms.FormsBackend.AllowUsersToStepBackwardCheckBox.AssertIsPresent("Next step button");          
            checkbox.AssertIsPresent("checked");
            checkbox.Click();
        }

        /// <summary>
        /// Changes the previous step text.
        /// </summary>
        /// <param name="newValue">The new value.</param>
        /// <param name="oldValue">The old value.</param>
        public void ChangePreviousStepText(string newValue, string oldValue = "Previous step")
        {
            HtmlInputText previousStep = EM.Forms.FormsBackend.PreviousStepInput;
            previousStep.AssertIsVisible("Previous step input");
            Assert.AreEqual(oldValue, previousStep.Text);
            previousStep.Click();

            Manager.Current.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
            Manager.Current.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Delete);
            Manager.Current.Desktop.KeyBoard.TypeText(newValue);

            Assert.AreEqual(newValue, previousStep.Text);
        }

        /// <summary>
        /// Changes the next step text.
        /// </summary>
        /// <param name="newValue">The new value.</param>
        /// <param name="oldValue">The old value.</param>
        public void ChangeNextStepTextInAdvancedSettings(string newValue, string oldValue = "Next step")
        {
            HtmlInputText nextStep = EM.Forms.FormsBackend.NextStepInputInAdvancedSettings;
            nextStep.AssertIsVisible("Next step input");
            Assert.AreEqual(oldValue, nextStep.Text);
            nextStep.Click();

            Manager.Current.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
            Manager.Current.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Delete);
            Manager.Current.Desktop.KeyBoard.TypeText(newValue);

            Assert.AreEqual(newValue, nextStep.Text);
        }

        /// <summary>
        /// Selects widget template from the drop-down in the widget designer
        /// </summary>
        /// <param name="templateTitle">widget template title</param>
        public void SelectNewTemplate(string templateTitle)
        {
            var templateSelector = EM.Forms.FormsBackend.TemplateSelector
              .AssertIsPresent("Template selector drop-down");
            templateSelector.SelectByValue(templateTitle);
            templateSelector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            templateSelector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
            ActiveBrowser.WaitForAsyncOperations();
        }

        /// Selects the cancel.
        /// </summary>
        public void SelectCancel()
        {
            HtmlAnchor cancelButton = EM.Forms.FormsBackend.CancelButton
            .AssertIsPresent("Cancel button");
            cancelButton.Click();
        }

        /// <summary>
        /// Changes the page label.
        /// </summary>
        /// <param name="texts">The texts.</param>
        /// <param name="index">The index.</param>
        public void ChangePageLabel(List<string> texts)
        {
            List<HtmlInputText> inputs = ActiveBrowser.Find.AllByExpression<HtmlInputText>("tagname=input", "ng-model=item.Title").ToList<HtmlInputText>();

            for (int i = 0; i < texts.Count; i++)
            {
                Assert.AreEqual(inputs.Count, texts.Count);
                inputs[i].Click();

                Manager.Current.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.Control);
                Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
                Manager.Current.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.Control);
                Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Delete);
                Manager.Current.Desktop.KeyBoard.TypeText(texts[i]);

                Assert.AreEqual(texts[i], inputs[i].Text);
            }
        }

        /// <summary>
        /// Change textbox label
        /// </summary>
        /// <param name="newValue">The new value.</param>
        public void ChangeTexboxLabel(string newValue)
        {
            HtmlInputText textbobLabel = EM.Forms.FormsBackend.TextBoxLabel.AssertIsPresent("Textbox label ");
            textbobLabel.Click();

            Manager.Current.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
            Manager.Current.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Delete);
            Manager.Current.Desktop.KeyBoard.TypeText(newValue);
        }

        /// <summary>
        /// Change label of multiple choices and checkbox fields
        /// </summary>
        /// <param name="text">Text</param>
        public void ChangeLabel(string text)
        {
            HtmlTextArea textArea = EM.Forms.FormsBackend.TextArea
                .AssertIsPresent("Text area");

            textArea.ScrollToVisible();
            textArea.Focus();
            textArea.MouseClick();

            Manager.Current.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
            Manager.Current.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Delete);
            Manager.Current.Desktop.KeyBoard.TypeText(text);
        }

        /// Duplicates the widget in the header.
        /// </summary>
        public void DuplicateWidgetInTheHeader()
        {
            var header = ActiveBrowser.Find.ById<HtmlDiv>("RadDockZoneHeader")
                .AssertIsPresent("header");
            var moreLink = header.Find.ByExpression<HtmlAnchor>("title=More");
            moreLink.MouseClick();

            var radMenu = Manager.Current.ActiveBrowser.Find.AllByCustom<RadMenu>(c => c.ID == "RadContextMenu1_detached")
                .FirstOrDefault();
            radMenu.Refresh();

            var duplicate = radMenu.Find.ByExpression<HtmlAnchor>("class=rmLink sfDuplicateItm");
            duplicate.AssertIsPresent("duplicate link");
            duplicate.MouseClick();
        }

        /// <summary>
        /// Selects naviagtion widget template from the drop-down in the widget designer
        /// </summary>
        /// <param name="templateTitle">widget template title</param>
        public void SelectNavigationTemplate(string templateTitle)
        {
            var templateSelector = EM.Forms.FormsBackend.NavigationTemplateSelector
              .AssertIsPresent("Template selector drop-down");
            templateSelector.SelectByValue(templateTitle);
            templateSelector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            templateSelector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
            ActiveBrowser.WaitForAsyncOperations();
        }

        /// <summary>
        /// Change css class in advanced settings
        /// </summary>
        /// <param name="text">Text</param>
        public void ChangeCssClassInAdvancedSettings(string text)
        {
            HtmlInputText cssClassInput = EM.Forms.FormsBackend.CssClassInAdvancedSettings
                .AssertIsPresent("Css class");

            cssClassInput.ScrollToVisible();
            cssClassInput.Focus();
            cssClassInput.MouseClick();

            Manager.Current.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
            Manager.Current.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Delete);
            Manager.Current.Desktop.KeyBoard.TypeText(text);
        }

        /// <summary>
        /// Apply css class
        /// </summary>
        /// <param name="cssClassName">css class name</param>
        public void ApplyCssClasses(string cssClassName)
        {
            HtmlAnchor moreOptions = this.EM.Widgets.WidgetDesignerContentScreen.MoreOptionsDiv.AssertIsPresent("More options span");
            moreOptions.Click();

            HtmlInputText cssClassesTextbox = EM.Forms.FormsBackend.CssClassesTextbox.AssertIsPresent("Css classes textbox");
            cssClassesTextbox.ScrollToVisible();
            cssClassesTextbox.Focus();
            cssClassesTextbox.MouseClick();

            Manager.Current.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
            Manager.Current.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Delete);
            Manager.Current.Desktop.KeyBoard.TypeText(cssClassName);
        }

        /// <summary>
        /// Apply css class
        /// </summary>
        public void ClickPreviewButton()
        {
            HtmlAnchor previewLink = ActiveBrowser.Find.ByExpression<HtmlAnchor>("tagname=a", "href=~Preview");
            Manager.Current.SetNewBrowserTracking(true);
            previewLink.Wait.ForExists();
            Assert.IsNotNull(previewLink, "The Preview button was not found.");
            Assert.IsTrue(previewLink.IsVisible(), "The Preview button was not visible.");
            previewLink.Click();
            Manager.Current.WaitForNewBrowserConnect("Preview", true, Manager.Current.Settings.ClientReadyTimeout);
            Manager.Current.SetNewBrowserTracking(false);
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
            Manager.Current.SetNewBrowserTracking(false);
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.RefreshDomTree();
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
        /// Click advanced button
        /// <summary>
        public void ClickAdvancedButton()
        {
            HtmlAnchor advancedLink = EM.Forms.FormsPropertiesScreenBaseFeather.AdvancedButton
                .AssertIsPresent("Advanced Link");
            advancedLink.Click();

            ActiveBrowser.WaitUntilReady();
        }

        /// <summary>
        /// Verify WebFramework exists
        /// <summary>
        public void VerifyWebFrameworkExists()
        {
            HtmlListItem webFramework = EM.Forms.FormsPropertiesScreenBaseFeather.WebFramework
                .AssertIsPresent("Web framework");

            Assert.IsTrue(webFramework.InnerText.Contains("Web framework"));
            Assert.IsTrue(webFramework.InnerText.Contains("MVC only"));
            Assert.IsTrue(webFramework.InnerText.Contains("Web Forms only"));
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
