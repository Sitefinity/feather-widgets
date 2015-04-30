using System;
using System.Linq;
using System.Web.Mvc;
using FeatherWidgets.TestUnit.DummyClasses.InlineClientAssets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models;
using Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models.JavaScript;

namespace FeatherWidgets.TestUnit.Identity
{
    /// <summary>
    /// Unit tests for the JavaScriptController.
    /// </summary>
    [TestClass]
    public class JavaScriptControllerTests
    {
        [TestMethod]
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

                var expectedScript = @"<script type=""text/javascript"">var inline = 5;</script>";

                Assert.AreEqual(expectedScript, viewModel.JavaScriptCode);
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
                controller.Model.Description = "test";

                var result = (ViewResult)controller.Index();

                var viewModel = (JavaScriptViewModel)result.Model;

                Assert.AreEqual("test", viewModel.Description);
            }
        }
    }
}
