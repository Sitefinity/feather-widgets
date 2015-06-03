using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using ServiceStack.Text;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Frontend.Taxonomies.Helpers;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UrlEvaluation;

namespace Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Models
{
    /// <summary>
    /// The model of the taxonomies widgets.
    /// </summary>
    public abstract class TaxonomyModel
    {
        #region Construction
        public TaxonomyModel()
        {
            this.ShowItemCount = true;
            this.SortExpression = DefaultSortExpression;
            this.UrlEvaluationMode = UrlEvaluationMode.UrlPath;
            this.ContentTypeName = ContentTypeExtensions.GetContentTypes().Select(x => x.FullTypeName).FirstOrDefault();
        }

        #endregion

        #region ITaxonomyModel implementation
        /// <summary>
        /// Gets or sets the full name of the static type that taxons associated to will be displayed.
        /// </summary>
        /// <value>The full name of the content type.</value>
        public string ContentTypeName
        {
            get
            {
                return this.contentTypeName;
            }
            set
            {
                this.contentTypeName = value;

                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.dynamicContentTypeName = string.Empty;
                    this.contentType = null;
                }
            }
        }

        /// <summary>
        /// Gets or sets the full name of the dynamic type that taxons associated to will be displayed.
        /// </summary>
        /// <value>The full name of the dynamic content type.</value>
        public string DynamicContentTypeName
        {
            get
            {
                return this.dynamicContentTypeName;
            }
            set
            {
                this.dynamicContentTypeName = value;

                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.contentTypeName = string.Empty;
                    this.contentType = null;
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the provider of the content type that filters the displayed taxa.
        /// </summary>
        /// <value>The name of the content provider.</value>
        public string ContentProviderName { get; set; }

        /// <summary>
        /// Determiens whether to display the count of the items associated with every taxon.
        /// </summary>
        /// <value>Show item count.</value>
        public bool ShowItemCount { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of taxa to display.
        /// </summary>
        /// <value>The taxa count limit.</value>
        public int TaxaCountLimit { get; set; }

        /// <summary>
        /// Gets or sets whether to show empty taxa.
        /// </summary>
        /// <value>Show empty taxonomies.</value>
        public bool ShowEmptyTaxa { get; set; }

        /// <summary>
        /// Gets or sets the URL of the page where content will be filtered by selected taxon.
        /// </summary>
        /// <value>The base URL.</value>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the taxonomy provider.
        /// </summary>
        /// <value>The taxonomy provider.</value>
        public string TaxonomyProviderName { get; set; }

        /// <summary>
        /// Gets or sets the taxonomy id.
        /// This is the default Id used by all sites and is not resolved for the current site.
        /// Plese use the ResolvedTaxonomyId when fetching taxa because it returns the id of the split taxonomy or the default one used by the other sites.
        /// </summary>
        /// <value>The taxonomy id.</value>
        public Guid TaxonomyId
        {
            get
            {
                return this.taxonomyId;
            }
            set
            {
                if (this.taxonomyId != value)
                {
                    this.taxonomyId = value;
                    this.taxonomy = null;
                }
            }
        }

        /// <summary>
        /// Gets or sets the Id of the content item for which the control should display the taxa.
        /// </summary>
        /// <value>The content id.</value>
        public Guid ContentId { get; set; }

        /// <summary>
        /// Gets or sets the name of the property that contains the taxonomy.
        /// </summary>
        /// <value>The name of the field.</value>
        public string FieldName
        {
            get
            {
                return this.fieldName;
            }
            set
            {
                this.fieldName = value;

                if (!string.IsNullOrEmpty(this.fieldName))
                {
                    this.InitializeTaxonomyManagerFromFieldName();
                }
            }
        }
        /// <summary>
        /// Gets or sets the sort expression.
        /// </summary>
        /// <value>The sort expression.</value>
        public string SortExpression { get; set; }

        /// <summary>
        /// Gets or sets the serialized collection with the ids of the specific taxa that the widget will show.
        /// Used only if the display mode setting of the widget is set to show only specific items.
        /// </summary>
        /// <value>The serialized collection with the selected taxa ids.</value>
        public string SerializedSelectedTaxaIds
        {
            get
            {
                return this.serializedSelectedTaxaIds;
            }
            set
            {
                if (this.serializedSelectedTaxaIds != value)
                {
                    this.serializedSelectedTaxaIds = value;

                    if (!this.serializedSelectedTaxaIds.IsNullOrEmpty())
                    {
                        this.selectedTaxaIds = JsonSerializer.DeserializeFromString<IList<string>>(this.serializedSelectedTaxaIds);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the URL key prefix. Used when building and evaluating URLs together with ContentView controls
        /// </summary>
        /// <value>The URL key prefix.</value>
        public string UrlKeyPrefix { get; set; }

        /// Gets or sets the CSS class that will be applied on the wrapper div of the Taxonomy widget (if such is presented).
        /// </summary>
        /// <value>The CSS class.</value>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the URL evaluation mode - URL segments or query string.
        /// The value of this property indicates which one is used.
        /// </summary>
        public UrlEvaluationMode UrlEvaluationMode { get; set; }

        /// <summary>
        /// Creates the view model.
        /// </summary>
        /// <returns></returns>
        public abstract TaxonomyViewModel CreateViewModel();

        /// <summary>
        /// Gets the taxon URL.
        /// </summary>
        /// <param name="taxon">The taxon.</param>
        /// <returns></returns>
        public abstract string GetTaxonUrl(ITaxon taxon);

        /// <summary>
        /// Gets the id of the split taxonomy used by the current site or the id of the default taxonomy used by all sites.
        /// Use it to fetch taxa that will be relevant for the current site with regards to the taxonomy's site sharing settings.
        /// </summary>
        /// <value>The resolved taxonomy id.</value>
        protected virtual Guid ResolvedTaxonomyId
        {
            get
            {
                return this.Taxonomy.Id;
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the taxonomy manager based on the specified provider.
        /// </summary>
        protected virtual TaxonomyManager CurrentTaxonomyManager
        {
            get
            {
                if (this.taxonomyManager == null)
                {
                    this.taxonomyManager = TaxonomyManager.GetManager(this.TaxonomyProviderName);
                }
                return this.taxonomyManager;
            }
            set { this.taxonomyManager = value; }
        }

        /// <summary>
        /// Gets or sets the type of the content that taxonomy control is going to display.
        /// If ContentTypeName is set returns it otherwise try to resolve DynamicContentTypeName.
        /// </summary>
        /// <value>The type of the content.</value>
        protected virtual Type ContentType
        {
            get
            {
                if (this.contentType == null)
                {
                    string typeName = string.Empty;
                    if (!this.ContentTypeName.IsNullOrWhitespace())
                    {
                        typeName = this.ContentTypeName;
                    }
                    else if (!this.DynamicContentTypeName.IsNullOrWhitespace())
                    {
                        typeName = this.DynamicContentTypeName;
                    }

                    this.contentType = TypeResolutionService.ResolveType(typeName, false);
                }

                return this.contentType;
            }
        }

        /// <summary>
        /// Gets the instance of <see cref="ITaxonomy"/> representing the taxonomy to which the taxon field is bound to.
        /// </summary>
        protected virtual ITaxonomy Taxonomy
        {
            get
            {
                return SystemManager.CurrentContext.IsMultisiteMode ?
                    this.CurrentTaxonomyManager.GetSiteTaxonomy<Taxonomy>(this.TaxonomyId) :
                    this.CurrentTaxonomyManager.GetTaxonomy<Taxonomy>(this.TaxonomyId);
            }
        }
        
        /// <summary>
        /// Returns the property descriptor of the specified FieldName.
        /// </summary>
        protected virtual PropertyDescriptor FieldPropertyDescriptor
        {
            get
            {
                if ((this.fieldPropertyDescriptor == null && !string.IsNullOrEmpty(this.FieldName)) || 
                    (this.fieldPropertyDescriptor != null && this.fieldPropertyDescriptor.Name != this.FieldName))
                {
                    this.fieldPropertyDescriptor = TypeDescriptor.GetProperties(this.ContentType)[this.FieldName];
                }
                return this.fieldPropertyDescriptor;
            }
        }
        #endregion

        #region Protected and virtual methods
        /// <summary>
        /// Gets view models of all available taxa in a flat list.
        /// </summary>
        /// <returns></returns>
        protected virtual IList<TaxonViewModel> GetAllTaxa<T>() where T : Taxon
        {
            var statistics = this.GetTaxonomyStatistics();

            var taxa = this.Sort(CurrentTaxonomyManager.GetTaxa<T>()
                                                       .Where(t => t.Taxonomy.Id == this.ResolvedTaxonomyId));

            return this.GetFlatTaxaViewModelsWithStatistics(taxa, statistics);
        }

        /// <summary>
        /// Gets the taxa view models for each taxon by using the provided ids of taxons that we want explicitly to be shown by the widget.
        /// </summary>
        /// <returns></returns>
        protected virtual IList<TaxonViewModel> GetSpecificTaxa<T>() where T : Taxon
        {
            var selectedTaxaGuids = this.selectedTaxaIds.Select(id => new Guid(id));

            var taxa = (IEnumerable<Taxon>)this.Sort(
                CurrentTaxonomyManager.GetTaxa<T>()
                    .Where(t => selectedTaxaGuids.Contains(t.Id)));

            if (this.SortExpression == "AsSetManually")
            {
                taxa = taxa.OrderBy(t => this.selectedTaxaIds.IndexOf(t.Id.ToString()));
            }

            var statistics = this.GetTaxonomyStatistics();

            return this.GetFlatTaxaViewModelsWithStatistics(taxa, statistics);
        }

        /// <summary>
        /// Gets all taxa statistics for the currently used taxonomy.
        /// </summary>
        /// <returns></returns>
        protected virtual IQueryable<TaxonomyStatistic> GetTaxonomyStatistics()
        {
            return this.CurrentTaxonomyManager
                .GetStatistics()
                .Where(
                    t =>
                    t.TaxonomyId == this.ResolvedTaxonomyId &&
                    t.MarkedItemsCount > 0 &&
                    t.StatisticType == GenericContent.Model.ContentLifecycleStatus.Live);
        }

        /// <summary>
        /// Gets the taxa view models with statistics for the usage of each taxon in the collection.
        /// </summary>
        /// <param name="taxa">The taxa.</param>
        /// <param name="statistics">The statistics.</param>
        /// <returns></returns>
        protected virtual IList<TaxonViewModel> GetFlatTaxaViewModelsWithStatistics(IEnumerable<ITaxon> taxa, IQueryable<TaxonomyStatistic> statistics)
        {
            var result = new List<TaxonViewModel>();

            foreach (var taxon in taxa)
            {
                if (!this.HasTranslationInCurrentLanguage(taxon))
                    continue;

                var viewModel = this.FilterTaxonByCount(taxon, statistics);
                if (viewModel != null)
                {
                    result.Add(viewModel);
                }
            }

            this.PopulateCloudSize(result);

            if (this.TaxaCountLimit > 0)
            {
                result = result.Take(TaxaCountLimit).ToList();
            }

            return result;
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
            var count = statistics.Where(s => s.TaxonId == taxon.Id)
                .Aggregate(0u, (acc, stat) => acc + stat.MarkedItemsCount);

            if (count == 0 && !this.ShowEmptyTaxa) return null;

            var url = this.GetTaxonUrl(taxon);
            return new TaxonViewModel(taxon, count)
            {
                Url = this.BuildUrl(url)
            };
        }

        /// <summary>
        /// Determines whether the provided taxon has translation to the current language.
        /// </summary>
        /// <param name="taxon">The taxon.</param>
        /// <returns></returns>
        protected virtual bool HasTranslationInCurrentLanguage(ITaxon taxon)
        {
            var t = (Taxon)taxon;
            return t.AvailableLanguages.Contains(taxon.Title.CurrentLanguage.Name) ||
                t.AvailableLanguages.Count() == 1 && t.AvailableLanguages[0] == string.Empty;
        }

        /// <summary>
        /// Initializes the taxonomy manager from field name.
        /// </summary>
        protected virtual void InitializeTaxonomyManagerFromFieldName()
        {
            var taxonomyDescriptor = this.FieldPropertyDescriptor as TaxonomyPropertyDescriptor;
            if (taxonomyDescriptor != null)
            {
                this.CurrentTaxonomyManager = TaxonomyManager.GetManager(taxonomyDescriptor.MetaField.TaxonomyProvider);
            }
        }

        /// <summary>
        /// Gets the taxa contained in a field which name is stored in the FieldName property.
        /// The field belongs to a content item with id stored in the ContentId property.
        /// </summary>
        /// <returns></returns>
        protected virtual IList<TaxonViewModel> GetTaxaByContentItem()
        {
            var fieldTaxonomyDescriptor = this.FieldPropertyDescriptor as TaxonomyPropertyDescriptor;

            if (fieldTaxonomyDescriptor != null)
            {
                var content = this.GetContentItem();

                var value = this.FieldPropertyDescriptor.GetValue(content);
                if (value != null)
                {
                    var isSingleTaxon = fieldTaxonomyDescriptor.MetaField.IsSingleTaxon;
                    var taxa = this.GetTaxaFromFieldValue(value, isSingleTaxon);

                    var statistics = this.GetTaxonomyStatistics();
                    return this.GetFlatTaxaViewModelsWithStatistics(taxa, statistics);
                }
            }
            else
            {
                throw new ArgumentException(String.Format("The specified field name \"{0}\" is not a taxonomy.",
                                                          this.FieldName));
            }
            return null;
        }
        
        /// <summary>
        /// Gets the taxa from the taxonomy's field of the item.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="isSingleTaxon">The is single taxon.</param>
        /// <returns></returns>
        protected virtual IEnumerable<ITaxon> GetTaxaFromFieldValue(object value, bool isSingleTaxon)
        {
            if (isSingleTaxon)
            {
                yield return this.GetSingleTaxon(value);
            }
            else
            {
                var taxa = value as IEnumerable;
                foreach (object item in taxa)
                {
                    yield return this.GetSingleTaxon(item);
                }
            }
        }

        /// <summary>
        /// Gets the taxon from given id or taxon object.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        protected virtual ITaxon GetSingleTaxon(object value)
        {
            var result = value as Taxon;
            if (result != null)
            {
                return result;
            }

            if (value is Guid)
            {
                return this.CurrentTaxonomyManager.GetTaxon((Guid)value);
            }
            return null;
        }

        /// <summary>
        /// Resolves the name of the provider used by the manager which is responsible for the content type that is filtering the shown taxa.
        /// Returns the default provider name for the manager if ContentProviderName is not set.
        /// </summary>
        /// <returns></returns>
        protected virtual string GetContentProviderName()
        {
            string providerName = String.Empty;

            if (String.IsNullOrWhiteSpace(this.ContentProviderName))
            {
                if (!string.IsNullOrEmpty(this.ContentTypeName))
                {
                    var manager = (IProviderResolver)ManagerBase.GetMappedManager(this.ContentType);

                    return manager.GetDefaultContextProvider().Name;
                }
                else if (!String.IsNullOrEmpty(this.DynamicContentTypeName))
                {
                    var moduleBuilderProvider = ModuleBuilderManager.GetManager().Provider;

                    DynamicModuleType dynamicContentType = moduleBuilderProvider.GetDynamicModuleTypes()
                        .FirstOrDefault(t => t.TypeName == this.ContentType.Name && t.TypeNamespace == this.ContentType.Namespace);

                    if (dynamicContentType != null)
                    {
                        DynamicModuleManager.GetDefaultProviderName(dynamicContentType.ModuleName);
                    }
                }
            }
            else
            {
                providerName = this.ContentProviderName;
            }

            return providerName;
        }

        /// <summary>
        /// Gets the content item from whose field the taxa will be retrieved.
        /// </summary>
        /// <returns></returns>
        protected virtual object GetContentItem()
        {
            var provider = this.GetContentProviderName();

            if (!string.IsNullOrEmpty(this.ContentTypeName))
            {
                var manager = ManagerBase.GetMappedManager(this.ContentType, this.ContentProviderName);
                return manager.GetItem(this.ContentType, this.ContentId);
            }
            else if(!string.IsNullOrEmpty(this.DynamicContentTypeName))
            {
                var manager = DynamicModuleManager.GetManager(provider);
                return manager.GetDataItem(this.ContentType, this.ContentId);
            }

            return null;
        }

        /// <summary>
        /// Populates the taxon size used for Cloud template.
        /// </summary>
        /// <param name="taxa">The taxa.</param>
        protected virtual void PopulateCloudSize(IList<TaxonViewModel> taxa)
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
        protected virtual double StandardDeviation(ICollection<double> data, out double average)
        {
            if (data.Count == 0)
            {
                average = 0;
                return 0;
            }

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
        protected virtual int GetSize(double count, double average, double stdDev)
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

        /// <summary>
        /// Builds the full url for a particular taxon filter
        /// Override this method  to change the pattern of the url 
        /// </summary>
        /// <param name="taxonRelativeUrl">The taxon relative URL.</param>
        /// <returns></returns>
        protected virtual string BuildUrl(string taxonRelativeUrl)
        {
            var url = this.BaseUrl;

            if (string.IsNullOrEmpty(url))
            {
                var siteMap = SiteMapBase.GetCurrentProvider();
                if (siteMap == null || (siteMap != null && siteMap.CurrentNode == null))
                {
                    return string.Empty;
                }

                var psn = siteMap.CurrentNode as PageSiteNode;
                if (psn != null)
                {
                    // Check if the page is a Group page and if yes take its first child page and emit an URL that has embedded the URL of the first child
                    var temp = RouteHelper.GetFirstPageDataNode(psn, true);
                    if (psn.NodeType == NodeType.Group && temp.Url != siteMap.CurrentNode.Url)
                    {
                        url = temp.Url;
                    }
                    else
                    {
                        var getUrlMethod = psn.GetType().GetMethod("GetUrl", BindingFlags.NonPublic | BindingFlags.Instance);
                        url = getUrlMethod.Invoke(psn, new object[] { true, true }) as string;
                    }
                }
                else
                {
                    url = siteMap.CurrentNode.Url;
                }
            }
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("BaseUrl property could not be resolved.");

            if (string.IsNullOrEmpty(this.FieldName))
                throw new ArgumentNullException("FieldName property could not be resolved.");

            url = RouteHelper.ResolveUrl(url, UrlResolveOptions.Absolute);

            var urlEvaluationMode = this.UrlEvaluationMode;
            if (urlEvaluationMode == Pages.Model.UrlEvaluationMode.UrlPath)
            {
                // Pages that are migrated from 3.7 have extensions (.aspx), which are unnecessary when we have segments after the page url.
                var getCurrentNodeExtensionMethod = typeof(PageHelper).GetMethod("GetCurrentNodeExtension", BindingFlags.NonPublic | BindingFlags.Static);
                string extension = getCurrentNodeExtensionMethod.Invoke(null, null) as string;

                if (!extension.IsNullOrEmpty() && url.EndsWith(extension))
                {
                    url = url.Substring(0, url.LastIndexOf(extension));
                }
            }

            var evaluator = new TaxonomyEvaluator();
            var taxonBuildOptions = TaxonBuildOptions.None;
            if (this.Taxonomy is Telerik.Sitefinity.Taxonomies.Model.HierarchicalTaxonomy)
                taxonBuildOptions = TaxonBuildOptions.Hierarchical;
            else if (this.Taxonomy is Telerik.Sitefinity.Taxonomies.Model.FlatTaxonomy)
                taxonBuildOptions = TaxonBuildOptions.Flat;


            string urlPrefix = this.UrlEvaluationMode == UrlEvaluationMode.QueryString ? this.UrlKeyPrefix : string.Empty;

            var rootTaxonomy = this.Taxonomy.RootTaxonomy ?? this.Taxonomy;
            var evaluatedResult = evaluator.BuildUrl(rootTaxonomy.Name, taxonRelativeUrl, this.FieldName, taxonBuildOptions, urlEvaluationMode, urlPrefix);


            return string.Concat(url, evaluatedResult);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Sorts the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        protected IQueryable<Taxon> Sort(IQueryable<Taxon> list)
        {
            int? totalCount = 1;

            string sortExp = this.SortExpression;
            try
            {
                if (this.SortExpression == "AsSetManually")
                {
                    sortExp = string.Empty;

                }
                list = DataProviderBase.SetExpressions(query: list,
                                             filterExpression: null,
                                             orderExpression: sortExp,
                                             skip: null,
                                             take: null,
                                             totalCount: ref totalCount);
            }
            catch (Exception)
            {
                list = DataProviderBase.SetExpressions(query: list,
                                                 filterExpression: null,
                                                 orderExpression: DefaultSortExpression,
                                                 skip: null,
                                                 take: null,
                                                 totalCount: ref totalCount);
            }
            return list;
        }
        #endregion

        #region Private fields and constants
        private TaxonomyManager taxonomyManager;
        private Type contentType;
        private ITaxonomy taxonomy;
        private Guid taxonomyId;
        private PropertyDescriptor fieldPropertyDescriptor;
        private string serializedSelectedTaxaIds;
        private IList<string> selectedTaxaIds = new List<string>();
        private const string DefaultSortExpression = "Title ASC";
        private string contentTypeName;
        private string dynamicContentTypeName;
        private string fieldName;
        #endregion
    }
}
