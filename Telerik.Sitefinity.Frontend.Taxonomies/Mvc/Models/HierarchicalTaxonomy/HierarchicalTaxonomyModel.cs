using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Models.HierarchicalTaxonomy
{
    /// <summary>
    /// The hierarchical taxonomy model.
    /// </summary>
    public class HierarchicalTaxonomyModel : TaxonomyModel, IHierarchicalTaxonomyModel
    {
        #region Properties
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

        /// <summary>
        /// If set to true, all hierarchical taxa will be shown as a flat list.
        /// </summary>
        /// <value>The flatten hierarchy.</value>
        public bool FlattenHierarchy { get; set; }
        #endregion

        #region Overriden methods
        /// <summary>
        /// Creates the view model.
        /// </summary>
        /// <returns></returns>
        public override TaxonomyViewModel CreateViewModel()
        {
            var viewModel = new TaxonomyViewModel();

            if (this.ContentId != Guid.Empty)
            {
                viewModel.Taxa = this.GetTaxaByContentItem();
                return viewModel;
            }

            switch (this.TaxaToDisplay)
            {
                case HierarchicalTaxaToDisplay.TopLevel:
                    viewModel.Taxa = this.GetTopLevelTaxa();
                    break;
                case HierarchicalTaxaToDisplay.UnderParticularTaxon:
                    viewModel.Taxa = this.GetTaxaByParent();
                    break;
                case HierarchicalTaxaToDisplay.Selected:
                    viewModel.Taxa = this.GetSpecificTaxa();
                    break;
                case HierarchicalTaxaToDisplay.UsedByContentType:
                    viewModel.Taxa = this.GetTaxaByContentType();
                    break;
                default:
                    viewModel.Taxa = this.GetTopLevelTaxa();
                    break;
            }
            return viewModel;
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

            return TaxaViewModelTreeBuilder.BuildTaxaTree(
                taxa,
                taxon => this.FilterTaxonByCount(taxon, statistics));
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
                return TaxaViewModelTreeBuilder.BuildTaxaTree(
                    rootTaxon.Subtaxa,
                    taxon => this.FilterTaxonByCount(taxon, statistics));
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

            var contentProviderName = this.GetContentProviderName();

            if (this.ContentType != null)
            {
                statistics = statistics.Where(s => s.DataItemType == this.ContentType.FullName);
            }

            if (!string.IsNullOrWhiteSpace(contentProviderName))
            {
                statistics = statistics.Where(s => s.ItemProviderName == contentProviderName);
            }

            var taxa = this.CurrentTaxonomyManager.GetTaxa<HierarchicalTaxon>()
                .Where(t => t.Taxonomy.Id == this.TaxonomyId && t.Parent == null);

            var taxaViewModels = TaxaViewModelTreeBuilder.BuildTaxaTree(
                taxa,
                t => this.FilterTaxonByCount(t, statistics));

            return taxaViewModels;
        }

        /// <summary>
        /// According to the usage count and the widget settings, returns a view model of the given taxon if it should be shown in the widget.
        /// Returns null if shouldn't be visible.
        /// </summary>
        /// <param name="taxon">The taxon.</param>
        /// <param name="statistics">The statistics.</param>
        /// <returns></returns>
        protected virtual TaxonViewModel FilterTaxonByCount(ITaxon taxon, IQueryable<TaxonomyStatistic> statistics)
        {
            var stats = statistics.Where(s => s.TaxonId == taxon.Id);

            var count = stats.Aggregate(0u, (acc, stat) => acc + stat.MarkedItemsCount);

            if (count == 0 && !this.ShowEmptyTaxa) return null;

            return new TaxonViewModel(taxon, count);
        }
        #endregion
    }
}
