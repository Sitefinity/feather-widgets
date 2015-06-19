using System;
using System.Linq;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
     /// <summary>
    /// This is the entry point class for styles and scripts widgets edit wrapper.
    /// </summary>
    public abstract class ScriptsAndStylesCommonWrapper : BaseWrapper
    {
        /// <summary>
        /// Fills the code in editable area.
        /// </summary>
        /// <param name="code">The code.</param>
        public void FillCodeInEditableArea(string code)
        {
            HtmlDiv editable = EM.ScriptsAndStyles.ScriptsAndStylesEditScreen
                                       .CodeMirrorLines
                                       .AssertIsPresent("Editable area");

            editable.ScrollToVisible();
            editable.Focus();
            editable.MouseClick();

            Manager.Current.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
            Manager.Current.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Delete);

            Manager.Current.Desktop.KeyBoard.TypeText(code);
        }

        /// <summary>
        /// Verifies the code in editable area.
        /// </summary>
        /// <param name="code">The code.</param>
        public void VerifyCodeInEditableArea(string code)
        {
            HtmlDiv editable = EM.ScriptsAndStyles.ScriptsAndStylesEditScreen
                                       .CodeMirrorLines
                                       .AssertIsPresent("Editable area");

            Assert.IsTrue(editable.InnerText.Contains(code));
        }

        /// <summary>
        /// Switches to link file option.
        /// </summary>
        public void SwitchToLinkFileOption()
        {
            HtmlInputRadioButton linkToCssFile = EM.ScriptsAndStyles.ScriptsAndStylesEditScreen.LinkToFile
            .AssertIsPresent("Link to css file button");
            linkToCssFile.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncJQueryRequests();
        }

        /// <summary>
        /// Click select button
        /// </summary>
        public void ClickSelectButton()
        {
            HtmlButton selectButton = EM.ScriptsAndStyles.ScriptsAndStylesEditScreen.SelectButton
                .AssertIsPresent("Select button");
            selectButton.Click();

            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncOperations();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Expand folder
        /// </summary>
        public void ExpandFolder(string folderName)
        {
            HtmlUnorderedList folderTree = EM.ScriptsAndStyles.ScriptsAndStylesEditScreen.FolderTree
                .AssertIsPresent("Folder tree");

            HtmlListItem listItem = folderTree.Find.ByExpression<HtmlListItem>("InnerText=" + folderName);
            var anchorExpand = listItem.Find.ByExpression<HtmlAnchor>("tagname=a");

            anchorExpand.Click();

            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Selects the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void SelectFile(string fileName)
        {
            ActiveBrowser.RefreshDomTree();
            HtmlUnorderedList fileTree = EM.ScriptsAndStyles.ScriptsAndStylesEditScreen.FileTree
                .AssertIsPresent("File tree");

            HtmlControl cssFile = fileTree.Find.ByExpression<HtmlControl>("InnerText=" + fileName);
            cssFile.Focus();
            cssFile.MouseClick();

            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Selects more options in the widget designer
        /// </summary>
        public void MoreOptions()
        {
            HtmlAnchor moreOptions = EM.ScriptsAndStyles.ScriptsAndStylesEditScreen.MoreOptions
                .AssertIsPresent("More options");

            moreOptions.ScrollToVisible();
            moreOptions.Focus();
            moreOptions.MouseClick();
        }

        /// <summary>
        /// Fill description
        /// </summary>
        /// <param name="description">The description</param>
        public void FillDescription(string description)
        {
            HtmlInputText input = EM.ScriptsAndStyles.ScriptsAndStylesEditScreen.Description
                .AssertIsPresent("Description");

            input.ScrollToVisible();
            input.Focus();
            input.MouseClick();

            Manager.Current.Desktop.KeyBoard.TypeText(description);
        }
    }
}
