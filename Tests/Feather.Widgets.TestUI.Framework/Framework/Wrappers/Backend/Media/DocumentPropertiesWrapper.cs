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
        /// Verifies the document link.
        /// </summary>
        /// <param name="title">The document.</param>
        /// <param name="href">The href.</param>
        public void VerifyDocumentLink(string title, string href)
        {
            HtmlAnchor doc = ActiveBrowser.Find.ByExpression<HtmlAnchor>("title=" + title)
                .AssertIsPresent("document");

            Assert.IsTrue(doc.HRef.StartsWith(href), "href is not correct");
        }
    }
}