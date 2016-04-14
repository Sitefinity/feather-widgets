using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.ImageGallery;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.RelatedData;

namespace FeatherWidgets.TestUnit.DummyClasses.Media
{
    /// <summary>
    /// This class is used for testing classes that depend on IImageGalleryModel.
    /// </summary>
    internal class DummyImageGalleryModel : IImageGalleryModel
    {
        public Telerik.Sitefinity.Frontend.Mvc.Models.ContentDetailsViewModel CreateDetailsViewModel(Telerik.Sitefinity.Model.IDataItem item, int? itemIndex)
        {
            return new Telerik.Sitefinity.Frontend.Mvc.Models.ContentDetailsViewModel();
        }

        public Telerik.Sitefinity.Frontend.Mvc.Models.ContentListViewModel CreateListViewModel(Telerik.Sitefinity.Taxonomies.Model.ITaxon taxonFilter, int page)
        {
            return new Telerik.Sitefinity.Frontend.Mvc.Models.ContentListViewModel();
        }

        public Telerik.Sitefinity.Frontend.Mvc.Models.ContentListViewModel CreateListViewModelByParent(IFolder parentItem, int p)
        {
            return new Telerik.Sitefinity.Frontend.Mvc.Models.ContentListViewModel();
        }

        public ContentListViewModel CreateListViewModelByRelatedItem(IDataItem relatedItem, int page)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public void SetRelatedDataProperties(IDataItem relatedItem, RelatedDataViewModel relatedDataViewModel)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public void SetModelProperties(ContentListSettingsViewModel viewModel)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public string DetailCssClass
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool? DisableCanonicalUrlMetaTag
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Telerik.Sitefinity.Frontend.Mvc.Models.ListDisplayMode DisplayMode
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool EnableSocialSharing
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string FilterExpression
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IList<Telerik.Sitefinity.Data.CacheDependencyKey> GetKeysOfDependentObjects(Telerik.Sitefinity.Frontend.Mvc.Models.ContentDetailsViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public IList<Telerik.Sitefinity.Data.CacheDependencyKey> GetKeysOfDependentObjects(Telerik.Sitefinity.Frontend.Mvc.Models.ContentListViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Telerik.Sitefinity.ContentLocations.IContentLocationInfo> GetLocations()
        {
            return this.DummyLocations;
        }

        public int? ItemsPerPage { get; set; }

        public string ListCssClass
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Telerik.Sitefinity.Frontend.Media.Mvc.Models.ParentFilterMode ParentFilterMode
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string ProviderName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Telerik.Sitefinity.Frontend.Mvc.Models.SelectionMode SelectionMode
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string SerializedAdditionalFilters
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string SerializedSelectedItemsIds
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string SerializedSelectedParentsIds
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string SortExpression
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<Telerik.Sitefinity.ContentLocations.IContentLocationInfo> DummyLocations { get; set; }

        public string SerializedThumbnailSizeModel
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string SerializedImageSizeModel
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string RelatedItemType
        {
            get
            {
                // TODO: Implement this property getter
                throw new NotImplementedException();
            }

            set
            {
                // TODO: Implement this property setter
                throw new NotImplementedException();
            }
        }

        public string RelatedItemProviderName
        {
            get
            {
                // TODO: Implement this property getter
                throw new NotImplementedException();
            }

            set
            {
                // TODO: Implement this property setter
                throw new NotImplementedException();
            }
        }

        public string RelatedFieldName
        {
            get
            {
                // TODO: Implement this property getter
                throw new NotImplementedException();
            }

            set
            {
                // TODO: Implement this property setter
                throw new NotImplementedException();
            }
        }

        public RelationDirection RelationTypeToDisplay
        {
            get
            {
                // TODO: Implement this property getter
                throw new NotImplementedException();
            }

            set
            {
                // TODO: Implement this property setter
                throw new NotImplementedException();
            }
        }

        public string UrlKeyPrefix { get; set; }
    }
}