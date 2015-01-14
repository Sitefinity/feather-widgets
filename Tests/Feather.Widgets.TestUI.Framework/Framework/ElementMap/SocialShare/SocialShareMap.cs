using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.SocialShare
{
    /// <summary>
    /// SocialShare Map
    /// </summary>
    public class SocialShareMap
    {
         /// <summary>
        /// Initializes a new instance of the <see cref="SocialShareMap" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public SocialShareMap(Find find)
        {
            this.find = find;
        }
 
        /// <summary>
        /// Gets the social share edit screen.
        /// </summary>
        /// <value>The social share edit screen.</value>
        public SocialShareWidgetEditScreen SocialShareWidgetEditScreen
        {
            get
            {
                return new SocialShareWidgetEditScreen(this.find);
            }
        }

        /// <summary>
        /// Gets the social share page editor.
        /// </summary>
        /// <value>The social share page editor.</value>
        public SocialShareOptions SocialSharePageEditor
        {
            get
            {
                return new SocialShareOptions(this.find);
            }
        }

        private Find find;
    }
}
