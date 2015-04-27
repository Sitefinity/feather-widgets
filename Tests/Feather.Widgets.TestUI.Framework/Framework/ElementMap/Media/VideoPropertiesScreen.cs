using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Media
{
    /// <summary>
    /// Provides access to VideoPropertiesScreen
    /// </summary>
    public class VideoPropertiesScreen : MediaPropertiesBaseScreen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImagePropertiesScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public VideoPropertiesScreen(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets the aspect ratio selector.
        /// </summary>
        public HtmlSelect AspectRatioSelector
        {
            get
            {
                return this.Get<HtmlSelect>("tagName=select", "ng-model=model.aspectRatio");
            }
        }

        /// <summary>
        /// Gets the play button.
        /// </summary>
        /// <value>The play button.</value>
        public HtmlDiv PlayButton
        {
            get
            {
                return this.Get<HtmlDiv>("tagName=div", "class=sf-Media-play-button glyphicon glyphicon-play");
            }
        }

        /// <summary>
        /// Gets the small video holder.
        /// </summary>
        /// <value>The small video holder.</value>
        public HtmlDiv SmallVideoHolder
        {
            get
            {
                return this.Get<HtmlDiv>("tagName=div", "class=col-md-5 text-center");
            }
        }

        /// <summary>
        /// Gets the big video holder.
        /// </summary>
        /// <value>The big video holder.</value>
        public HtmlDiv BigVideoHolder
        {
            get
            {
                return this.Get<HtmlDiv>("tagName=div", "class=text-center col-md-12 sf-Media--info-video");
            }
        }

        /// <summary>
        /// Gets the width number.
        /// </summary>
        /// <value>The width number.</value>
        public HtmlInputNumber WidthNumber
        {
            get
            {
                return this.Get<HtmlInputNumber>("tagName=input", "ng-model=model.width");
            }
        }

        /// <summary>
        /// Gets max height number.
        /// </summary>
        /// <value>The height number.</value>
        public HtmlInputNumber HeightNumber
        {
            get
            {
                return this.Get<HtmlInputNumber>("tagName=input", "ng-model=model.height");
            }
        }
    }
}
