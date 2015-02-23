using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.ImageGallery
{
    public class ImageGalleryModel : IImageGalleryModel
    {
        public IEnumerable<ContentLocations.IContentLocationInfo> GetLocations()
        {
            throw new NotImplementedException();
        }


        public Frontend.Mvc.Models.ContentListViewModel CreateListViewModel(Taxonomies.Model.ITaxon taxonFilter, int page)
        {
            throw new NotImplementedException();
        }

        public Frontend.Mvc.Models.ContentDetailsViewModel CreateDetailsViewModel(Model.IDataItem item)
        {
            throw new NotImplementedException();
        }

        public IList<Data.CacheDependencyKey> GetKeysOfDependentObjects(Frontend.Mvc.Models.ContentListViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public IList<Data.CacheDependencyKey> GetKeysOfDependentObjects(Frontend.Mvc.Models.ContentDetailsViewModel viewModel)
        {
            throw new NotImplementedException();
        }


        public Frontend.Mvc.Models.ContentListViewModel CreateListViewModelByParent(Model.IDataItem parentItem, int p)
        {
            throw new NotImplementedException();
        }
    }
}
