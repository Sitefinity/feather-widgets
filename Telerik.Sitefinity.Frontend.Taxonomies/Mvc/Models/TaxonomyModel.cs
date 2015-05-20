using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ServiceStack.Text;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

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
        public string ShowItemCount { get; set; }

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
        protected virtual IList<TaxonViewModel> GetSpecificTaxa()
        {
            var selectedTaxaGuids = this.selectedTaxaIds.Select(id => new Guid(id));

            var taxa = this.CurrentTaxonomyManager
                .GetTaxa<ITaxon>()
                .Where(t => selectedTaxaGuids.Contains(t.Id));

            return this.GetTaxaViewModelsWithStatistics(taxa);
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
        /// <returns></returns>
        protected virtual IList<TaxonViewModel> GetTaxaViewModelsWithStatistics(IEnumerable<ITaxon> taxa)
        {
            var statistics = this.GetTaxonomyStatistics();

            var result = new List<TaxonViewModel>();

            foreach (var taxon in taxa)
            {
                uint count = statistics
                    .Where(s => s.TaxonId == taxon.Id)
                    .Aggregate(0u, (acc, stat) => acc + stat.MarkedItemsCount);

                var viewModel = new TaxonViewModel(taxon, count);
                result.Add(viewModel);
            }

            return result;
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Resolves the name of the provider used by the manager which is responsible for the content type that is filtering the shown taxa.
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

        #endregion

        #region Private fields and constants
        private TaxonomyManager taxonomyManager;
        private Type contentType;
        private ITaxonomy taxonomy;
        private PropertyDescriptor fieldPropertyDescriptor;
        private string serializedSelectedTaxaIds;
        private IList<string> selectedTaxaIds = new List<string>();
        #endregion
    }
}
