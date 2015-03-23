using Ninject.Modules;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Document;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.DocumentsList;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.ImageGallery;

namespace Telerik.Sitefinity.Frontend.Media
{
    /// <summary>
    /// This class is used to describe the bindings which will be used by the Ninject container when resolving classes
    /// </summary>
    public class InterfaceMappings : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<IImageModel>().To<ImageModel>();
            Bind<IDocumentModel>().To<DocumentModel>();
            Bind<IImageGalleryModel>().To<ImageGalleryModel>();
            Bind<IDocumentsListModel>().To<DocumentsListModel>();
        }
    }
}
