using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Selectors
{
    /// <summary>
    /// This class contains references to the elements contained in the Feather Edit Content Screen back-end screens.
    /// </summary>
    public class SelectorsMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectorsMap" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public SelectorsMap(Find find)
        {
            this.find = find;
        }

        /// <summary>
        /// Gets the news widget backend
        /// </summary>
        public SelectorsScreen SelectorsScreen
        {
            get
            {
                return new SelectorsScreen(this.find);
            }
        }

        private Find find;
    }
}
