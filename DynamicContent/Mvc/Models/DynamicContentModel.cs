using System;
using System.Collections.Generic;
using System.Linq;

using ServiceStack.Text;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;

namespace DynamicContent.Mvc.Models
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
        public override IList<CacheDependencyKey> GetKeysOfDependentObjects(ContentListViewModel viewModel)
        {
            if (this.ContentType != null)
            {
                var result = new List<CacheDependencyKey>(1);
                result.Add(new CacheDependencyKey { Key = this.ContentType.FullName, Type = typeof(Telerik.Sitefinity.DynamicModules.Model.DynamicContent) });

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
                var result = new List<CacheDependencyKey>(1);
                if (viewModel.Item != null && viewModel.Item.Id != Guid.Empty)
                {
                    result.Add(new CacheDependencyKey { Key = viewModel.Item.Id.ToString().ToUpperInvariant(), Type = typeof(Telerik.Sitefinity.DynamicModules.Model.DynamicContent) });
                }

                return result;
            }
            else
            {
                return new List<CacheDependencyKey>(0);
            }
        }

        /// <inheritdoc />
        public virtual ContentListViewModel CreateListViewModelByParent(Telerik.Sitefinity.DynamicModules.Model.DynamicContent parentItem, int page)
        {
            if (page < 1)
                throw new ArgumentException("'page' argument has to be at least 1.", "page");

            var manager = DynamicModuleManager.GetManager(this.ProviderName);
            var query = manager.GetItemSuccessors(parentItem, this.ContentType);
            if (query == null)
                return this.CreateListViewModelInstance();

            var viewModel = this.CreateListViewModelInstance();
            this.PopulateListViewModel(page, query, viewModel);

            return viewModel;
        }

        /// <inheritdoc />
        protected override IQueryable<IDataItem> GetItemsQuery()
        {
            if (this.ContentType == null)
                throw new InvalidOperationException("ContentType cannot be inferred from the WidgetName. A required module might be deactivated.");

            var manager = DynamicModuleManager.GetManager(this.ProviderName);
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
    }
}
