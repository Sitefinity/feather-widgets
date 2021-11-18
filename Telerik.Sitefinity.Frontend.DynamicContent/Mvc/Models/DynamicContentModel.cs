using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using ServiceStack.Text;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.DynamicContent.Mvc.Models
{
    /// <summary>
    /// This class represents model used for DynamicContent widget.
    /// </summary>
    public class DynamicContentModel : ContentModelBase, IDynamicContentModel
    {
        /// <inheritdoc />
        public bool ListMode { get; set; }

        /// <inheritdoc />
        public ParentFilterMode ParentFilterMode { get; set; }

        /// <inheritdoc />
        public string CurrentlyOpenParentType { get; set; }

        /// <inheritdoc />
        public string SerializedSelectedParentsIds { get; set; }

        /// <inheritdoc />
        public bool ShowListViewOnEmpyParentFilter { get; set; }

        /// <inheritdoc />
        public bool HideListViewOnChildDetailsView { get; set; }

        /// <inheritdoc />
        public bool ShowDetailsViewOnChildDetailsView { get; set; }

        /// <inheritdoc />
        public override IList<CacheDependencyKey> GetKeysOfDependentObjects(ContentListViewModel viewModel)
        {
            if (this.ContentType != null)
            {
                var result = new List<CacheDependencyKey>(2);
                var manager = this.GetManager();
                string applicationName = manager != null && manager.Provider != null ? manager.Provider.ApplicationName : string.Empty;

                result.AddRange(OutputCacheDependencyHelper.GetPublishedContentCacheDependencyKeys(this.ContentType, applicationName));
                result.Add(new CacheDependencyKey() { Key = this.ContentType.FullName, Type = typeof(DynamicModule) });

                this.AddCommonDependencies(result, this.ContentType);
                return result;
            }
            else
            {
                return new List<CacheDependencyKey>(0);
            }
        }

        /// <inheritdoc />
        public override IList<CacheDependencyKey> GetKeysOfDependentObjects(ContentDetailsViewModel viewModel)
        {
            if (this.ContentType != null)
            {
                var result = new List<CacheDependencyKey>(2);
                if (viewModel.Item != null && viewModel.Item.Fields.Id != Guid.Empty)
                {
                    result.AddRange(OutputCacheDependencyHelper.GetPublishedContentCacheDependencyKeys(this.ContentType, viewModel.Item.Fields.Id));
                    result.Add(new CacheDependencyKey() { Key = this.ContentType.FullName, Type = typeof(DynamicModule) });
                }

                this.AddCommonDependencies(result, this.ContentType, viewModel.Item);

                return result;
            }
            else
            {
                return new List<CacheDependencyKey>(0);
            }
        }

        /// <inheritdoc />
        public bool HideListView(RequestContext context)
        {
            if (this.HideListViewOnChildDetailsView)
            {
                var contentType = this.ContentType;
                var routeParams = MvcRequestContextBuilder.GetRouteParams(context);
                var urlParamsString = RouteHelper.GetUrlParameterString(routeParams);
                if (urlParamsString != null)
                {
                    var manager = this.GetManager() as DynamicModuleManager;

                    var allTypes = ModuleBuilderManager.GetModules().SelectMany(x => x.Types);
                    var dynamicModuleType = allTypes.FirstOrDefault(x => x.TypeNamespace == contentType.Namespace && x.TypeName == contentType.Name);
                    var dynamicModuleTypesForThisModule = allTypes.Where(x => x.ModuleId == dynamicModuleType.ModuleId).ToList();

                    var successorTypes = this.GetTypeSuccessors(dynamicModuleTypesForThisModule, dynamicModuleType).Select(c => c.GetFullTypeName()).ToList();
                    var item = manager.Provider.GetItemsFromUrl(urlParamsString, successorTypes, true).FirstOrDefault();
                    if (item != null)
                    {
                        return successorTypes.Contains(item.GetType().FullName);
                    }
                }
            }

            return false;
        }

        /// <inheritdoc />
        public virtual ContentListViewModel CreateListViewModelByParent(Telerik.Sitefinity.DynamicModules.Model.DynamicContent parentItem, int page)
        {
            if (page < 1)
                throw new ArgumentException("'page' argument has to be at least 1.", "page");

            var manager = this.GetManagerInstance();
            var query = manager.GetItemSuccessors(parentItem, this.ContentType);
            if (query == null)
                return this.CreateListViewModelInstance();

            var viewModel = this.CreateListViewModelInstance();
            this.PopulateListViewModel(page, query, viewModel);

            return viewModel;
        }

        /// <inheritdoc />
        public override ContentDetailsViewModel CreateDetailsViewModel(IDataItem item)
        {
            var viewModel = base.CreateDetailsViewModel(item);

            viewModel.ProviderName = this.GetManagerInstance().Provider.Name;

            return viewModel;
        }

        /// <inheritdoc />
        protected override IQueryable<IDataItem> GetItemsQuery()
        {
            if (this.ContentType == null)
                throw new InvalidOperationException("ContentType cannot be inferred from the WidgetName. A required module might be deactivated.");

            var manager = this.GetManagerInstance();

            return manager.GetDataItems(this.ContentType);
        }

        /// <inheritdoc />
        protected override string CompileFilterExpression()
        {
            var baseExpression = base.CompileFilterExpression();

            if (this.ParentFilterMode == ParentFilterMode.Selected && this.SerializedSelectedParentsIds.IsNullOrEmpty() == false)
            {
                var selectedItemIds = JsonSerializer.DeserializeFromString<IList<string>>(this.SerializedSelectedParentsIds);
                var parentFilterExpression = string.Join(" OR ", selectedItemIds.Select(id => "SystemParentId = " + id.Trim()));
                if (baseExpression.IsNullOrEmpty())
                    return "({0})".Arrange(parentFilterExpression);
                else
                    return "({0}) and ({1})".Arrange(baseExpression, parentFilterExpression);
            }
            else
            {
                return baseExpression;
            }
        }

        /// <inheritdoc />
        protected override ContentListViewModel CreateListViewModelInstance()
        {
            return new DynamicContentListViewModel();
        }

        /// <inheritdoc />
        protected override IManager GetManager()
        {
            if (this.ContentType == null)
                throw new InvalidOperationException("Cannot resolve manager because ContentType is not set.");

            return this.GetManagerInstance();
        }

        /// <summary>
        /// Gets the manager instance.
        /// </summary>
        /// <returns></returns>
        protected virtual DynamicModuleManager GetManagerInstance()
        {
            if (this.ProviderName == null)
            {
                var dynamicType = ModuleBuilderManager.GetActiveTypes().FirstOrDefault(t => t.FullTypeName == this.ContentType.FullName);
                this.ProviderName = DynamicModuleManager.GetDefaultProviderName(dynamicType.ModuleName);
            }

            var manager = DynamicModuleManager.GetManager(this.ProviderName);

            return manager;
        }

        private IEnumerable<IDynamicModuleType> GetTypeSuccessors(IEnumerable<IDynamicModuleType> dynamicModuleTypes, IDynamicModuleType parent)
        {
            var result = new List<IDynamicModuleType>();
            var childTypes = dynamicModuleTypes.Where(x => x.ParentTypeId == parent.Id).ToList();
            if (childTypes.Count > 0)
            {
                result.AddRange(childTypes);
                foreach (var type in childTypes)
                {
                    var children = this.GetTypeSuccessors(dynamicModuleTypes, type);
                    result.AddRange(children);
                }
            }

            return result;
        }
    }
}
