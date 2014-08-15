using ArtOfTest.WebAii.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Content
{
    public class ContentMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForumsMap" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public ContentMap(Find find)
        {
            this.find = find;
        }

        /// <summary>
        /// Gets the content block widget backend
        /// </summary>
        public ContentBlockWidgetScreen ContentBlockWidget
        {
            get
            {
                return new ContentBlockWidgetScreen(this.find);
            }
        }

        private Find find;
    }
}
