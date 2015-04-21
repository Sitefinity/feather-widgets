using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.Identity;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.Lists;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.ModuleBuilder;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend
{
    /// <summary>
    /// This is the entry point class for all frontend wrappers.
    /// </summary>
    public class FrontendWrappesFacade
    {
        /// <summary>
        /// Provides unified access to the ContentBlockWrapperFacade 
        /// </summary>
        /// <returns>Returns the ContentBlockWrapperFacade</returns>
        public ContentBlockWrapperFacade ContentBlock()
        {
            return new ContentBlockWrapperFacade();
        }

        /// <summary>
        /// Provides unified access to the NavigationWrapperFacade 
        /// </summary>
        /// <returns>Returns the NavigationWrapperFacade</returns>
        public NavigationWrapperFacade Navigation()
        {
            return new NavigationWrapperFacade();
        }

        /// <summary>
        /// Provides unified access to the NewsWrapperFacade 
        /// </summary>
        /// <returns>Returns the NewsWrapperFacade</returns>
        public NewsWrapperFacade News()
        {
            return new NewsWrapperFacade();
        }

        /// <summary>
        /// Provides unified access to the ModuleBuilderWrapperFacade 
        /// </summary>
        /// <returns>Returns the ModuleBuilderWrapperFacade</returns>
        public ModuleBuilderWrapperFacade ModuleBuilder()
        {
            return new ModuleBuilderWrapperFacade();
        }

        /// <summary>
        /// Provides unified access to the SocialShareWrapperFacade 
        /// </summary>
        /// <returns></returns>
        public SocialShareWrapperFacade SocialShare()
        {
            return new SocialShareWrapperFacade();
        }

        /// <summary>
        /// Provides unified access to the SearchWrapperFacade 
        /// </summary>
        /// <returns></returns>
        public SearchWrapperFacade Search()
        {
            return new SearchWrapperFacade();
        }

        /// <summary>
        /// Provides access to frontend common wrapper.
        /// </summary>
        /// <returns></returns>
        public FrontendCommonWrapper CommonWrapper()
        {
            return new FrontendCommonWrapper();
        }

        /// <summary>
        /// Provides access to Login and registration widgets wrappers on the frontend.
        /// </summary>
        /// <returns></returns>
        public IdentityWrapperFacade Identity()
        {
            return new IdentityWrapperFacade();
        }

        /// <summary>
        /// Images the gallery.
        /// </summary>
        /// <returns></returns>
        public ImageGalleryWrapperFacade ImageGallery()
        {
            return new ImageGalleryWrapperFacade();
        }

        /// <summary>
        /// Documents the list.
        /// </summary>
        /// <returns></returns>
        public DocumentListWrapperFacade DocumentsList()
        {
            return new DocumentListWrapperFacade();
        }

        /// <summary>
        /// Provides access to lists frontend wrapper. 
        /// </summary>
        /// <returns></returns>
        public ListsWrapperFacade Lists()
        {
            return new ListsWrapperFacade();
        }
    }
}
