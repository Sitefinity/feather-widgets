using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using FeatherWidgets.TestUnit.DummyClasses.InlineClientAssets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models.EmbedCode;

namespace FeatherWidgets.TestUnit.InlineClientAssets
{
    /// <summary>
    /// Unit tests for the EmbedCodeController
    /// </summary>
    [TestClass]
    public class EmbedCodeControllerTests
    {
        /// <summary>
        /// Index_s the script generation_ inline code is provided.
        /// </summary>
        [TestMethod]
        [Owner("Manev")]
        [Description("Text index action if the script generation inline code is provided.")]
        public void Index_ScriptGeneration_InlineCodeIsProvided()
        {
            using (var controller = new DummyEmbedCodeController())
            {
                string code = "<script>var inline = 5;</script>";

                controller.Model.InlineCode = code;

                var result = (ViewResult)controller.Index();

                var viewModel = (EmbedCodeViewModel)result.Model;

                Assert.AreEqual(code, viewModel.EmbedCode);
            }
        }

        /// <summary>
        /// Index_s the display description_ if in design mode.
        /// </summary>
        [TestMethod]
        [Owner("Manev")]
        [Description("Text index action if the display description in design mode.")]
        public void Index_DisplayDescription_IfInDesignMode()
        {
            using (var controller = new DummyEmbedCodeController(true))
            {
                string code = "<script>var inline = 5;</script>";

                string description = "provided description";

                controller.Model.InlineCode = code;

                controller.Model.Description = description;

                var result = (ViewResult)controller.Index();

                Assert.AreEqual(result.ViewBag.DesignModeContent, description);
            }
        }

        /// <summary>
        /// Index_s the test description is not displayed_ if not in design mode.
        /// </summary>
        [TestMethod]
        [Owner("Manev")]
        [Description("Test index action the test description is not displayed if not in design mode.")]
        public void Index_TestDescriptionIsNotDisplayed_IfNotInDesignMode()
        {
            using (var controller = new DummyEmbedCodeController())
            {
                string code = "<script>var inline = 5;</script>";

                string description = "provided description";

                controller.Model.InlineCode = code;

                controller.Model.Description = description;

                var result = (ViewResult)controller.Index();

                Assert.IsNull(result.ViewBag.DesignModeContent);

                var viewModel = (EmbedCodeViewModel)result.Model;

                Assert.AreEqual(code, viewModel.EmbedCode);
            }
        }

        /// <summary>
        /// Index_s the cropped text is displayed_ in_ design_ mode.
        /// </summary>
        [TestMethod]
        [Owner("Manev")]
        [Description("Test index and a cropped text is displayed when in design mode.")]
        public void Index_CroppedTextIsDisplayed_In_Design_Mode()
        {
            using (var controller = new DummyEmbedCodeController(string.Empty, true))
            {
                var sb = new StringBuilder();
                sb.Append("<script>")
                  .AppendLine()
                  .Append("var inline = 5;")
                  .AppendLine()
                  .Append("</script>");

                string code = sb.ToString();

                controller.Model.InlineCode = code;

                var result = (ViewResult)controller.Index();

                Assert.AreEqual(result.ViewBag.DesignModeContent, "<script>\r\nvar inline = 5;\r\n...\r\n");
            }
        }
    }
}
