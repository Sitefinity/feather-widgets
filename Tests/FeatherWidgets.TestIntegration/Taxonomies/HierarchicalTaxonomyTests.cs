using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using MbUnit.Framework;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Models;
using Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Models.HierarchicalTaxonomy;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.TestUtilities;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

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
            using (new ElevatedModeRegion(TaxonomyManager.GetManager()))
            {
                var taxonomyOperations = new TaxonomiesOperations();

                foreach (var taxon in this.taxaNamesWithParents)
                {
                    string parentName;
                    string taxonName;

                    this.GetTaxonNameAndParentName(taxon, out parentName, out taxonName);

                    taxonomyOperations.CreateHierarchicalTaxon(taxonName, parentName, "Categories");
                }

                var newsOperations = new NewsOperations();
                var newsItemId = Guid.NewGuid();
                newsOperations.CreateNewsItem("n1", newsItemId);

                taxonomyOperations.AddTaxonsToNews(newsItemId, new string[] { "c3", "c3c2c1" }, new string[0]);

                var newsManager = NewsManager.GetManager();

                // The item have to be published again after a taxon is asigned in order to update the taxonomy statistics.
                var newsItem = newsManager.GetNewsItem(newsItemId);
                newsItem.SetWorkflowStatus(newsManager.Provider.ApplicationName, "Published");
                newsManager.SaveChanges();
                newsManager.Lifecycle.Publish(newsItem);
                newsManager.SaveChanges();
            }
        }

        [FixtureTearDown]
        public void FixtureTearDown()
        {
            var newsOperations = new NewsOperations();
            newsOperations.DeleteAllNews();

            this.DeleteAllCategories();
        }

        [Test]
        [Author(TestAuthor.Team7)]
        [Description("Verifies that if the option to show all taxa is set, all taxa will be retrieved.")]
        public void Categories_VerifyAllTaxaIsRetrieved()
        {
            var model = new HierarchicalTaxonomyModel();
            model.TaxaToDisplay = HierarchicalTaxaToDisplay.All;
            model.ShowEmptyTaxa = true;

            var viewModel = model.CreateViewModel();

            Assert.AreEqual(this.taxaNamesWithParents.Count, viewModel.Taxa.Count);

            for (int i = 0; i < viewModel.Taxa.Count; i++)
            {
                string parentName;
                string taxonName;
                this.GetTaxonNameAndParentName(this.taxaNamesWithParents[i], out parentName, out taxonName);

                var actual = viewModel.Taxa[i];

                Assert.AreEqual(taxonName, actual.Title);
            }
        }

        [Test]
        [Author(TestAuthor.Team7)]
        [Description("Verifies that if the option to show specific taxa is set, the exactly same taxa will be retrieved.")]
        public void Categories_VerifySpecificTaxaIsRetrieved()
        {
            var model = new HierarchicalTaxonomyModel();
            model.TaxaToDisplay = HierarchicalTaxaToDisplay.Selected;
            model.ShowEmptyTaxa = true;

            var selectedTaxaIds = TaxonomyManager.GetManager()
                .GetTaxa<HierarchicalTaxon>()
                .Where(t => this.taxaNamesWithParents[1] == t.Title.ToString() || this.taxaNamesWithParents[2] == t.Title.ToString())
                .Select(t => t.Id);

            var serializer = new JavaScriptSerializer();
            model.SerializedSelectedTaxaIds = serializer.Serialize(selectedTaxaIds);

            var viewModel = model.CreateViewModel();

            Assert.AreEqual(2, viewModel.Taxa.Count);

            for (int i = 0; i < viewModel.Taxa.Count; i++)
            {
                var expected = this.taxaNamesWithParents[i + 1];
                var actual = viewModel.Taxa[i];

                Assert.AreEqual(expected, actual.Title);
            }
        }

        [Test]
        [Author(TestAuthor.Team7)]
        [Description("Verifies that only top level taxa is retrieved if that option is selected.")]
        public void Categories_VerifyTopLevelTaxaIsRetrieved()
        {
            var model = new HierarchicalTaxonomyModel();
            model.TaxaToDisplay = HierarchicalTaxaToDisplay.TopLevel;
            model.ShowEmptyTaxa = true;

            var viewModel = model.CreateViewModel();

            var topLevelNames = this.taxaNamesWithParents.Where(tn => !tn.Contains('|')).ToList();

            Assert.AreEqual(topLevelNames.Count, viewModel.Taxa.Count);

            for (int i = 0; i < viewModel.Taxa.Count; i++)
            {
                var expected = topLevelNames[i];
                var actual = viewModel.Taxa[i];

                Assert.AreEqual(expected, actual.Title);
            }
        }

        [Test]
        [Author(TestAuthor.Team7)]
        [Description("Verifies that only taxa under particular parent is retrieved if that option is selected.")]
        public void Categories_VerifyTaxaUnderParticularParentIsRetrieved()
        {
            var model = new HierarchicalTaxonomyModel();
            model.TaxaToDisplay = HierarchicalTaxaToDisplay.UnderParticularTaxon;
            model.ShowEmptyTaxa = true;

            var rootTaxon = TaxonomyManager.GetManager().GetTaxa<HierarchicalTaxon>().FirstOrDefault(t => t.Title == "c3");
            model.RootTaxonId = rootTaxon.Id;

            var viewModel = model.CreateViewModel();

            var innerLevelNames = this.taxaNamesWithParents
                .Where(tn => tn.Contains("c3|"))
                .Select(tn => tn.Split('|')[1])
                .ToList();

            Assert.AreEqual(innerLevelNames.Count, viewModel.Taxa.Count);

            for (int i = 0; i < viewModel.Taxa.Count; i++)
            {
                var expected = innerLevelNames[i];
                var actual = viewModel.Taxa[i];

                Assert.AreEqual(expected, actual.Title);
            }
        }

        [Test]
        [Author(TestAuthor.Team7)]
        [Description("Verifies that only taxa filtered by content type is retrieved if that option is selected.")]
        public void Categories_VerifyTaxaFilteredByContentTypeIsRetrieved()
        {
            var model = new HierarchicalTaxonomyModel();
            model.TaxaToDisplay = HierarchicalTaxaToDisplay.UsedByContentType;
            model.ContentTypeName = typeof(NewsItem).FullName;

            var viewModel = model.CreateViewModel();

            var expectedFilteredTaxa = new List<string>() { "c1", "c3", "c3|c3c2c1" };

            Assert.AreEqual(expectedFilteredTaxa.Count, viewModel.Taxa.Count);

            for (var i = 0; i < expectedFilteredTaxa.Count; i++)
            {
                var actual = viewModel.Taxa[i];

                var expectedTaxon = expectedFilteredTaxa[i];

                string taxonParentName;
                string taxonName;
                this.GetTaxonNameAndParentName(expectedTaxon, out taxonParentName, out taxonName);

                Assert.AreEqual(taxonName, actual.Title);
            }
        }

        [Test]
        [Author(TestAuthor.Team7)]
        [Description("Verifies that only taxa that is assigned to a content item is shown when ShowEmptyTaxa is disabled.")]
        public void Categories_VerifyEmptyTaxaIsNotRetrieved()
        {
            var model = new HierarchicalTaxonomyModel();
            model.TaxaToDisplay = HierarchicalTaxaToDisplay.All;

            var viewModel = model.CreateViewModel();

            var expectedFilteredTaxa = new List<string>() { "c1", "c3", "c3|c3c2c1" };

            Assert.AreEqual(expectedFilteredTaxa.Count, viewModel.Taxa.Count);

            for (var i = 0; i < expectedFilteredTaxa.Count; i++)
            {
                var actual = viewModel.Taxa[i];

                var expectedTaxon = expectedFilteredTaxa[i];

                string taxonParentName;
                string taxonName;
                this.GetTaxonNameAndParentName(expectedTaxon, out taxonParentName, out taxonName);

                Assert.AreEqual(taxonName, actual.Title);
            }
        }

        [Test]
        [Author(TestAuthor.Team7)]
        [Description("Verifies that only taxa from a specific content item is shown when ContentId is set.")]
        public void Categories_VerifyTaxaFromContentItemIsRetrieved()
        {
            var item = NewsManager.GetManager().GetNewsItems().FirstOrDefault();

            var model = new HierarchicalTaxonomyModel();
            model.ContentId = item.Id;

            var viewModel = model.CreateViewModel();

            var itemTaxa = (IList<Guid>)item.GetValue("Category");

            Assert.AreEqual(itemTaxa.Count, viewModel.Taxa.Count);

            var taxonomyManager = TaxonomyManager.GetManager();
            for (var i = 0; i < itemTaxa.Count; i++)
            {
                var taxon = taxonomyManager.GetTaxa<HierarchicalTaxon>().FirstOrDefault(t => t.Id == itemTaxa[i]);
                Assert.AreEqual(taxon.Title.ToString(), viewModel.Taxa[i].Title);
            }
        }

        [Test]
        [Author(TestAuthor.Team7)]
        [Description("Verifies that hierarchical taxon view model is constructed if FlattenHierarchy is turned off.")]
        public void Categories_VerifyHierarchicalTaxaIsRetrieved()
        {
            var model = new HierarchicalTaxonomyModel();
            model.TaxaToDisplay = HierarchicalTaxaToDisplay.TopLevel;
            model.ShowEmptyTaxa = true;
            model.FlattenHierarchy = false;

            var viewModel = model.CreateViewModel();

            var expectedTaxaTree = this.GetExpectedTaxaTree(this.taxaNamesWithParents);

            foreach (var taxon in expectedTaxaTree)
            {
                this.AssertTaxonIsInPlace(viewModel.Taxa, taxon);
            }
        }

        [Test]
        [Author(TestAuthor.Team7)]
        [Description("Verifies that hierarchical taxon view model filtered by content type is constructed if FlattenHierarchy is turned off.")]
        public void Categories_VerifyHierarchicalTaxaFilteredByContentTypeIsRetrieved()
        {
            var model = new HierarchicalTaxonomyModel();
            model.TaxaToDisplay = HierarchicalTaxaToDisplay.UsedByContentType;
            model.ContentTypeName = typeof(NewsItem).FullName;
            model.FlattenHierarchy = false;

            var viewModel = model.CreateViewModel();

            var expectedTaxaTree = this.GetExpectedTaxaTree(new List<string>() { "c1", "c3", "c3|c3c2c1" });

            foreach (var taxon in expectedTaxaTree)
            {
                this.AssertTaxonIsInPlace(viewModel.Taxa, taxon);
            }
        }

        /// <summary>
        /// Asserts the expected taxon is in the right place in the tree.
        /// </summary>
        /// <param name="taxa">The taxa.</param>
        /// <param name="expectedTaxon">The expectedTaxon. Tuple<[expectedTaxon title], [expectedTaxon parent title]></param>
        private void AssertTaxonIsInPlace(IList<TaxonViewModel> taxa, Tuple<string, string> expectedTaxon)
        {
            // Tupple<[Taxon], [ParentTite]>
            var queue = new Queue<Tuple<TaxonViewModel, string>>(
                taxa.Select(t => new Tuple<TaxonViewModel, string>(t, string.Empty)));

            while (queue.Count > 0)
            {
                var currentTaxon = queue.Dequeue();
                if (currentTaxon.Item1.Title == expectedTaxon.Item1)
                {
                    Assert.AreEqual(expectedTaxon.Item2, currentTaxon.Item2);
                    return;
                }

                foreach (var childTaxon in currentTaxon.Item1.SubTaxa)
                {
                    queue.Enqueue(new Tuple<TaxonViewModel, string>(childTaxon, currentTaxon.Item1.Title));
                }
            }
        }

        /// <summary>
        /// Gets the expected taxa tree as list of parents.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Tuple<string, string>> GetExpectedTaxaTree(List<string> taxaNames)
        {
            return taxaNames.Select((tn) =>
            {
                string parentName;
                string taxonName;
                this.GetTaxonNameAndParentName(tn, out parentName, out taxonName);

                return new Tuple<string, string>(taxonName, parentName);
            });
        }

        private void DeleteAllCategories()
        {
            var manager = TaxonomyManager.GetManager();
            using (new ElevatedModeRegion(manager))
            {
                var taxa = manager.GetTaxa<HierarchicalTaxon>();
                foreach (var taxon in taxa)
                {
                    manager.DeleteItem(taxon);
                }

                manager.SaveChanges();
            }
        }

        private void GetTaxonNameAndParentName(string taxon, out string parentName, out string taxonName)
        {
            var parts = taxon.Split('|');
            parentName = parts.Length == 2 ? parts[0] : string.Empty;
            taxonName = parts.Length == 2 ? parts[1] : parts[0];
        }

        /// <summary>
        /// Parent name format - "[parent title]|[taxon title]"
        /// </summary>
        private List<string> taxaNamesWithParents = new List<string>() { "c1", "c2", "c3", "c3|c3c1", "c3|c3c2", "c3c2|c3c2c1" };
    }
}
