using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.jQuery;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Media
{
    /// <summary>
    /// This is an netry point for MediaPropertiesBaseWrapper.
    /// </summary>
    public class MediaPropertiesBaseWrapper : BaseWrapper
    {
        /// <summary>
        /// Changes the media file.
        /// </summary>
        public void ChangeMediaFile()
        {
            HtmlButton changeBtn = this.EM.Media.MediaPropertiesBaseScreen.ChangeButton.AssertIsPresent("Change button");

            changeBtn.Click();
            ActiveBrowser.WaitForAsyncRequests();
        }

        /// <summary>
        /// Edits all properties.
        /// </summary>
        public void EditAllProperties()
        {
            HtmlButton editBtn = this.EM.Media.MediaPropertiesBaseScreen.EditAllPropertiesButton.AssertIsPresent("Edit all properties button");
            editBtn.Click();
            ActiveBrowser.WaitForAsyncOperations();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Enters the new title.
        /// </summary>
        /// <param name="title">The image title.</param>
        public void EnterNewTitleInPropertiesDialogAndPublish(string title)
        {
            var frames = Manager.Current.ActiveBrowser.Frames;
            HtmlInputText titleField = frames[0].Find.ByExpression<HtmlInputText>("tagName=input", "id=?TitleFieldControl_0_ctl00_0_ctl00_0_textBox_write_0")
                                                .AssertIsPresent("Image title field");
            titleField.Text = string.Empty;
            titleField.Text = title;

            HtmlAnchor publishBtn = frames[0].Find.ByExpression<HtmlAnchor>("class=sfLinkBtn sfSave", "title=~Publish", "id=?_Publish").AssertIsPresent("Publish button");
            publishBtn.Click();
            ActiveBrowser.WaitForAsyncOperations();
            ActiveBrowser.RefreshDomTree();
        }
 
        /// <summary>
        /// Clicks Done button on media properties dialog.
        /// </summary>
        public void ConfirmMediaProperties()
        {
            HtmlButton doneBtn = this.EM.Media.MediaPropertiesBaseScreen.DoneButton.AssertIsPresent("Done button");

            doneBtn.Click();
        }

        /// <summary>
        /// Confirms the image properties in image widget.
        /// </summary>
        public void ConfirmImagePropertiesInImageWidget()
        {
            HtmlButton saveBtn = this.EM.Media.MediaPropertiesBaseScreen.SaveButtonInMediaWidget.AssertIsPresent("Done button");

            saveBtn.Click();
            ActiveBrowser.WaitForAsyncOperations();
            ActiveBrowser.RefreshDomTree();
        }
    }
}