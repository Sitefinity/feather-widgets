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
    /// This is an netry point for DocumentPropertiesWrapper.
    /// </summary>
    public class DocumentPropertiesWrapper : MediaPropertiesBaseWrapper
    {
        /// <summary>
        /// Checks if image title is populated correctly.
        /// </summary>
        /// <param name="imageTitle">The image title.</param>
        /// <returns>true or false depending on the image title in the textbox.</returns>
        public bool IsDocumentTitlePopulated(string imageTitle)
        {
            HtmlInputText titleField = this.EM.Media.ImagePropertiesScreen.ImageTitleField
                                           .AssertIsPresent("Image title field");

            return imageTitle.Equals(titleField.Text);
        }

        /// <summary>
        /// Enters new title for image.
        /// </summary>
        /// <param name="documentTitle">The image title.</param>
        public void EnterDocumentTitle(string imageTitle)
        {
            HtmlInputText titleField = this.EM.Media.ImagePropertiesScreen.ImageTitleField
                                           .AssertIsPresent("Image title field");

            titleField.Text = string.Empty;
            titleField.Text = imageTitle;
            titleField.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }

        /// <summary>
        /// Verifies the document link.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="href">The href.</param>
        public void VerifyDocumentLink(string title, string href)
        {
            HtmlAnchor doc = ActiveBrowser.Find.ByExpression<HtmlAnchor>("title=" + title)
                .AssertIsPresent("document");

            Assert.IsTrue(doc.HRef.StartsWith(href), "href is not correct");
        }
    }
}