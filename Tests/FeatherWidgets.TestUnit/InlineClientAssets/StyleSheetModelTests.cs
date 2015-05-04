using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models;
using Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models.StyleSheet;

namespace FeatherWidgets.TestUnit.InlineClientAssets
{
    /// <summary>
    /// Tests the StyleSheet model class.
    /// </summary>
    [TestClass]
    public class StyleSheetModelTests
    {
        [TestMethod]
        [Owner("Boyko-Karadzhov")]
        [Description("Ensures that GetMarkup returns proper inline markup when in inline mode.")]
        public void GetMarkup_InlineMode_ReturnsStyleTag()
        {
            var testStyles = "Test styles.";
            var model = new StyleSheetModel();
            model.InlineStyles = testStyles;
            model.Mode = ResourceMode.Inline;
            model.MediaType = "screen";

            var result = model.GetMarkup();

            Assert.AreEqual(@"<style type=""text/css"" media=""screen"">Test styles.</style>", result, "GetMarkup did not return the expected inline markup.");
        }

        [TestMethod]
        [Owner("Boyko-Karadzhov")]
        [Description("Ensures that GetMarkup returns proper reference when in reference mode.")]
        public void GetMarkup_ReferenceMode_ReturnsLinkTag()
        {
            var testStyles = "http://my-styles.com/styles.css";
            var model = new StyleSheetModel();
            model.ResourceUrl = testStyles;
            model.Mode = ResourceMode.Reference;
            model.MediaType = "screen";

            var result = model.GetMarkup();

            Assert.AreEqual(@"<link href=""http://my-styles.com/styles.css"" media=""screen"" rel=""stylesheet"" type=""text/css"" />", result, "GetMarkup did not return the expected link markup.");
        }
    }
}
