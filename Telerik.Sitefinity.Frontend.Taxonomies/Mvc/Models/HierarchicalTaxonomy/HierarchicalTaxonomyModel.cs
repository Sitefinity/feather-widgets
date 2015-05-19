using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Taxonomies.Model;

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
        /// Gets the taxa with the usage metrics for each taxon filtered by one of the several display modes.
        /// </summary>
        /// <returns></returns>
        protected override IDictionary<ITaxon, uint> GetFilteredTaxaWithCount()
        {
            switch (this.TaxaToDisplay)
            {
                case HierarchicalTaxaToDisplay.TopLevel:
                    return this.GetAllTaxa();
                case HierarchicalTaxaToDisplay.UnderParticularTaxon:
                    return this.GetTaxaByParent();
                case HierarchicalTaxaToDisplay.Selected:
                    return this.GetSpecificTaxa();
                case HierarchicalTaxaToDisplay.UsedByContentType:
                    return GetTaxaByContentType();
                default:
                    return this.GetAllTaxa();
            }
        }

        #endregion

        #region Protected methods
        protected virtual IDictionary<ITaxon, uint> GetAllTaxa()
        {
            var taxa = this.Taxonomy.Taxa;

            return this.AddCountToTaxa(taxa);
        }

        protected virtual IDictionary<ITaxon, uint> GetTaxaByContentType()
        {
            throw new NotImplementedException();
        }

        protected virtual IDictionary<ITaxon, uint> GetTaxaByParent()
        {
            var rootTaxon = this.CurrentTaxonomyManager.GetTaxon(this.RootTaxonId) as HierarchicalTaxon;

            if (rootTaxon != null)
	        {
		        return this.AddCountToTaxa(rootTaxon.Subtaxa);
	        }

            return new Dictionary<ITaxon, uint>();
        }
        #endregion
    }
}
