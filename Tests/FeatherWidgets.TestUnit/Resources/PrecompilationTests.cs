using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.News.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUnit.Resources
{
    /// <summary>
    /// Tests precompilation.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Precompilation")]
    [TestClass]
    public class PrecompilationTests
    {
        /// <summary>
        /// Tests whether the views of Telerik.Sitefinity.Frontend are precompiled.
        /// </summary>
        [TestMethod]
        [Owner("Boyko-Karadzhov")]
        [Description("Tests whether the views of Telerik.Sitefinity.Frontend.News are precompiled.")]
        public void NewsAssembly_HasPrecompiledViews()
        {
            string[] failedViews;
            Assert.IsTrue(AssemblyLoaderHelper.EnsurePrecompiledRazorViews(typeof(NewsController).Assembly, out failedViews), "Some views are not precompiled: " + string.Join(", ", failedViews));
        }
    }
}
