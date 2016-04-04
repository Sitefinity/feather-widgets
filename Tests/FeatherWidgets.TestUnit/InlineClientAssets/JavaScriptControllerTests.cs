using System;
using System.Linq;
using System.Web.Mvc;
using FeatherWidgets.TestUnit.DummyClasses.InlineClientAssets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models;
using Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models.JavaScript;

namespace FeatherWidgets.TestUnit.InlineClientAssets
{
    /// <summary>
    /// Unit tests for the JavaScriptController.
    /// </summary>
    [TestClass]
    public class JavaScriptControllerTests
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)"), TestMethod]
        [Owner("Mateev")]
        [Description("Calls the Index view and verifies that the generated script tag contains the provided inline javascript code.")]
        public void Index_ScriptGeneration_InlineCodeIsProvided()
        {
            using (var controller = new DummyJavaScriptController())
            {
                controller.Model.Mode = ResourceMode.Inline;

                controller.Model.InlineCode = "var inline = 5;";

                var result = (ViewResult)controller.Index();

                var viewModel = (JavaScriptViewModel)result.Model;

                var expectedScript = string.Format(@"<script type=""text/javascript"">{0}var inline = 5;{0}</script>", Environment.NewLine);

                Assert.AreEqual(expectedScript, viewModel.JavaScriptCode);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)"), TestMethod]
        [Owner("NPetrova")]
        [Description("Calls the Index view and verifies that the generated short inline script code in design mode is correct.")]
        public void Index_ScriptGeneration_ShortInlineCodeIsProvided()
        {
            using (var controller = new DummyJavaScriptController())
            {
                controller.Model.Mode = ResourceMode.Inline;

                controller.Model.InlineCode = "var inline = 5;";

                var result = (ViewResult)controller.Index();

                var viewModel = (JavaScriptViewModel)result.Model;

                var expectedScript = string.Format(@"<script type=""text/javascript"">{0}var inline = 5;{0}...{0}", Environment.NewLine);

                Assert.AreEqual(expectedScript, viewModel.DesignModeContent);
            }
        }

        [TestMethod]
        [Owner("Mateev")]
        [Description("Calls the Index view and verifies that the generated script tag contains reference to the provided file.")]
        public void Index_ScriptGeneration_FileUrlIsProvided()
        {
            using (var controller = new DummyJavaScriptController())
            {
                controller.Model.Mode = ResourceMode.Reference;

                controller.Model.FileUrl = "~/test.js";

                var result = (ViewResult)controller.Index();

                var viewModel = (JavaScriptViewModel)result.Model;

                var expectedScript = @"<script type=""text/javascript"" src=""/test.js""></script>";

                Assert.AreEqual(expectedScript, viewModel.JavaScriptCode);
            }
        }

        [TestMethod]
        [Owner("Mateev")]
        [Description("Calls the Index view and verifies that the description from the model is passed to the view.")]
        public void Index_DescriptionIsPassedToView()
        {
            using (var controller = new DummyJavaScriptController())
            {
                controller.Model.InlineCode = "not empty";
                controller.Model.Description = "test";

                var result = (ViewResult)controller.Index();

                var viewModel = (JavaScriptViewModel)result.Model;

                Assert.AreEqual("test", viewModel.DesignModeContent);
            }
        }
    }
}
