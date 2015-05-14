using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    /// <summary>
    /// This is the entry point class for pages backend wrappers.
    /// </summary>
    public class PagesWrapperFacade
    {
        /// <summary>
        /// Provides unified access to the PageZoneEditorWrapper
        /// </summary>
        /// <returns>Returns the PageZoneEditorWrapper</returns>
        public PageZoneEditorWrapper PageZoneEditorWrapper()
        {
            return new PageZoneEditorWrapper();
        }

        /// <summary>
        /// Pages the zone editor media wrapper.
        /// </summary>
        /// <returns></returns>
        public PageZoneEditorMediaWrapper PageZoneEditorMediaWrapper()
        {
            return new PageZoneEditorMediaWrapper();
        }
    }
}
