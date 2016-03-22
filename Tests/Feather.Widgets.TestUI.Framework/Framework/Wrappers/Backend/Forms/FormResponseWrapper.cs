using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.jQuery;
using Telerik.Sitefinity.TestUI.Framework.ElementMap.Forms;
using Telerik.Sitefinity.TestUI.Framework.Wrappers.Backend.Forms;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Forms
{
    /// <summary>
    /// Main dialog wrapper operating on PropertyEditorDialog frame based element map
    /// </summary>
    public class FormResponseWrapper : FormsResponseCreateEditScreenWrapper<FormsResponsesCreateScreenFrame>
    {
        /// <summary>
        /// Verifies the navigation pages labels.
        /// </summary>
        /// <param name="labels">The labels.</param>
        /// <param name="navIndex">Index of the nav.</param>
        public void VerifyNavigationPagesLabelsInCreateResponseScreen(List<string> labels, int navIndex = 0)
        {
            var lists = this.ActiveWindowEM.Find.AllByExpression<HtmlUnorderedList>("class=sf-FormNav");
            lists[navIndex].AssertIsVisible("Navigation list");

            var pageLabels = lists[navIndex].Find.AllByTagName("li");
            for (int i = 0; i < labels.Count; i++)
            {
                Assert.AreEqual((i + 1) + labels[i], pageLabels[i].InnerText);
            }
        }

        /// <summary>
        /// Verify next step text
        /// </summary>
        /// <param name="buttonText">The button text.</param>
        /// <param name="isVisible">if set to <c>true</c> [is visible].</param>
        public void VerifyNextStepTextInCreateResponseScreen(string buttonText = "Next step", bool isVisible = true)
        {
            HtmlButton nextButton = this.ActiveWindowEM.Find.ByExpression<HtmlButton>("data-sf-btn-role=next");

            if (!isVisible)
            {
                nextButton.AssertIsNotVisible("Next step button");
            }
            else
            {
                nextButton.AssertIsVisible("Next step button");
                Assert.IsTrue(nextButton.InnerText.Contains(buttonText), "Button text ");
            }
        }

        /// <summary>
        /// Sets the TextBox Content in the Frontend of the form
        /// </summary>
        public void SetTextboxContentInCreateResponseScreen(string content)
        {
            HtmlInputText textbox = this.ActiveWindowEM.Find.ByExpression<HtmlInputText>("id=?_textBox_write").AssertIsPresent("Text field");

            textbox.ScrollToVisible();
            textbox.Focus();
            textbox.MouseClick();

            Manager.Current.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
            Manager.Current.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Delete);

            Manager.Current.Desktop.KeyBoard.TypeText(content);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Tab);
        }

        /// <summary>
        /// Clicks the submit button in the frontend of the form
        /// </summary>
        public void ClickSubmitInCreateResponseScreen()
        {
            HtmlAnchor submitButton = this.ActiveWindowEM.Find.ByExpression<HtmlAnchor>("id=?_submitButton");
            submitButton.MouseClick();
        }

        /// <summary>
        /// Gets the element map.
        /// </summary>
        /// <value>The element map.</value>
        protected override FormsResponsesCreateScreenFrame ActiveWindowEM
        {
            get
            {
                return new FormsResponsesCreateScreenFrame(ActiveBrowser.Find);
            }
        }
    }
}
