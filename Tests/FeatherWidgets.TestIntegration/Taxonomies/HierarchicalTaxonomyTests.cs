using System;
using System.Collections.Generic;
using System.Linq;
using MbUnit.Framework;
using Telerik.Sitefinity.TestUtilities;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Taxonomies;

namespace FeatherWidgets.TestIntegration.Taxonomies
{
    [TestFixture]
    [AssemblyFixture]
    [Description("Integration tests for the Hierarchical taxonomy model.")]
    [Author(TestAuthor.Team7)]
    public class HierarchicalTaxonomyTests
    {
        [FixtureSetUp]
        public void FixtureSetUp()
        {
            var taxonomyOperations = new TaxonomiesOperations();

            foreach (var taxon in this.taxaNames)
            {
                taxonomyOperations.CreateHierarchicalTaxon(TaxonomyManager.CategoriesTaxonomyId, taxon);
            }

            var newsOperations = new NewsOperations();
            var newsItemsId = Guid.NewGuid();
            newsOperations.CreateNewsItem("n1", newsItemsId);

            taxonomyOperations.AddTaxonsToNews(newsItemsId, this.taxaNames.Take(1), new List<string>());
        }

        [FixtureTearDown]
        public void FixtureTearDown()
        {
            var newsOperations = new NewsOperations();
            newsOperations.DeleteAllNews();

            var taxonomyOperations = new TaxonomiesOperations();
            taxonomyOperations.DeleteCategories(this.taxaNames.ToArray());
        }

        private List<string> taxaNames = new List<string>() { "c1", "c2", "c3" };
    }
}
