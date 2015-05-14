using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Css;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Identity;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Lists;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Media;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.ModuleBuilder;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.WidgetTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    /// <summary>
    /// This is the entry point class for all backend wrappers.
    /// </summary>
    public class BackendWrappersFacade
    {
        /// <summary>
        /// Provides unified access to the pages
        /// </summary>
        /// <returns>Returns the PagesWrapperFacade</returns>
        public PagesWrapperFacade Pages()
        {
            return new PagesWrapperFacade();
        }

        /// <summary>
        /// Provides unified access to the content block
        /// </summary>
        /// <returns>Returns the ContentBlocksWrapperFacade</returns>
        public ContentBlocksWrapperFacade ContentBlocks()
        {
            return new ContentBlocksWrapperFacade();
        }

        /// <summary>
        /// Provides unified access to the navigation
        /// </summary>
        /// <returns>Returns the NavigationWidgetEditWrapper</returns>
        public NavigationWrapperFacade Navigation()
        {
            return new NavigationWrapperFacade();
        }

        /// <summary>
        /// Provides access to module builder.
        /// </summary>
        /// <returns>Returns the ModuleBuilderWrapperFacade.</returns>
        public ModuleBuilderWrapperFacade ModuleBuilder()
        {
            return new ModuleBuilderWrapperFacade();
        }

        /// <summary>
        /// Commons this instance.
        /// </summary>
        /// <returns></returns>
        public WidgetsFacade Widgets()
        {
            return new WidgetsFacade();
        }

        /// <summary>
        /// Commons this instance.
        /// </summary>
        /// <returns></returns>
        public SocialShareWrapperFacade SocialShare()
        {
            return new SocialShareWrapperFacade();
        }

        /// <summary>
        /// Widgets templates.
        /// </summary>
        /// <returns></returns>
        public WidgetTemplatesWrapperFacade WidgetTemplates()
        {
            return new WidgetTemplatesWrapperFacade();
        }

        /// <summary>
        /// Search.
        /// </summary>
        /// <returns></returns>
        public SearchWrapperFacade Search()
        {
            return new SearchWrapperFacade();
        }

        /// <summary>
        /// Media.
        /// </summary>
        /// <returns></returns>
        public MediaWrapperFacade Media()
        {
            return new MediaWrapperFacade();
        }

        /// <summary>
        /// Login and registration widgets.
        /// </summary>
        /// <returns></returns>
        public IdentityWrapperFacade Identity()
        {
            return new IdentityWrapperFacade();
        }

        /// <summary>
        /// Lists widget.
        /// </summary>
        /// <returns></returns>
        public ListsWrapperFacade Lists()
        {
            return new ListsWrapperFacade();
        }

        /// <summary>
        /// ScriptsAndStyles widget.
        /// </summary>
        /// <returns></returns>
        public ScriptsAndStylesWrapperFacade ScriptAndStyles()
        {
            return new ScriptsAndStylesWrapperFacade();
        }
    }
}
