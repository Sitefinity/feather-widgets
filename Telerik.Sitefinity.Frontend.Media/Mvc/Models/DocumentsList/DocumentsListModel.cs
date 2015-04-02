using System;
using System.Linq;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;
using SfDocument = Telerik.Sitefinity.Libraries.Model.Document;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.DocumentsList
{
    /// <summary>
    /// This class is used as a model for DocumentsListController.
    /// </summary>
    public class DocumentsListModel : MediaGalleryModelBase<SfDocument>, IDocumentsListModel
    {
        /// <inheritdoc />
        protected override ContentDetailsViewModel CreateDetailsViewModelInstance()
        {
            return new DocumentDetailsViewModel();
        }

        /// <inheritdoc />
        protected override ContentListViewModel CreateListViewModelInstance()
        {
            return new DocumentsListViewModel();
        }

        /// <inheritdoc />
        protected override ItemViewModel CreateItemViewModelInstance(IDataItem item)
        {
            return new DocumentItemViewModel(item);
        }

        /// <inheritdoc />
        protected override void PopulateListViewModel(int page, IQueryable<IDataItem> query, ContentListViewModel viewModel)
        {
            base.PopulateListViewModel(page, query, viewModel);

            foreach (var item in viewModel.Items)
            {
                ((DocumentItemViewModel)item).Extension = this.GetExtension((SfDocument)item.DataItem);
            }
        }

        public override ContentDetailsViewModel CreateDetailsViewModel(IDataItem item)
        {
            var viewModel = (DocumentDetailsViewModel)base.CreateDetailsViewModel(item);
            viewModel.Extension = this.GetExtension((SfDocument)item);
            
            return viewModel;
        }

        /// <inheritdoc />
        protected override IQueryable<IDataItem> GetItemsQuery()
        {
            var manager = (LibrariesManager)this.GetManager();

            return manager.GetDocuments();
        }

        /// <summary>
        /// Removes the dot at the beginning of the document extension.
        /// </summary>
        /// <param name="document">The document extension.</param>
        /// <returns>The extension without the dot at the beginning.</returns>
        protected virtual string GetExtension(SfDocument document)
        {
            var ext = document.Extension;
            if (!string.IsNullOrWhiteSpace(ext) && ext.Length > 0)
                ext = ext.Remove(0, 1);

            return ext;
        }
    }
}
