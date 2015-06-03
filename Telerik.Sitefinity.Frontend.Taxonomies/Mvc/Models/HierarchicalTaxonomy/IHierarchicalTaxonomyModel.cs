using System;
using System.Linq;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Models.HierarchicalTaxonomy
{
    /// <summary>
    /// The public interface of the hierarchical taxonomy widget't model.
    /// </summary>
    public interface IHierarchicalTaxonomyModel
    {
        /// <summary>
        /// Gets or sets the full name of the static type that taxons associated to will be displayed.
        /// </summary>
        /// <value>The full name of the content type.</value>
        string ContentTypeName { get; set; }

        /// <summary>
        /// Gets or sets the full name of the dynamic type that taxons associated to will be displayed.
        /// </summary>
        /// <value>The full name of the dynamic content type.</value>
        string DynamicContentTypeName { get; set; }

        /// <summary>
        /// Gets or sets the name of the provider of the content type that filters the displayed taxa.
        /// </summary>
        /// <value>The name of the content provider.</value>
        string ContentProviderName { get; set; }

        /// <summary>
        /// Determiens whether to display the count of the items associated with every taxon.
        /// </summary>
        /// <value>Show item count.</value>
        bool ShowItemCount { get; set; }

        /// <summary>
        /// Gets or sets the URL of the page where content will be filtered by selected taxon.
        /// </summary>
        /// <value>The base URL.</value>
        string BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the name taxonomy provider.
        /// </summary>
        /// <value>The taxonomy provider.</value>
        string TaxonomyProviderName { get; set; }

        /// <summary>
        /// Gets or sets the taxonomy id.
        /// </summary>
        /// <value>The taxonomy id.</value>
        Guid TaxonomyId { get; set; }

        /// <summary>
        /// Gets or sets the Id of the content item for which the control should display the taxa.
        /// </summary>
        /// <value>The content id.</value>
        Guid ContentId { get; set; }

        /// <summary>
        /// Gets or sets the name of the property that contains the taxonomy.
        /// </summary>
        /// <value>The name of the field.</value>
        string FieldName { get; set; }

        /// <summary>
        /// Gets or sets the serialized collection with the ids of the specific taxa that the widget will show.
        /// Used only if the display mode setting of the widget is set to show only specific items.
        /// </summary>
        /// <value>The serialized collection with the selected taxa ids.</value>
        string SerializedSelectedTaxaIds { get; set; }

        /// <summary>
        /// Gets or sets the URL key prefix. Used when building and evaluating URLs together with ContentView controls
        /// </summary>
        /// <value>The URL key prefix.</value>
        string UrlKeyPrefix { get; set; }

        /// <summary>
        /// Determines what taxa will be displayed by the widget/
        /// </summary>
        /// <value>The taxa to display.</value>
        HierarchicalTaxaToDisplay TaxaToDisplay { get; set; }

        /// <summary>
        /// Determines whether to show taxa that doesn't have classificated content.
        /// </summary>
        /// <value>The show empty taxa.</value>
        bool ShowEmptyTaxa { get; set; }

        /// <summary>
        /// If set to true, all hierarchical taxa will be shown as a flat list.
        /// </summary>
        /// <value>The flatten hierarchy.</value>
        bool FlattenHierarchy { get; set; }

        /// <summary>
        /// Determines how many levels from the hierarchy to include.
        /// </summary>
        /// <value>The levels.</value>
        int Levels { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of taxa to display.
        /// </summary>
        /// <value>The taxa count limit.</value>
        int TaxaCountLimit { get; set; }

        /// <summary>
        /// Gets or sets the root category which children will be displayed as a top level in the widget.
        /// Used only if this display mode is selected.
        /// </summary>
        /// <value>The parent category.</value>
        Guid RootTaxonId { get; set; }

        /// <summary>
        /// Gets or sets the sort expression.
        /// </summary>
        /// <value>The sort expression.</value>
        string SortExpression { get; set; }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the Taxonomy widget (if such is presented).
        /// </summary>
        /// <value>The CSS class.</value>
        string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the URL evaluation mode - URL segments or query string.
        /// The value of this property indicates which one is used.
        /// </summary>
        UrlEvaluationMode UrlEvaluationMode { get; set; }

        /// <summary>
        /// Creates the view model.
        /// </summary>
        /// <returns></returns>
        TaxonomyViewModel CreateViewModel();
    }
}
