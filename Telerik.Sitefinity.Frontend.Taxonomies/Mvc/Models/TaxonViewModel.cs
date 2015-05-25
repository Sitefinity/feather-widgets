using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Models
{
    /// <summary>
    /// This class represents the view model of a Taxon item.
    /// </summary>
    public class TaxonViewModel
    {
        #region Construction
        public TaxonViewModel(ITaxon taxon, uint count)
        {
            this.Count = count;
            this.Title = taxon.Title;

            this.SubTaxa = new List<TaxonViewModel>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the sub taxa of the taxon if it is hierarchical.
        /// </summary>
        /// <value>The sub taxa.</value>
        public IList<TaxonViewModel> SubTaxa { get; set; }

        /// <summary>
        /// Gets or sets how many content items are classified by this taxon.
        /// </summary>
        /// <value>The count.</value>
        public uint Count { get; set; }

        /// <summary>
        /// Gets or sets the title of the taxon.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the url of the taxon.
        /// </summary>
        /// <value>The url.</value>
        public string Url { get; set; }
        #endregion
    }
}
