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
    }
}
