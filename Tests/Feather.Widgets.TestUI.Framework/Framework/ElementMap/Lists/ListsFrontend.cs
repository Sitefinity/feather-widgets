using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Lists
{
    /// <summary>
    /// Provides access to Lists widget frontend elements.
    /// </summary>
    public class ListsFrontend : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListsFrontend" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public ListsFrontend(Find find)
            : base(find)
        {
        }
    }
}
