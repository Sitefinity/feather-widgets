using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    public class ContentBlocksWrapperFacade
    {
        public ContentBlockWidgetEditWrapper ContentBlocksWrapper()
        {
            return new ContentBlockWidgetEditWrapper();
        }

        public ContentBlockWidgetShareWrapper ContentBlocksShareWrapper()
        {
            return new ContentBlockWidgetShareWrapper();
        }
    }
}
