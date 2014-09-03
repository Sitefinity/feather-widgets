using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend
{
    public class NewsWrapperFacade
    {
        /// <summary>
        /// Provides unified access to the NewsWrapper
        /// </summary>
        /// <returns>Returns the NewsWrapper</returns>
        public NewsWrapper NewsWrapper()
        {
            return new NewsWrapper();
        }
    }
}
