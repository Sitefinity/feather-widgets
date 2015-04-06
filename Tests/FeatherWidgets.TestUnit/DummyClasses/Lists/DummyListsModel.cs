using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Frontend.Lists.Mvc.Models;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Lists.Model;
using Telerik.Sitefinity.Model;

namespace FeatherWidgets.TestUnit.DummyClasses.Media
{
    internal class DummyListsModel : ListsModel, IListsModel
    {
        public DummyListsModel()
        {
        }

        protected internal List<DummyList> Items { get; set; }

        public override ContentListViewModel CreateListViewModel(Telerik.Sitefinity.Taxonomies.Model.ITaxon taxonFilter, int page)
        {
            return new ContentListViewModel();
        }

        public override ContentDetailsViewModel CreateDetailsViewModel(IDataItem item)
        {
            return new ContentDetailsViewModel();
        }

        protected override IQueryable<IDataItem> GetItemsQuery()
        {
            return this.Items.AsQueryable();
        }

        internal class DummyList : List
        {
            public DummyList(string name, Guid id)
                : base(name, id)
            {
            }

            public override Lstring Title
            {
                get;
                set;
            }
        }
    }
}
