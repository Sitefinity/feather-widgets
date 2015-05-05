using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.CSS
{
    /// <summary>
    /// This class contains references to the elements contained in the Feather css widget back-end screens.
    /// </summary>
    public class CssMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CssMap" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public CssMap(Find find)
        {
            this.find = find;
        }

        /// <summary>
        /// Gets the css widget backend
        /// </summary>
        public CssWidgetEditScreen CssWidgetEditScreen
        {
            get
            {
                return new CssWidgetEditScreen(this.find);
            }
        }

        private Find find;
    }
}
