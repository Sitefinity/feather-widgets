﻿using ArtOfTest.WebAii.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private Find find;
    }
}
