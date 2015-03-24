using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.DocumentsList;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using SfDocument = Telerik.Sitefinity.Libraries.Model.Document;

namespace FeatherWidgets.TestUnit.DummyClasses.Media
{
    public class DummyDocumentsListModel : DocumentsListModel, IDocumentsListModel
    {
        public ParentFilterMode ParentFilterMode { get; set; }

        public string SerializedSelectedParentsIds { get; set; }

        public override ContentListViewModel CreateListViewModel(Telerik.Sitefinity.Taxonomies.Model.ITaxon taxonFilter, int page)
        {
            return new ContentListViewModel();
        }

        public ContentListViewModel CreateListViewModelByParent(Telerik.Sitefinity.IFolder parentItem, int page)
        {
            return new ContentListViewModel();
        }

        protected override IQueryable<Telerik.Sitefinity.Model.IDataItem> GetItemsQuery()
        {
            return new List<SfDocument>().AsQueryable();
        }
    }
}
