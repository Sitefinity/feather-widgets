using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Models.FlatTaxonomy
{
    public class FlatTaxonomyModel : TaxonomyModel, IFlatTaxonomyModel
    {
        #region Properties
        /// <summary>
        /// Determines what taxa will be displayed by the widget.
        /// </summary>
        /// <value>The taxa to display.</value>
        public FlatTaxaToDisplay TaxaToDisplay { get; set; }
        #endregion

        #region Overriden methods
        /// <summary>
        /// Gets the taxa with the usage metrics for each taxon filtered by one of the several display modes.
        /// </summary>
        /// <returns></returns>
        protected override IDictionary<ITaxon, uint> GetFilteredTaxaWithCount()
        {
            switch (this.TaxaToDisplay)
            {
                case FlatTaxaToDisplay.All:
                    return this.GetAllTaxa();
                case FlatTaxaToDisplay.Selected:
                    return this.GetSpecificTaxa();
                case FlatTaxaToDisplay.UsedByContentType:
                    return this.GetTaxaByContentType();
                default:
                    return this.GetAllTaxa();
            }
        }

        #endregion

        #region Protected methods
        protected virtual IDictionary<ITaxon, uint> GetAllTaxa()
        {
            var taxa = this.CurrentTaxonomyManager.GetTaxa<ITaxon>()
                .Where(t => t.Taxonomy.RootTaxonomyId == this.TaxonomyId);

            return this.AddCountToTaxa(taxa);
        }

        protected virtual IDictionary<ITaxon, uint> GetTaxaByContentType()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
