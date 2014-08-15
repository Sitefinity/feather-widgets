using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework.Framework.ElementMap.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap
{
    public class FeatherElementMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SitefinityElementMap" /> class.
        /// </summary>
        public FeatherElementMap()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SitefinityElementMap" /> class.
        /// </summary>
        /// <param name="find">The find object used to get the elements/controls.</param>
        public FeatherElementMap(Find find)
        {
            this.find = find;
        }

        /// <summary>
        /// Gets or sets the events element map.
        /// It contains the finding expressions for all back-end events screens.
        /// </summary>
        /// <value>An initialized instance of events element map.</value>
        public ContentMap GenericContent
        {
            get
            {
                if (this.contentMap == null)
                {
                    this.EnsureFindIsInitialized();
                    this.contentMap = new ContentMap(this.find);
                }
                return contentMap;
            }
            private set
            {
                contentMap = value;
            }
        }

        private void EnsureFindIsInitialized()
        {
            if (this.find == null)
            {
                throw new NotSupportedException("The element map can't be used without specifying its Find object.");
            }
        }

        private Find find;
        private ContentMap contentMap;
    }
}
