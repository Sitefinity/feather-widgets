using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Taxonomies.Model;
using SfModel = Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Models.HierarchicalTaxonomy
{
    /// <summary>
    /// The hierarchical taxonomy model.
    /// </summary>
    public class HierarchicalTaxonomyModel : TaxonomyModel, IHierarchicalTaxonomyModel
    {
        /// <summary>
        /// Determines what taxa will be displayed by the widget.
        /// </summary>
        /// <value>The taxa to display.</value>
        public HierarchicalTaxaToDisplay TaxaToDisplay { get; set; }

        /// <summary>
        /// Determines how many levels from the hierarchy to include.
        /// </summary>
        /// <value>The levels.</value>
        public int Levels { get; set; }

        /// <summary>
        /// Gets or sets the root taxon which children will be displayed as a top level in the widget.
        /// Used only if this display mode is selected.
        /// </summary>
        /// <value>The parent category.</value>
        public Guid RootTaxonId { get; set; }

        #region Overriden methods
        /// <summary>
        /// Creates the view model.
        /// </summary>
        /// <returns></returns>
        public override TaxonomyViewModel CreateViewModel()
        {
            var viewModel = new TaxonomyViewModel();
            switch (this.TaxaToDisplay)
            {
                case HierarchicalTaxaToDisplay.TopLevel:
                    viewModel.Taxa = this.GetTopLevelTaxa();
                    break;
                case HierarchicalTaxaToDisplay.UnderParticularTaxon:
                    viewModel.Taxa = this.GetTaxaByParent();
                    break;
                case HierarchicalTaxaToDisplay.Selected:
                    break;
                    //return this.GetSpecificTaxa();
                case HierarchicalTaxaToDisplay.UsedByContentType:
                    viewModel.Taxa = this.GetTaxaByContentType();
                    break;
                default:
                    break;
                    //return this.GetAllTaxa();
            }
            return viewModel;
        }

        /// <summary>
        /// Gets the taxa with the usage metrics for each taxon filtered by one of the several display modes.
        /// </summary>
        /// <returns></returns>
        protected override IDictionary<ITaxon, uint> GetFilteredTaxaWithCount()
        {
            return null;
        }

        #endregion

        #region Protected methods
        /// <summary>
        /// Gets the taxa view model trees starting from the root level.
        /// </summary>
        /// <returns></returns>
        protected virtual IList<TaxonViewModel> GetTopLevelTaxa()
        {
            var statistics = this.GetTaxonomyStatistics();

            var taxa = this.CurrentTaxonomyManager.GetTaxa<HierarchicalTaxon>()
                 .Where(t => t.Taxonomy.Id == this.TaxonomyId && t.Parent == null);

            var taxaViewModels = BuildFullTaxaTrees(statistics, taxa);

            return taxaViewModels;
        }

        /// <summary>
        /// Gets the taxa view model trees starting from the children of the provided parent id.
        /// </summary>
        /// <returns></returns>
        protected virtual IList<TaxonViewModel> GetTaxaByParent()
        {
            var statistics = this.GetTaxonomyStatistics();

            var rootTaxon = this.CurrentTaxonomyManager.GetTaxon(this.RootTaxonId) as HierarchicalTaxon;

            if (rootTaxon != null)
            {
                return this.BuildFullTaxaTrees(statistics, rootTaxon.Subtaxa);
            }

            return new List<TaxonViewModel>();
        }

        /// <summary>
        /// Creates trees of view models each representing a taxon that is used by the content type that the widget is set to work with.
        /// </summary>
        /// <returns></returns>
        protected virtual IList<TaxonViewModel> GetTaxaByContentType()
        {
            var statistics = this.GetTaxonomyStatistics();

            var taxa = this.CurrentTaxonomyManager.GetTaxa<HierarchicalTaxon>()
                .Where(t => t.Taxonomy.Id == this.TaxonomyId && t.Parent == null);

            var contentProviderName = this.GetContentProviderName();

            var taxaViewModels = TaxaViewModelTreeBuilder.BuildTaxaTree(
                taxa,
                t =>
                {
                    var stats = statistics.Where(s => s.TaxonId == t.Id);

                    if (this.ContentType != null)
	                {
		                stats = stats.Where(s => s.DataItemType == this.ContentType.FullName);
	                }
                    if (!string.IsNullOrWhiteSpace(contentProviderName))
	                {
		                stats = stats.Where(s => s.ItemProviderName == contentProviderName);
	                }

                    //TODO: check the setting Include empty taxa is set to true
                    if (stats.Count() > 0)
                    {
                        var count = stats.Aggregate(0u, (acc, stat) => acc + stat.MarkedItemsCount);
                        return new TaxonViewModel(t, count);
                    }

                    return null;
                });

            return taxaViewModels;
        }

        private IList<TaxonViewModel> BuildFullTaxaTrees(IQueryable<TaxonomyStatistic> statistics, IEnumerable<HierarchicalTaxon> taxa)
        {
            var taxaViewModels = TaxaViewModelTreeBuilder.BuildTaxaTree(
                taxa,
                t =>
                {
                    var stats = statistics.Where(s => s.TaxonId == t.Id);

                    //TODO: check the setting Include empty taxa is set to true
                    if (stats.Count() > 0)
                    {
                        var count = stats.Aggregate(0u, (acc, stat) => acc + stat.MarkedItemsCount);
                        return new TaxonViewModel(t, count);
                    }

                    return null;
                });

            return taxaViewModels;
        }
        #endregion
    }
}
