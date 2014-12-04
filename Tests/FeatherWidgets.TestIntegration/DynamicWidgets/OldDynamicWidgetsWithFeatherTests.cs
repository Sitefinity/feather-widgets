﻿using System;
using System.Globalization;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.DynamicWidgets
{
    /// <summary>
    /// This is a test class with tests related to old dynamic widgets when module is created with Feather.
    /// </summary>
    [TestFixture]
    [Description("This is a test class with tests related to old dynamic widgets when module is created with Feather.")]
    public class OldDynamicWidgetsWithFeatherTests
    {
        [FixtureSetUp]
        public void Setup()
        {
            ServerOperationsFeather.DynamicModules().ImportModule(ModuleResource);
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.ModuleBuilder().ActivateModule(ModuleName, string.Empty, TransactionName);

            for (int i = 1; i <= 3; i++)
            {
                string countryName = "Country" + i.ToString(CultureInfo.InvariantCulture);

                ServerOperationsFeather.DynamicModuleBooking()
                    .CreateCountry(countryName);
            }
        }

        [SetUp]
        public void TestSetup()
        {
            this.pageId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageName);
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author("FeatherTeam")]
        [Description("Adds all hierarchical MVC dynamic widgets on page and verifies the proper cities are displayed when selecting a country on the frontend.")]
        public void OldDynamicWidgetsWithFeatherTests_OldDynamicWidget()
        {
            try
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.ModuleBuilder()
                    .AddCustomContentTypeWidgetToPage(ResolveTypeCountry, "Booking - Country", WidgetNameCountries, "Body", this.pageId);

                string url = UrlPath.ResolveAbsoluteUrl("~/" + PageName);

                string responseContent = PageInvoker.ExecuteWebRequest(url);

                for (int i = 1; i <= 3; i++)
                    Assert.IsTrue(responseContent.Contains("Country" + i.ToString(CultureInfo.InvariantCulture)), "The dynamic item with this title was found!");
            }
            finally
            {
               Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
            }
        }

        [FixtureTearDown]
        public void Teardown()
        {
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.ModuleBuilder().DeleteAllModules(string.Empty, TransactionName);
        }

        #region Fields and constants

        private const string ModuleName = "Booking";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.Booking.zip";
        private const string TransactionName = "Module Installations";
        private const string PageName = "TestPage";
        private const string ResolveTypeCountry = "Telerik.Sitefinity.DynamicTypes.Model.Booking.Country";
        private const string WidgetNameCountries = "Countries";
        private Guid pageId = Guid.Empty;

        #endregion
    }
}
