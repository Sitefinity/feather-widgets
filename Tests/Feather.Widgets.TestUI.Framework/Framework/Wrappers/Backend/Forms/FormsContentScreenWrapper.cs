using System;
using System.Collections.Generic;
using System.Linq;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.jQuery;

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
            var inputs = ActiveBrowser.Find.AllByExpression<HtmlInputText>("ng-model=item.Title");
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
    }
}
