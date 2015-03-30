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
        protected override IQueryable<IDataItem> GetItemsQuery()
        {
            var manager = (LibrariesManager)this.GetManager();

            return manager.GetDocuments();
        }
    }
}
