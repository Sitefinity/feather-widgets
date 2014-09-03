using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using News.Mvc.Models;

namespace FeatherWidgets.TestUnit.News
{
    /// <summary>
    /// Tests methods of the NewsModel class.
    /// </summary>
    [TestClass]
    public class NewsModelTests
    {
        #region CompileFilterExpression
        
        /// <summary>
        /// Checks whether the CompileFilterExpression method compiles a correct filter with taxonomy filter and custom filter.
        /// </summary>
        [TestMethod]
        [Owner("Boyko-Karadzhov")]
        [Description("Checks whether the CompileFilterExpression method compiles a correct filter with taxonomy filter and custom filter.")]
        public void CompileFilterExpression_TaxonFilterAndCustomFilter_CompiledCorrectly()
        {
            var tag1Id = new Guid("726c8b4f-743f-43f8-9c04-8737b43f507c");
            var tag2Id = new Guid("cb4009e1-e971-49c6-9024-c7d41050639d");

            var model = new NewsModel();
            model.SelectionMode = NewsSelectionMode.FilteredNews;
            model.TaxonomyFilter = new Dictionary<string, IList<Guid>>();
            model.TaxonomyFilter["Tags"] = new List<Guid>() { tag1Id, tag2Id };
            model.FilterExpression = "MyField = 5";

            var result = model.CompileFilterExpression();

            Assert.AreEqual("((Tags.Contains((726c8b4f-743f-43f8-9c04-8737b43f507c)) OR Tags.Contains((cb4009e1-e971-49c6-9024-c7d41050639d)))) AND (MyField = 5)", result, "Compiled filter expression was not as expected.");
        }

        #endregion
    }
}
