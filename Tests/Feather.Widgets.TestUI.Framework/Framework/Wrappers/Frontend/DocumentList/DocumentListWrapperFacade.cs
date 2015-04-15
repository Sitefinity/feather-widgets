using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend
{
    public class DocumentListWrapperFacade
    {
        /// <summary>
        /// Provides unified access to the DocumentListWrapper
        /// </summary>
        /// <returns>Returns the DocumentListWrapper</returns>
        public DocumentListWrapper DocumentListWrapper()
        {
            return new DocumentListWrapper();
        }
    }
}
