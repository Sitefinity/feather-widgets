using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Models.FlatTaxonomy
{
    public class FlatTaxonomyModel : TaxonomyModel, IFlatTaxonomyModel
    {
        public FlatTaxonomyModel()
        {
            this.TaxonomyId = TaxonomyManager.TagsTaxonomyId;

            if (string.IsNullOrEmpty(this.FieldName))
            {
                this.FieldName = DefaultFieldName;
            }
        }

        #region Properties
        /// <summary>
        /// Determines what taxa will be displayed by the widget.
        /// </summary>
        /// <value>The taxa to display.</value>
        public FlatTaxaToDisplay TaxaToDisplay { get; set; }

        #endregion

        #region Overriden methods
        /// <summary>
        /// Creates the view model.
        /// </summary>
        /// <returns></returns>
        public override TaxonomyViewModel CreateViewModel()
        {
            var viewModel = new TaxonomyViewModel { ShowItemCount = this.ShowItemCount };

            if (this.ContentId != Guid.Empty)
            {
                viewModel.Taxa = this.GetTaxaByContentItem();
                return viewModel;
            }

            switch (this.TaxaToDisplay)
            {
                case FlatTaxaToDisplay.Selected:
                    viewModel.Taxa = this.GetSpecificTaxa<FlatTaxon>();
                    break;

                case FlatTaxaToDisplay.UsedByContentType:
                    viewModel.Taxa = this.GetTaxaByContentType();
                    break;

                default:
                    viewModel.Taxa = this.GetAllTaxa<FlatTaxon>();
                    break;

            }

            return viewModel;
        }

        #endregion

        #region Private
        /// <summary>
        /// Creates list of view models each representing a taxon that is used by the content type that the widget is set to work with.
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

            var taxa = this.Sort(CurrentTaxonomyManager.GetTaxa<FlatTaxon>()
                                                                    .Where(t => t.Taxonomy.Id == this.TaxonomyId));

            return this.GetFlatTaxaViewModelsWithStatistics(taxa, statistics);
        }
        #endregion

        #region Private fields
        private const string DefaultFieldName = "Tags";
        #endregion
    }
}
