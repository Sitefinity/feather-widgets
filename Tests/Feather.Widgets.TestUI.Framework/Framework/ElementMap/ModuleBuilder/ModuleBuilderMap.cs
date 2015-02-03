using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.ModuleBuilder
{
    public class ModuleBuilderMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleBuilderMap" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public ModuleBuilderMap(Find find)
        {
            this.find = find;
        }

        /// <summary>
        /// Gets the ModuleBuilderEditContentTypeScreen.
        /// </summary>
        public ModuleBuilderEditContentTypeScreen ModuleBuilderEditContentTypeScreen
        {
            get
            {
                return new ModuleBuilderEditContentTypeScreen(this.find);
            }
        }

        /// <summary>
        /// Gets the DynamicWidgetAdvancedSettingsScreen
        /// </summary>
        public DynamicWidgetAdvancedSettingsScreen DynamicWidgetAdvancedSettings
        {
            get
            {
                return new DynamicWidgetAdvancedSettingsScreen(this.find);
            }
        }

        private Find find;
    }
}
