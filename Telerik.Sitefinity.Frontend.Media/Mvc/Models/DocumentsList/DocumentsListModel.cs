using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.Text;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.DocumentsList
{
    public class DocumentsListModel : ContentModelBase, IDocumentsListModel
    {
        /// <inheritdoc />
        public ParentFilterMode ParentFilterMode { get; set; }

        /// <inheritdoc />
        public string SerializedSelectedParentsIds { get; set; }

        /// <summary>
        /// Gets or sets the type of content that is loaded.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public override Type ContentType
        {
            get
            {
                return typeof(Document);
            }

            set
            {
            }
        }

        /// <inheritdoc />
        protected override ContentDetailsViewModel CreateDetailsViewModelInstance()
        {
            return new DocumentDetailViewModel();
        }

        /// <inheritdoc />
        public ContentListViewModel CreateListViewModelByParent(IFolder parentItem, int page)
        {
            if (page < 1)
                throw new ArgumentException("'page' argument has to be at least 1.", "page");

            var query = ((LibrariesManager)this.GetManager()).GetDescendants(parentItem);
            if (query == null)
                return this.CreateListViewModelInstance();

            var viewModel = this.CreateListViewModelInstance();
            this.PopulateListViewModel(page, query, viewModel);

            return viewModel;
        }

        /// <inheritdoc />
        protected override void PopulateListViewModel(int page, IQueryable<IDataItem> query, ContentListViewModel viewModel)
        {
            int? totalPages = null;
            var selectedItemIds = JsonSerializer.DeserializeFromString<IList<string>>(this.SerializedSelectedParentsIds);

            if (this.ParentFilterMode == Models.ParentFilterMode.Selected && selectedItemIds.Count == 0)
            {
                viewModel.Items = Enumerable.Empty<ItemViewModel>();
            }
            else
            {
                viewModel.Items = this.ApplyListSettings(page, query, out totalPages);
            }

            this.SetViewModelProperties(viewModel, page, totalPages);
        }

        /// <inheritdoc />
        protected override ContentListViewModel CreateListViewModelInstance()
        {
            return new DocumentsListViewModel();
        }

        /// <inheritdoc />
        protected override IQueryable<IDataItem> GetItemsQuery()
        {
            var manager = (LibrariesManager)this.GetManager();

            return manager.GetDocuments();
        }

        /// <inheritdoc />
        protected override string CompileFilterExpression()
        {
            var baseExpression = base.CompileFilterExpression();

            if (this.ParentFilterMode == ParentFilterMode.Selected && !this.SerializedSelectedParentsIds.IsNullOrEmpty())
            {
                var selectedItemIds = JsonSerializer.DeserializeFromString<IList<string>>(this.SerializedSelectedParentsIds);
                var parentFilterExpression = string.Join(" OR ", selectedItemIds.Select(id => "((Parent.Id = " + id.Trim() + " AND FolderId = null)" + " OR FolderId = " + id.Trim() + ")"));
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
    }
}
