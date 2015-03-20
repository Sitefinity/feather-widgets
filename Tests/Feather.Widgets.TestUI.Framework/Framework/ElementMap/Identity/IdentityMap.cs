using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Identity
{
    /// <summary>
    /// This class contains references to the elements contained in the Feather login and regisration widgets.
    /// </summary>
    public class IdentityMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityMap" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public IdentityMap(Find find)
        {
            this.find = find;
        }

        private Find find;
    }
}
