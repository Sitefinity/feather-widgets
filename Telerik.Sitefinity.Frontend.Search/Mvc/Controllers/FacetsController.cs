using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Frontend.Search.Mvc.Models;
using Telerik.Sitefinity.Frontend.Search.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Search.SearchFacets;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.PropertyPersisters;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Search.Facets;
using Telerik.Sitefinity.Search.Impl.Elasticsearch;
using Telerik.Sitefinity.Search.Impl.Facets;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Search;
using Telerik.Sitefinity.Services.Search.Configuration;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.Search.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of Facets widget.
    /// </summary>
    [ControllerToolboxItem(
        Name = FacetsController.WidgetName,
        Title = nameof(SearchWidgetsResources.FacetsWidgetTitle),
        Description = nameof(SearchWidgetsResources.FacetsWidgDescription),
        ResourceClassId = nameof(SearchWidgetsResources),
        SectionName = "Search",
        ModuleName = "Search",
        Ordinal = 0.6f,
        CssClass = FacetsController.WidgetIconCssClass)]
    [Localization(typeof(SearchWidgetsResources))]
    public class FacetsController : Controller, ICustomWidgetVisualizationExtended
    {
        /// <summary>
        /// Initializes a new instance of the Facets Controller
        /// </summary>
        public FacetsController()
        {
            this.searchFacetsQueryStringPorcessor = new SearchFacetsQueryStringProcessor();
            this.SelectedFacets = new List<FacetWidgetFieldModel>();
            this.settingsProvider = new SearchFacetFieldSettingsProvider();
            this.searchFacetsViewModelBuilder = new SearchFacetsViewModelBuilder();
            this.widgetSettingsFacetFieldMapper = new WidgetSettingsFacetFieldMapper();
        }

        /// <summary>
        /// Renders appropriate view depending on the <see cref="TemplateName" />
        /// </summary>
        /// <param name="page">The current page</param>
        /// <param name="searchQuery">The search input</param>
        /// <param name="filter">base 64 encoded filter</param>
        /// <param name="language">The language</param>
        /// <param name="resultsForAllSites">The param used to filter search results per site when the index is for all sites.</param>
        /// <returns>The facet's widget view</returns>
        public ActionResult Index(int? page, string searchQuery = null, string filter = null, string language = null, bool? resultsForAllSites = null)
        {
            HttpContext.Request.QueryStringGet(nameof(page));
            HttpContext.Request.QueryStringGet(nameof(searchQuery));
            HttpContext.Request.QueryStringGet(nameof(filter));
            HttpContext.Request.QueryStringGet(nameof(language));
            HttpContext.Request.QueryStringGet(nameof(resultsForAllSites));

            if (this.ShouldShowEmptyWidgetView())
            {
                return new EmptyResult();
            }

            if (!this.IsSearchModuleActivated())
            {
                return this.Content(this.SearchModuleDeactivatedMessage);
            }

            if (!this.SearchServiceSupportsFacets())
            {
                return this.Content(this.FacetsNotSupportedMessage);
            }

            var facetsViewModel = new FacetsWidgetViewModel();
            facetsViewModel.AppliedFiltersLabel = this.AppliedFiltersLabel;
            facetsViewModel.ClearAllLabel = this.ClearAllLabel;
            facetsViewModel.FilterResultsLabel = this.FilterResultsLabel;
            facetsViewModel.ShowMoreLabel = this.ShowMoreLabel;
            facetsViewModel.ShowLessLabel = this.ShowLessLabel;
            facetsViewModel.IsShowMoreLessButtonActive = this.IsShowMoreLessButtonActive;
            facetsViewModel.DisplayItemCount = this.DisplayItemCount;

            if (!string.IsNullOrEmpty(searchQuery))
            {
                var facetableFieldsFromIndex = this.settingsProvider.GetFacetableFields(this.IndexCatalogue);
                var selectedFacetsToBeUsed = this.SelectedFacets
                    .GroupBy(x => x.FacetableFieldNames[0])
                    .Select(f => f.LastOrDefault())
                    .Where(x => facetableFieldsFromIndex.Keys.Contains(x.FacetableFieldNames[0]));

                List<FacetField> facetFields = this.widgetSettingsFacetFieldMapper.MapWidgetSettingsToFieldsModel(selectedFacetsToBeUsed);

                string decodedFilter = filter?.Base64Decode();
                var searchServiceFacetResponse = this.searchFacetsQueryStringPorcessor.GetFacets(searchQuery, language, decodedFilter, this.IndexCatalogue, facetFields, this.GetSearchFields(), resultsForAllSites);

                facetsViewModel.SearchFacets = this.searchFacetsViewModelBuilder.BuildFacetsViewModel(this.SelectedFacets, searchServiceFacetResponse, facetableFieldsFromIndex, this.SortType);
                var currentPageUrl = this.GetCurrentPageUrl();
                this.ViewBag.CurrentPageUrl = currentPageUrl;
            }

            this.ViewBag.HasAnyFacetElements = this.HasAnyFacetElements(facetsViewModel.SearchFacets);

            return this.View(this.TemplateName, facetsViewModel);
        }

        /// <summary>
        /// Determines whether the search module is activated.
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsSearchModuleActivated()
        {
            var module = SystemManager.GetModule(SearchModule.ModuleName);
            return module != null;
        }

        /// <summary>
        /// Gets or sets the search indexes.
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Search facets setup", 0)]
        [DisplayName("Search index")]
        [Placeholder("Select search index")]
        [Required(ErrorMessage = "Select search index")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [Choice(ServiceUrl = "Default.GetFacetableIndexes", ServiceWarningMessage = "No search index with facetable fields has been created yet. To manage search indexes, go to Administration > Search indexes, or contact your administrator for assistance.")]
        [Description("[{\"Type\":1,\"Chunks\":[{\"Value\":\"To display facetable fields on your site,\",\"Presentation\":[]},{\"Value\":\"select the same search index as the one\",\"Presentation\":[]},{\"Value\":\"selected in the Search box widget.\",\"Presentation\":[]}]}]")]
        public string IndexCatalogue { get; set; }

        /// <summary>
        /// Get or sets the additional fileds for facet widget
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Search facets setup", 0)]
        [TableView(Reorderable = true, Selectable = false, MultipleSelect = false)]
        [DisplayName("Set facetable fields")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"IndexCatalogue\",\"operator\":\"NotEquals\",\"value\":null }]}")]
        [PropertyPersistence(PersistAsJson = true)]
        public IList<FacetWidgetFieldModel> SelectedFacets { get; set; }

        /// <summary>
        /// Gets or sets the sort type.
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Search facets setup", 0)]
        [DisplayName("Sort fields")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"IndexCatalogue\",\"operator\":\"NotEquals\",\"value\":null }]}")]
        [Choice(Choices = "[{\"Title\":\"As set manually\",\"Name\":\"0\",\"Value\":0,\"Icon\":\"ban\"},{\"Title\":\"Alphabetically\",\"Name\":\"2\",\"Value\":2,\"Icon\":null}]")]
        public string SortType { get; set; }

        /// <summary>
        /// Gets or sets whatever the facets display item count
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Search facets setup", 1)]
        [DisplayName("Display item count")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [Choice("[{\"Title\":\"Yes\",\"Name\":\"Yes\",\"Value\":\"True\",\"Icon\":null},{\"Title\":\"No\",\"Name\":\"No\",\"Value\":\"False\",\"Icon\":null}]")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"IndexCatalogue\",\"operator\":\"NotEquals\",\"value\":null }]}")]
        public bool DisplayItemCount
        {
            get
            {
                return this.displayItemCount;
            }
            set
            {
                this.displayItemCount = value;
            }
        }

        /// <summary>
        /// Gets or sets whatever the show more/less option is active or not 
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Search facets setup", 1)]
        [DisplayName("Collapse large facet lists")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [Choice("[{\"Title\":\"Yes\",\"Name\":\"Yes\",\"Value\":\"True\",\"Icon\":null},{\"Title\":\"No\",\"Name\":\"No\",\"Value\":\"False\",\"Icon\":null}]")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"IndexCatalogue\",\"operator\":\"NotEquals\",\"value\":null }]}")]
        [Description("[{\"Type\":1,\"Chunks\":[{\"Value\":\"Specifies whether to collapse facet lists on\",\"Presentation\":[]}, {\"Value\":\"your site with more than 10 entries. If 'No'\",\"Presentation\":[2]}, {\"Value\":\"is selected, all facets are displayed.\",\"Presentation\":[2]}]}]")]
        public bool IsShowMoreLessButtonActive
        {
            get
            {
                return this.isShowMoreLessButtonActive;
            }
            set
            {
                this.isShowMoreLessButtonActive = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the template that will be displayed.
        /// </summary>
        /// <value></value>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Display settings", 1)]
        [DisplayName("Template")]
        public string TemplateName
        {
            get
            {
                return this.templateName;
            }

            set
            {
                this.templateName = value;
            }
        }

        /// <inheritdoc />
        [Browsable(false)]
        public string EmptyLinkText
        {
            get
            {
                return Res.Get<SearchWidgetsResources>().FacetWidgetEmptyResult;
            }
        }

        /// <inheritdoc />
        [Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                return this.IsSearchModuleActivated() && this.SearchServiceSupportsFacets() && string.IsNullOrEmpty(this.IndexCatalogue);
            }
        }

        /// <summary>
        /// Gets the facets widget not supported search service message
        /// </summary>
        /// <value>The facets not supported message.</value>
        [Browsable(false)]
        public virtual string FacetsNotSupportedMessage
        {
            get { return this.GetResource("FacetsNotSupportedMessage"); }
        }

        /// <summary>
        /// Gets the message shown when the newsletters module is deactivated.
        /// </summary>
        /// <value>The newsletters module deactivated message.</value>
        [Browsable(false)]
        public virtual string SearchModuleDeactivatedMessage
        {
            get { return this.GetResource("SearchModuleDeactivatedMessage"); }
        }

        /// <summary>
        /// Gets the resource.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns></returns>
        protected virtual string GetResource(string resourceName)
        {
            return Res.Get(typeof(SearchWidgetsResources), resourceName);
        }

        /// <inheritdoc />
        [Category(PropertyCategory.Advanced)]
        [DisplayName("CSS class")]
        public string WidgetCssClass { get; set; }

        /// <summary>
        /// Gets or sets the name of the fields that the search will be done by
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("Search fields")]
        [DefaultValue(FacetsController.DefaultSearchFieldNames)]
        [Description("[{\"Type\":1,\"Chunks\":[{\"Value\":\"List of fields to be used in the search facets. These fields must be the same as those specified in the Search results widget.\",\"Presentation\":[]}]}]")]
        public string SearchFields { get; set; }

        /// <summary>
        /// Gets or sets the Search facets header
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Labels and messages", 0)]
        [DisplayName("Search facets header")]
        [DefaultValue(FacetsController.FilterResultsDefaultValue)]
        public string FilterResultsLabel
        {
            get
            {
                if (string.IsNullOrEmpty(this.filterResultsLabel))
                {
                    return FacetsController.FilterResultsDefaultValue;
                }

                return this.filterResultsLabel;
            }

            set
            {
                filterResultsLabel = value;
            }
        }

        /// <summary>
        /// Gets or sets the applied filters label
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Labels and messages", 1)]
        [DisplayName("Search facets label")]
        [DefaultValue(FacetsController.AppliedFiltersDefaultValue)]
        public string AppliedFiltersLabel
        {
            get
            {
                if (string.IsNullOrEmpty(this.appliedFiltersLabel))
                {
                    return FacetsController.AppliedFiltersDefaultValue;
                }

                return this.appliedFiltersLabel;
            }

            set
            {
                this.appliedFiltersLabel = value;
            }
        }

        /// <summary>
        /// Gets or sets the clear all label.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Labels and messages", 2)]
        [DisplayName("Clear facets link")]
        [DefaultValue(FacetsController.ClearAllDefaultValue)]
        public string ClearAllLabel
        {
            get
            {
                if (string.IsNullOrEmpty(clearAllLabel))
                {
                    return FacetsController.ClearAllDefaultValue;
                }

                return this.clearAllLabel;
            }

            set
            {
                this.clearAllLabel = value;
            }
        }

        /// <summary>
        /// Gets or sets the show more label
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Labels and messages", 3)]
        [DisplayName("Show more link")]
        [DefaultValue(FacetsController.ShowMoreLabelDefaultValue)]
        public string ShowMoreLabel
        {
            get
            {
                if (string.IsNullOrEmpty(this.showMoreLabel))
                {
                    return FacetsController.ShowMoreLabelDefaultValue;
                }

                return this.showMoreLabel;
            }

            set
            {
                showMoreLabel = value;
            }
        }

        /// <summary>
        /// Gets or sets the show less label
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Labels and messages", 4)]
        [DisplayName("Show less link")]
        [DefaultValue(FacetsController.ShowLessLabelDefaultValue)]
        public string ShowLessLabel
        {
            get
            {
                if (string.IsNullOrEmpty(this.showLessLabel))
                {
                    return FacetsController.ShowLessLabelDefaultValue;
                }

                return this.showLessLabel;
            }

            set
            {
                showLessLabel = value;
            }
        }

        private bool ShouldShowEmptyWidgetView()
        {
            var isEdit = SystemManager.IsDesignMode && !SystemManager.IsPreviewMode && !SystemManager.IsInlineEditingMode;
            return (!this.IsSearchModuleActivated() && !isEdit) || string.IsNullOrEmpty(this.IndexCatalogue);
        }

        private string[] GetSearchFields()
        {
            var searchFields = this.SearchFields;
            if (string.IsNullOrEmpty(this.SearchFields))
            {
                searchFields = FacetsController.DefaultSearchFieldNames;
            }

            return searchFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private bool SearchServiceSupportsFacets()
        {
            var serchConfig = Config.Get<SearchConfig>();

            return facetableSearchServices.Contains(serchConfig.CurrentSearchService);
        }

        private bool HasAnyFacetElements(IList<SearchFacetsViewModel> searchFacets)
        {
            bool hasAnyFacetElements = false;
            if (searchFacets.Any())
            {
                hasAnyFacetElements = searchFacets.Any(sf => sf.FacetElements.Any());
            }

            return hasAnyFacetElements;
        }

        internal const string WidgetIconCssClass = "sfListitemsIcn sfMvcIcn";
        private string templateName = "Facets";
        private string clearAllLabel;
        private string appliedFiltersLabel;
        private string filterResultsLabel;
        private string showMoreLabel;
        private string showLessLabel;
        private bool isShowMoreLessButtonActive = true;
        private bool displayItemCount = true;
        private const string DefaultSearchFieldNames = "Title,Content";
        private const string WidgetName = "Facets_MVC";
        private const string ClearAllDefaultValue = "Clear all";
        private const string AppliedFiltersDefaultValue = "Applied filters";
        private const string FilterResultsDefaultValue = "Filter results";
        private const string ShowMoreLabelDefaultValue = "Show more";
        private const string ShowLessLabelDefaultValue = "Show less";
        private readonly SearchFacetsQueryStringProcessor searchFacetsQueryStringPorcessor;
        private readonly HashSet<string> facetableSearchServices = new HashSet<string>() { AzureSearchService.ServiceName, ElasticsearchService.ServiceName };
        private readonly SearchFacetFieldSettingsProvider settingsProvider;
        private readonly SearchFacetsViewModelBuilder searchFacetsViewModelBuilder;
        private readonly WidgetSettingsFacetFieldMapper widgetSettingsFacetFieldMapper;
    }
}
