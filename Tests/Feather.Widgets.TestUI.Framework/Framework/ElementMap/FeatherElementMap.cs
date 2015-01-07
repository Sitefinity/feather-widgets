using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework.Framework.ElementMap.Widgets;
using Feather.Widgets.TestUI.Framework.Framework.ElementMap.Content;
using Feather.Widgets.TestUI.Framework.Framework.ElementMap.Navigation;
using Feather.Widgets.TestUI.Framework.Framework.ElementMap.ModuleBuilder;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap
{
    /// <summary>
    /// This is the entry point class for all Feather element maps.
    /// </summary>
    public class FeatherElementMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatherElementMap" /> class.
        /// </summary>
        public FeatherElementMap()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatherElementMap" /> class.
        /// </summary>
        /// <param name="find">The find object used to get the elements/controls.</param>
        public FeatherElementMap(Find find)
        {
            this.find = find;
        }

        /// <summary>
        /// Gets the content element map.
        /// </summary>
        /// <value>An initialized instance of events element map.</value>
        public ContentMap GenericContent
        {
            get
            {
                if (this.contentMap == null)
                {
                    this.EnsureFindIsInitialized();
                    this.contentMap = new ContentMap(this.find);
                }

                return this.contentMap;
            }

            private set
            {
                this.contentMap = value;
            }
        }

        /// <summary>
        /// Gets the navigation element map.
        /// </summary>
        /// <value>An initialized instance of events element map.</value>
        public NavigationMap Navigation
        {
            get
            {
                if (this.navigationMap == null)
                {
                    this.EnsureFindIsInitialized();
                    this.navigationMap = new NavigationMap(this.find);
                }

                return this.navigationMap;
            }

            private set
            {
                this.navigationMap = value;
            }
        }

        /// <summary>
        /// Gets the news element map.
        /// </summary>
        /// <value>An initialized instance of events element map.</value>
        public WidgetDesignerContentScreenMap Widgets
        {
            get
            {
                if (this.widgetDesignerContentScreenMap == null)
                {
                    this.EnsureFindIsInitialized();
                    this.widgetDesignerContentScreenMap = new WidgetDesignerContentScreenMap(this.find);
                }

                return this.widgetDesignerContentScreenMap;
            }

            private set
            {
                this.widgetDesignerContentScreenMap = value;
            }
        }

        public ModuleBuilderMap ModuleBuilder
        {
            get
            {
                if (this.moduleBuilderMap == null)
                {
                    this.EnsureFindIsInitialized();
                    this.moduleBuilderMap = new ModuleBuilderMap(this.find);
                }

                return this.moduleBuilderMap;
            }

            private set
            {
                this.moduleBuilderMap = value;
            }
        }

        private void EnsureFindIsInitialized()
        {
            if (this.find == null)
            {
                throw new NotSupportedException("The element map can't be used without specifying its Find object.");
            }
        }

        private Find find;
        private ContentMap contentMap;
        private NavigationMap navigationMap;
        private ModuleBuilderMap moduleBuilderMap;
        private WidgetDesignerContentScreenMap widgetDesignerContentScreenMap;
    }
}
