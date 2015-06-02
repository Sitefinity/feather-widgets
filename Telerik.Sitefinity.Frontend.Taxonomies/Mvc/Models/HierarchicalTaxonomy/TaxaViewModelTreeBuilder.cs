using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Models.HierarchicalTaxonomy
{
    /// <summary>
    /// A helper class that creates trees of taxon view models that represents the taxa hierarchy.
    /// It allows filtering of the taxa that should be included in the trees.
    /// </summary>
    public class TaxaViewModelTreeBuilder
    {
        /// <summary>
        /// Creates trees of view models that represents the hierarchy of the given taxa.
        /// A method delegate for creating a taxon view model should be provided.
        /// If the method returns null, the taxon won't be included in the tree.
        /// The taxa hierarchy is traversed in a breadth-first manner so the ordering of the taxa in each level is preserved.
        /// </summary>
        /// <param name="taxa">The taxa.</param>
        /// <param name="viewModelBuilder">A method that creates a taxon view model by given taxon.
        /// If null is returned, the taxon won't be included in the tree.</param>
        /// <param name="taxaCountLimit">The maximum number of taxa that will be included in the tree.</param>
        /// <returns></returns>
        public static IList<TaxonViewModel> BuildTaxaTree(IEnumerable<HierarchicalTaxon> taxa, Func<ITaxon, TaxonViewModel> viewModelBuilder, int taxaCountLimit)
        {
            var trees = new List<TaxonViewModel>();
            var currentTaxaCount = 0;
            foreach (var taxon in taxa)
            {
                var subTree = TaxaViewModelTreeBuilder.BuildTaxaTreeBfs(taxon, viewModelBuilder, taxaCountLimit, ref currentTaxaCount);
                if (subTree != null)
                {
                    trees.Add(subTree);
                }

                if (taxaCountLimit > 0 && currentTaxaCount >= taxaCountLimit)
                {
                    break;
                }
            }

            return trees;
        }

        private static TaxonViewModel BuildTaxaTreeBfs(HierarchicalTaxon taxon, Func<ITaxon, TaxonViewModel> viewModelBuilder, int taxaCountLimit, ref int currentTaxaCount)
        {
            var queue = new Queue<TaxonData>();
            TaxonViewModel rootViewModel = null;

            queue.Enqueue(new TaxonData() { Taxon = taxon });

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();

                var currentViewModel = viewModelBuilder.Invoke(currentNode.Taxon);

                if (currentViewModel != null)
                {
                    // If this is the first created view model, set it to be the root one.
                    if (rootViewModel == null) rootViewModel = currentViewModel;

                    if (currentNode.LastKnownParent != null)
                    {
                        currentNode.LastKnownParent.SubTaxa.Add(currentViewModel);
                    }

                    currentTaxaCount++;
                    if (taxaCountLimit > 0 && currentTaxaCount == taxaCountLimit)
                    {
                        return rootViewModel;
                    }
                }

                // If the current taxon is included in the tree, it should be the parent of the inner taxa.
                var lastKnownParent = currentViewModel ?? currentNode.LastKnownParent;

                foreach (var childTaxon in currentNode.Taxon.Subtaxa)
                {
                    queue.Enqueue(new TaxonData() 
                    {
                        LastKnownParent = lastKnownParent,
                        Taxon = childTaxon
                    });
                }
            }

            return rootViewModel;
        }

        private class TaxonData
        {
            /// <summary>
            /// Gets or sets the taxon that is currently examined.
            /// </summary>
            /// <value>The taxon.</value>
            public HierarchicalTaxon Taxon { get; set; }

            /// <summary>
            /// Gets or sets the last known parent in the newly constructed tree.
            /// </summary>
            /// <value>The last known parent.</value>
            public TaxonViewModel LastKnownParent { get; set; }
        }
    }
}
