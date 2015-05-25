using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ServiceStack.Text;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Modules.GenericContent;
using System.Collections;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules;

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
            if (!string.IsNullOrEmpty(this.FieldName))
            {
                this.InitializeTaxonomyManagerFromFieldName();
            }
            this.ShowItemCount = true;
            this.SortExpression = DefaultSortExpression;
            this.ContentTypeName = DefaultContentType;
        }
        #endregion

        #region ITaxonomyModel implementation
        /// <summary>
        /// Gets or sets the full name of the static type that taxons associated to will be displayed.
        /// </summary>
        /// <value>The full name of the content type.</value>
        public string ContentTypeName { get; set; }

        /// <summary>
        /// Gets or sets the full name of the dynamic type that taxons associated to will be displayed.
        /// </summary>
        /// <value>The full name of the dynamic content type.</value>
        public string DynamicContentTypeName { get; set; }

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
        /// </summary>
        /// <value>The taxonomy id.</value>
        public Guid TaxonomyId { get; set; }

        /// <summary>
        /// Gets or sets the Id of the content item for which the control should display the taxa.
        /// </summary>
        /// <value>The content id.</value>
        public Guid ContentId { get; set; }

        /// <summary>
        /// Gets or sets the name of the property that contains the taxonomy.
        /// </summary>
        /// <value>The name of the field.</value>
        public string FieldName { get; set; }

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
        /// Creates the view model.
        /// </summary>
        /// <returns></returns>
        public abstract TaxonomyViewModel CreateViewModel();
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
                    if (!this.ContentTypeName.IsNullOrWhitespace())
                    {
                        this.contentType = TypeResolutionService.ResolveType(this.ContentTypeName, false);
                    }
                    else if (!this.DynamicContentTypeName.IsNullOrWhitespace())
                    {
                        this.contentType = TypeResolutionService.ResolveType(this.DynamicContentTypeName, false);
                    }
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
                if (this.taxonomy == null)
                {
                    this.taxonomy = this.CurrentTaxonomyManager.GetTaxonomy(this.TaxonomyId);
                }
                return this.taxonomy;
            }
        }

        /// <summary>
        /// Returns the property descriptor of the specified FieldName.
        /// </summary>
        protected virtual PropertyDescriptor FieldPropertyDescriptor
        {
            get
            {
                if (this.fieldPropertyDescriptor == null && !string.IsNullOrEmpty(this.FieldName))
                {
                    this.fieldPropertyDescriptor = TypeDescriptor.GetProperties(this.ContentType)[this.FieldName];
                }
                return this.fieldPropertyDescriptor;
            }
        }
        #endregion

        #region Protected and virtual methods
        /// <summary>
        /// Gets the taxa view models for each taxon by using the provided ids of taxons that we want explicitly to be shown by the widget.
        /// </summary>
        /// <returns></returns>
        protected virtual IList<TaxonViewModel> GetSpecificTaxa<T>() where T : Taxon
        {
            var selectedTaxaGuids = this.selectedTaxaIds.Select(id => new Guid(id));

            var taxa = this.CurrentTaxonomyManager.GetTaxa<T>()
                                    .Where(t => selectedTaxaGuids.Contains(t.Id));

            var statistics = this.GetTaxonomyStatistics();

            return this.GetFlatTaxaViewModelsWithStatistics(taxa, statistics);
        }

        /// <summary>
        /// Gets all taxa statistics for the currently used taxonomy.
        /// </summary>
        /// <returns></returns>
        protected virtual IQueryable<TaxonomyStatistic> GetTaxonomyStatistics()
        {
            var taxonomyIdGuid = SystemManager.CurrentContext.IsMultisiteMode ?
                this.CurrentTaxonomyManager.GetSiteTaxonomy<Taxonomy>(this.TaxonomyId).Id :
                this.TaxonomyId;

            return this.CurrentTaxonomyManager
                .GetStatistics()
                .Where(
                    t =>
                    t.TaxonomyId == taxonomyIdGuid &&
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

            return new TaxonViewModel(taxon, count);
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
                //if (!this.DynamicContentTypeName.IsNullOrWhitespace())
                //{
                //    var manager = ManagerBase.GetMappedManager(this.TaxonomyContentType);

                //    if (!SystemManager.CurrentContext.IsMultisiteMode)
                //    {
                //        providerName = manager.Provider.Name;
                //    }
                //    else
                //    {
                //        var dataSourceName = SystemManager.DataSourceRegistry.GetDataSource(manager.GetType().FullName).Name;
                //        var provider = SystemManager.CurrentContext.CurrentSite.GetDefaultProvider(dataSourceName);
                //        providerName = provider.ProviderName;
                //    }
                //}
                //else if (!String.IsNullOrWhiteSpace(this.DynamicContentType))
                //{
                //    var moduleBuilderManager = ModuleBuilderManager.GetManager();
                //    DynamicModuleType dynamicContentType = moduleBuilderManager.GetDynamicModuleType(moduleBuilderManager.ResolveDynamicClrType(this.DynamicContentType));

                //    if (dynamicContentType != null)
                //    {
                //        if (!SystemManager.CurrentContext.IsMultisiteMode)
                //        {
                //            DynamicModuleManager manager = DynamicModuleManager.GetManager();
                //            providerName = manager.Provider.Name;
                //        }
                //        else
                //        {                                                        
                //            var dataSourceName = SystemManager.DataSourceRegistry.GetDataSource(dynamicContentType.ModuleName).Name;
                //            var provider = SystemManager.CurrentContext.CurrentSite.GetDefaultProvider(dataSourceName);
                //            providerName = provider.ProviderName;
                //        }
                //    }
                //}                
            }
            else
            {
                
                providerName = this.ContentProviderName;
            }

            return providerName;
        }

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
        #endregion

        #region Private fields and constants
        private TaxonomyManager taxonomyManager;
        private Type contentType;
        private ITaxonomy taxonomy;
        private PropertyDescriptor fieldPropertyDescriptor;
        private string serializedSelectedTaxaIds;
        private IList<string> selectedTaxaIds = new List<string>();
        private const string DefaultSortExpression = "PublicationDate DESC";
        private const string DefaultContentType = "Telerik.Sitefinity.News.Model.NewsItem";
        #endregion
    }
}
