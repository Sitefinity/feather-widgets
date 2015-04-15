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
        /// Provides unified access to the DocumentsListWrapper
        /// </summary>
        /// <returns>Returns the DocumentsListWrapper</returns>
        public DocumentListWrapper DocumentsListWrapper()
        {
            return new DocumentListWrapper();
        }
    }
}
