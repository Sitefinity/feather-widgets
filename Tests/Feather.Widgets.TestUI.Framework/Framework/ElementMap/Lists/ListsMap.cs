using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Lists
{
    /// <summary>
    /// This class contains references to the elements contained in the Feather Lists widget screens.
    /// </summary>
    public class ListsMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListsMap" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public ListsMap(Find find)
        {
            this.find = find;
        }

        /// <summary>
        /// Gets the Lists widget edit screen
        /// </summary>
        public ListsWidgetEditScreen ListsWidgetEditScreen
        {
            get
            {
                return new ListsWidgetEditScreen(this.find);
            }
        }

        /// <summary>
        /// Gets the Lists widet frontend screen
        /// </summary>
        public SimpleListsFrontend SimpleListsFrontend
        {
            get
            {
                return new SimpleListsFrontend(this.find);
            }
        }

        private Find find;
    }
}
