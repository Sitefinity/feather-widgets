using System;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestUnit.Identity
{
    /// <summary>
    /// Tests the PageRouteHandler class.
    /// </summary>
    [TestClass]
    public class PageRouteHandlerHelperTest
    {
        [TestMethod]
        [Description("Tests whether RegisterCustomOutputCacheVariation method in PageRouteHandler exists.")]
        [Owner("Martin Nikolov")]
        public void RegisterCustomOutputCacheVariation_MethodExists()
        {
            var pageRouteHandlerType = typeof(PageRouteHandler);
            var method = pageRouteHandlerType.GetMethod(ProfileController.RegisterOCVariationMethodName, BindingFlags.NonPublic | BindingFlags.Static, null, new Type[] { typeof(ICustomOutputCacheVariation) }, null);

            Assert.IsNotNull(method);
        }
    }
}