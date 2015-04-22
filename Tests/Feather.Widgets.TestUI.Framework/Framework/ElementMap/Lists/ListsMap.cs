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
        /// Gets the Simple list template
        /// </summary>
        public SimpleListFrontend SimpleListFrontend
        {
            get
            {
                return new SimpleListFrontend(this.find);
            }
        }

        /// <summary>
        /// Gets the Expanded list template
        /// </summary>
        public ExpandedListFrontend ExpandedListFrontend
        {
            get
            {
                return new ExpandedListFrontend(this.find);
            }
        }

        /// <summary>
        /// Gets the Expandable list template
        /// </summary>
        public ExpandableListFrontend ExpandableListFrontend
        {
            get
            {
                return new ExpandableListFrontend(this.find);
            }
        }

        /// <summary>
        /// Gets the Anchor list template
        /// </summary>
        public AnchorListFrontend AnchorListFrontend
        {
            get
            {
                return new AnchorListFrontend(this.find);
            }
        }

        private Find find;
    }
}
