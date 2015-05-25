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
            var viewModel = new TaxonomyViewModel();
            
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
                    //viewModel.Taxa = this.GetTaxaByContentType();
                    viewModel.Taxa = this.GetTaxaByContentItem();
                    break;

                default:
                    break;
                    //return this.GetAllTaxa();
            }

            this.PopulateCloudSize(viewModel.Taxa);

            return viewModel;
        }
        #endregion

        #region Private methods

        /// <summary>
        /// Populates the taxon size used for Cloud template.
        /// </summary>
        /// <param name="taxa">The taxa.</param>
        private void PopulateCloudSize(IList<TaxonViewModel> taxa)
        {
            List<double> counts = taxa.Select(x => x.Count).Select(t => (double)t).ToList();

            double average;
            var stdDev = this.StandardDeviation(counts, out average);

            foreach (var item in taxa)
            {
                item.CloudSize = this.GetSize(item.Count, average, stdDev);
            }
        }

        /// <summary>
        /// Calculates standard deviation
        /// </summary>       
        private double StandardDeviation(ICollection<double> data, out double average)
        {
            double squaresSum = 0;
            average = data.Average();

            foreach (double number in data)
            {
                squaresSum += Math.Pow((number - average), 2);
            }

            var n = (double)data.Count;
            return Math.Sqrt(squaresSum / (n - 1));
        }

        /// <summary>
        /// The size is calculated by the occurrence (count) of the taxa
        /// in relation to the mean value and the standard deviation.
        /// </summary>
        private int GetSize(double count, double average, double stdDev)
        {
            double sizeFactor = (count - average);

            if (sizeFactor != 0 && stdDev != 0)
            {
                sizeFactor = sizeFactor / stdDev;
            }

            if (sizeFactor > 2)
            {
                return 6;
            }
            if (sizeFactor > 1.33 && sizeFactor <= 2)
            {
                return 5;
            }
            if (sizeFactor > 0.67 && sizeFactor <= 1.33)
            {
                return 4;
            }
            if (sizeFactor > -0.67 && sizeFactor <= 0.67)
            {
                return 3;
            }
            if (sizeFactor > -1.33 && sizeFactor <= -0.67)
            {
                return 2;
            }
            return 1;
        }

        #endregion
    }
}
