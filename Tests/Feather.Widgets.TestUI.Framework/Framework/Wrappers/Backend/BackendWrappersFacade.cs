using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    public class BackendWrappersFacade
    {
        public PagesWrapperFacade Pages()
        {
            return new PagesWrapperFacade();
        }

        public ContentBlocksWrapperFacade ContentBlocks()
        {
            return new ContentBlocksWrapperFacade();
        }

        public NavigationWidgetEditWrapper Navigation()
        {
            return new NavigationWidgetEditWrapper();
        }
    }
}
