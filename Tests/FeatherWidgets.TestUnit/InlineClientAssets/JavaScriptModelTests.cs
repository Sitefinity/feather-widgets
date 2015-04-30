using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models.JavaScript;

namespace FeatherWidgets.TestUnit.InlineClientAssets
{
    /// <summary>
    /// Unit tests for the JavaScript model.
    /// </summary>
    [TestClass]
    public class JavaScriptModelTests
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Web.UI.WebControls.Literal.set_Text(System.String)"), Owner("Mateev")]
        [Description("Verifies that the provided script is appended in the end of the given page when there is a LiteralControl marker.")]
        [TestMethod]
        public void PlaceScriptInTheEndOfTheBodyTag_LiteralMarker()
        {
            var page = new Page();

            page.Controls.Add(new Button());

            // The mvc master page has literal control that is used to mark the body end and there are inserted all scripts.
            page.Controls.Add(new LiteralControl() { Text = "marker" });

            var model = new JavaScriptModel();
            model.PlaceScriptBeforeBodyEnd(page, "script");

            var expectedScriptControl = page.Controls[1];

            Assert.IsInstanceOfType(expectedScriptControl, typeof(LiteralControl));

            var expectedScriptLiteral = (LiteralControl)expectedScriptControl;

            Assert.AreEqual("script", expectedScriptLiteral.Text);
        }
    }
}
