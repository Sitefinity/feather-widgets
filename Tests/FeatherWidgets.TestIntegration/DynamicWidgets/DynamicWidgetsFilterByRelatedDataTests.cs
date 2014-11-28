using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DynamicContent.Mvc.Controllers;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.TestIntegration.Data.Content;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.DynamicWidgets
{
    /// <summary>
    /// This is a test class with tests related to dynamic widgets and filtering by related data.
    /// </summary>
    public class DynamicWidgetsFilterByRelatedDataTests
    {
        [FixtureSetUp]
        public void Setup()
        {
            ServerOperationsFeather.DynamicModules().ImportModule(Module1Resource);
            ServerOperations.ModuleBuilder().ActivateModule(Module1Resource, this.providerName, TransactionName);
            ServerOperationsFeather.DynamicModules().ImportModule(Module2Resource);
            ServerOperations.ModuleBuilder().ActivateModule(Module2Resource, this.providerName, TransactionName);

            string[] colors = new string[] { "purple", "pink", "red", "green", "black", "yellow", "blue" };

            foreach (var color in colors)
            {
                ServerOperationsFeather.DynamicModule1Operations().CreateColor(color);
            }
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author("FeatherTeam")]
        [Description("Adds 2 MVC dynamic widgets on page and add child relation to one of the widgets, then verify the filtering on the frontend")]
        [Ignore("not finished because of a bug")]
        public void DynamicWidgets_ChildRelationType_DisplayRelatedItemsOnFrontend()
        {
            try
            {
                var colorBlueId = ServerOperationsFeather.DynamicModule1Operations().GetColorIdByTitle("blue");
                var colorGreenId = ServerOperationsFeather.DynamicModule1Operations().GetColorIdByTitle("green");

                Guid[] item1RelatedIds = new Guid[2] { colorBlueId, colorGreenId };
              
                ServerOperationsFeather.DynamicModule2Operations().CreateItem("item1", item1RelatedIds);

                var colorRedId = ServerOperationsFeather.DynamicModule1Operations().GetColorIdByTitle("red");
                var colorYellowId = ServerOperationsFeather.DynamicModule1Operations().GetColorIdByTitle("yellow");

                Guid[] item2RelatedIds = new Guid[2] { colorRedId, colorYellowId };

                ServerOperationsFeather.DynamicModule2Operations().CreateItem("item2", item2RelatedIds);

                this.pageId = ServerOperations.Pages().CreatePage(PageName);

                var itemsWidget = this.CreateMvcWidget(ResolveTypeItem, WidgetNameItem);
                var colorsWidget = this.CreateMvcWidget(ResolveTypeColor, WidgetNameColor, RelatedFieldName, ResolveTypeItem);

                var controls = new List<System.Web.UI.Control>();
                controls.Add(itemsWidget);
                controls.Add(colorsWidget);

                PageContentGenerator.AddControlsToPage(this.pageId, controls);

                string url = UrlPath.ResolveAbsoluteUrl("~/" + PageName + "/item1");
                string responseContent = PageInvoker.ExecuteWebRequest(url);
                string expectedContent1 = "<a href=" + "/" + "test/blue" + ">";
                string expectedContent2 = "<a href=" + "/" + "test/green" + ">";
                Assert.IsTrue(responseContent.Contains(expectedContent1));
                Assert.IsTrue(responseContent.Contains(expectedContent2));

                url = UrlPath.ResolveAbsoluteUrl("~/" + PageName + "/item2");
                responseContent = PageInvoker.ExecuteWebRequest(url);
                expectedContent1 = "<a href=" + "/" + "test/red" + ">";
                expectedContent2 = "<a href=" + "/" + "test/yellow" + ">";
                Assert.IsTrue(responseContent.Contains(expectedContent1));
                Assert.IsTrue(responseContent.Contains(expectedContent2));
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
        private const string PageName = "FeatherPage";
    }
}
