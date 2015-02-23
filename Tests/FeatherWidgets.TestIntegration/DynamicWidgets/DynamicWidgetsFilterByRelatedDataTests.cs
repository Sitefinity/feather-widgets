using System;
using System.Collections.Generic;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.DynamicContent.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.TestIntegration.Data.Content;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Utilities.HtmlParsing;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.DynamicWidgets
{
    /// <summary>
    /// This is a test class with tests related to dynamic widgets and filtering by related data.
    /// </summary>
    [TestFixture]
    public class DynamicWidgetsFilterByRelatedDataTests
    {
        [FixtureSetUp]
        public void Setup()
        {
            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(Module1Name, Module1Resource);
            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(Module2Name, Module2Resource);
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.Team2)]
        [Description("Adds 2 MVC dynamic widgets on page and add child relation to one of the widgets, then verify the filtering on the frontend")]
        public void DynamicWidgets_ChildRelationType_DisplayRelatedItemsOnFrontend()
        {
            string url;
            string responseContent;
            string expectedContent;
            string pageName = "FeatherPage";
            string pageUrl = "featherpage";
            string item1 = "item1";
            string item2 = "item2";

            foreach (var color in this.colors)
            {
                ServerOperationsFeather.DynamicModule1Operations().CreateColor(color);
            }

            try
            {
                string[] item1RelatedColors = new string[] { this.colors[0], this.colors[1] };

                ServerOperationsFeather.DynamicModule2Operations().CreateItem(item1, item1RelatedColors);

                string[] item2RelatedColors = new string[] { this.colors[2], this.colors[3] };

                ServerOperationsFeather.DynamicModule2Operations().CreateItem(item2, item2RelatedColors);

                this.pageId = ServerOperations.Pages().CreatePage(pageName);

                var itemsWidget = this.CreateMvcWidget(ResolveTypeItem, WidgetNameItem);
                var colorsWidget = this.CreateMvcWidget(ResolveTypeColor, WidgetNameColor, RelatedFieldName, ResolveTypeItem);

                var controls = new List<System.Web.UI.Control>();
                controls.Add(itemsWidget);
                controls.Add(colorsWidget);

                PageContentGenerator.AddControlsToPage(this.pageId, controls);

                url = UrlPath.ResolveAbsoluteUrl("~/" + pageUrl + "/" + item1);
                responseContent = PageInvoker.ExecuteWebRequest(url);

                expectedContent = UrlPath.ResolveAbsoluteUrl("~/" + pageUrl + "/" + this.colors[0]);
                Assert.IsTrue(this.ContainsElement(responseContent, expectedContent), "Link to color " + this.colors[0] + " was not found on the frontend");

                expectedContent = UrlPath.ResolveAbsoluteUrl("~/" + pageUrl + "/" + this.colors[1]);
                Assert.IsTrue(this.ContainsElement(responseContent, expectedContent), "Link to color " + this.colors[1] + " was not found on the frontend");

                expectedContent = UrlPath.ResolveAbsoluteUrl("~/" + pageUrl + "/" + this.colors[2]);
                Assert.IsFalse(this.ContainsElement(responseContent, expectedContent), "Link to color " + this.colors[2] + " was found on the frontend, but it shouldn't");

                expectedContent = UrlPath.ResolveAbsoluteUrl("~/" + pageUrl + "/" + this.colors[5]);
                Assert.IsFalse(this.ContainsElement(responseContent, expectedContent), "Link to color " + this.colors[5] + " was found on the frontend, but it shouldn't");

                url = UrlPath.ResolveAbsoluteUrl("~/" + pageUrl + "/" + item2);
                responseContent = PageInvoker.ExecuteWebRequest(url);

                expectedContent = UrlPath.ResolveAbsoluteUrl("~/" + pageUrl + "/" + this.colors[2]);
                Assert.IsTrue(this.ContainsElement(responseContent, expectedContent), "Link to color " + this.colors[2] + " was not found on the frontend");

                expectedContent = UrlPath.ResolveAbsoluteUrl("~/" + pageUrl + "/" + this.colors[3]);
                Assert.IsTrue(this.ContainsElement(responseContent, expectedContent), "Link to color " + this.colors[3] + " was not found on the frontend");

                expectedContent = UrlPath.ResolveAbsoluteUrl("~/" + pageUrl + "/" + this.colors[0]);
                Assert.IsFalse(this.ContainsElement(responseContent, expectedContent), "Link to color " + this.colors[0] + " was found on the frontend, but it shouldn't");

                expectedContent = UrlPath.ResolveAbsoluteUrl("~/" + pageUrl + "/" + this.colors[5]);
                Assert.IsFalse(this.ContainsElement(responseContent, expectedContent), "Link to color " + this.colors[5] + " was found on the frontend, but it shouldn't");                
            }
            finally
            {
                ServerOperations.Pages().DeleteAllPages();
            }
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.Team2)]
        [Description("Adds 2 MVC dynamic widgets on page and add parent relation to one of the widgets, then verify the filtering on the frontend")]
        public void DynamicWidgets_ParentRelationType_DisplayRelatedItemsOnFrontend()
        {
            string url;
            string responseContent;
            string expectedContent;
            string pageName = "FeatherPage";
            string pageUrl = "featherpage";
            string item1 = "item1";
            string item2 = "item2";

            foreach (var color in this.colors)
            {
                ServerOperationsFeather.DynamicModule1Operations().CreateColor(color);
            }

            try
            {
                string[] item1RelatedColors = new string[] { this.colors[0], this.colors[1] };

                ServerOperationsFeather.DynamicModule2Operations().CreateItem(item1, item1RelatedColors);

                string[] item2RelatedColors = new string[] { this.colors[2], this.colors[3] };

                ServerOperationsFeather.DynamicModule2Operations().CreateItem(item2, item2RelatedColors);

                this.pageId = ServerOperations.Pages().CreatePage(pageName);

                var itemsWidget = this.CreateMvcWidget(ResolveTypeItem, WidgetNameItem, RelatedFieldName, ResolveTypeColor, RelationDirection.Parent);
                var colorsWidget = this.CreateMvcWidget(ResolveTypeColor, WidgetNameColor);

                var controls = new List<System.Web.UI.Control>();
                controls.Add(itemsWidget);
                controls.Add(colorsWidget);

                PageContentGenerator.AddControlsToPage(this.pageId, controls);

                url = UrlPath.ResolveAbsoluteUrl("~/" + pageUrl + "/" + this.colors[0]);
                responseContent = PageInvoker.ExecuteWebRequest(url);

                expectedContent = UrlPath.ResolveAbsoluteUrl("~/" + pageUrl + "/" + item1);
                Assert.IsTrue(this.ContainsElement(responseContent, expectedContent), "Link to " + item1 + " was not found on the frontend");

                expectedContent = UrlPath.ResolveAbsoluteUrl("~/" + pageUrl + "/" + item2);
                Assert.IsFalse(this.ContainsElement(responseContent, expectedContent), "Link to " + item2 + " was found on the frontend, but it shouldn't");              

                url = UrlPath.ResolveAbsoluteUrl("~/" + pageUrl + "/" + this.colors[3]);
                responseContent = PageInvoker.ExecuteWebRequest(url);

                expectedContent = UrlPath.ResolveAbsoluteUrl("~/" + pageUrl + "/" + item2);
                Assert.IsTrue(this.ContainsElement(responseContent, expectedContent), "Link to " + item2 + " was not found on the frontend");

                expectedContent = UrlPath.ResolveAbsoluteUrl("~/" + pageUrl + "/" + item1);
                Assert.IsFalse(this.ContainsElement(responseContent, expectedContent), "Link to " + item1 + " was found on the frontend, but it shouldn't");
            }
            finally
            {
                ServerOperations.Pages().DeleteAllPages();
            }
        }

        [FixtureTearDown]
        public void Teardown()
        {
            ServerOperations.ModuleBuilder().DeleteAllModules(this.providerName, TransactionName);
        }

        private MvcWidgetProxy CreateMvcWidget(string resolveType, string widgetName, string relatedFieldName = null, string relatedItemType = null, RelationDirection relationTypeToDisplay = RelationDirection.Child)
        {
            var mvcProxy = new MvcWidgetProxy();
            mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
            var dynamicController = new DynamicContentController();
            dynamicController.Model.ContentType = TypeResolutionService.ResolveType(resolveType);
            dynamicController.Model.RelatedFieldName = relatedFieldName;
            dynamicController.Model.RelatedItemType = relatedItemType;
            dynamicController.Model.RelationTypeToDisplay = relationTypeToDisplay;

            mvcProxy.Settings = new ControllerSettings(dynamicController);
            mvcProxy.WidgetName = widgetName;

            return mvcProxy;
        }

        private bool ContainsElement(string pageContent, string element)
        {
            using (HtmlParser parser = new HtmlParser(pageContent))
            {
                HtmlChunk chunk = null;
                parser.SetChunkHashMode(false);
                parser.AutoExtractBetweenTagsOnly = false;
                parser.CompressWhiteSpaceBeforeTag = false;
                parser.KeepRawHTML = true;

                while ((chunk = parser.ParseNext()) != null)
                {
                    if (chunk.TagName.Equals("h3"))
                    {
                        chunk = parser.ParseNextTag();

                        if (chunk.Html.Contains(element))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private const string Module1Name = "Module1";
        private const string Module2Name = "Module2";
        private const string Module1Resource = "FeatherWidgets.TestUtilities.Data.DynamicModules.Module1.zip";
        private const string Module2Resource = "FeatherWidgets.TestUtilities.Data.DynamicModules.Module2.zip";
        private const string TransactionName = "Module Installations";
        private const string ResolveTypeColor = "Telerik.Sitefinity.DynamicTypes.Model.Module1.Color";
        private const string ResolveTypeItem = "Telerik.Sitefinity.DynamicTypes.Model.Module2.Item";
        private const string WidgetNameItem = "Item";
        private const string WidgetNameColor = "Color";
        private const string RelatedFieldName = "RelatedColor";
        private Guid pageId = Guid.Empty;
        private string providerName = string.Empty;
        private string[] colors = new string[] { "blue", "yellow", "red", "green", "black", "purple", "pink" };
    }
}
