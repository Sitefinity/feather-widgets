using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend
{
    public class BlogsWrapperFacade
    {
        /// <summary>
        /// Provides unified access to the BlogsWrapper
        /// </summary>
        /// <returns>Returns the BlogsWrapper</returns>
        public BlogsWrapper BlogsWrapper()
        {
            return new BlogsWrapper();
        }
    }
}
